using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain.Localization;
using AtiehJobCore.Core.Domain.Seo;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Core.MongoDb;
using AtiehJobCore.Core.Utilities;
using AtiehJobCore.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtiehJobCore.Services.Seo
{
    public static class SeoExtensions
    {
        #region Fields

        private static Dictionary<string, string> _seoCharacterTable;
        private static readonly object SLock = new object();

        #endregion


        #region General

        /// <summary>
        /// Get search engine friendly name (slug)
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Search engine  name (slug)</returns>
        public static string GetSeName<T>(this T entity)
            where T : BaseMongoEntity, ISlugSupported, ILocalizedEntity
        {
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            return GetSeName(entity, workContext.WorkingLanguage.Id);
        }

        /// <summary>
        ///  Get search engine friendly name (slug)
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="returnDefaultValue">A value indicating whether
        /// to return default value (if language specified one is not found)</param>
        /// <param name="ensureTwoPublishedLanguages">A value indicating whether
        /// to ensure that we have at least two published languages; otherwise, load only default value</param>
        /// <returns>Search engine  name (slug)</returns>
        public static string GetSeName<T>(this T entity, string languageId, bool returnDefaultValue = true,
            bool ensureTwoPublishedLanguages = true)
            where T : BaseMongoEntity, ISlugSupported, ILocalizedEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var seName = string.Empty;
            if (!string.IsNullOrEmpty(languageId))
            {
                var value = entity.Locales.FirstOrDefault(x => x.LanguageId == languageId && x.LocaleKey == "SeName");
                if (value != null)
                    if (!string.IsNullOrEmpty(value.LocaleValue))
                        seName = value.LocaleValue;
            }

            //set default value if required
            if (string.IsNullOrEmpty(seName) && returnDefaultValue)
            {
                seName = entity.SeName;
            }

            return seName;
        }

        /// <summary>
        /// Get search engine friendly name (slug)
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="entityName">Entity name</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="returnDefaultValue">A value indicating whether to return default value
        /// (if language specified one is not found)</param>
        /// <param name="ensureTwoPublishedLanguages">A value indicating whether
        /// to ensure that we have at least two published languages; otherwise, load only default value</param>
        /// <returns>Search engine  name (slug)</returns>
        public static string GetSeName(string entityId, string entityName, string languageId, bool returnDefaultValue = true,
            bool ensureTwoPublishedLanguages = true)
        {
            var result = string.Empty;

            var urlRecordService = EngineContext.Current.Resolve<IUrlRecordService>();
            if (!string.IsNullOrEmpty(languageId))
            {
                //ensure that we have at least two published languages
                var loadLocalizedValue = true;
                if (ensureTwoPublishedLanguages)
                {
                    var lService = EngineContext.Current.Resolve<ILanguageService>();
                    var totalPublishedLanguages = lService.GetAllLanguages().Count;
                    loadLocalizedValue = totalPublishedLanguages >= 2;
                }
                //localized value
                if (loadLocalizedValue)
                {
                    result = urlRecordService.GetActiveSlug(entityId, entityName, languageId);
                }
            }
            //set default value if required
            if (string.IsNullOrEmpty(result) && returnDefaultValue)
            {
                result = urlRecordService.GetActiveSlug(entityId, entityName, "");
            }

            return result;
        }

        /// <summary>
        /// Validate search engine name
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="seName">Search engine name to validate</param>
        /// <param name="name">User-friendly name used to generate sename</param>
        /// <param name="ensureNotEmpty">Ensure that sename is not empty</param>
        /// <returns>Valid sename</returns>
        public static string ValidateSeName<T>(this T entity, string seName, string name, bool ensureNotEmpty)
              where T : BaseMongoEntity, ISlugSupported
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            //use name if sename is not specified
            if (string.IsNullOrWhiteSpace(seName) && !string.IsNullOrWhiteSpace(name))
                seName = name;

            //validation
            seName = GetSeName(seName);

            //max length
            //For long URLs we can get the following error:
            //"the specified path, file name, or both are too long. The fully qualified file name
            //must be less than 260 characters, and the directory name must be less than 248 characters"
            //that's why we limit it to 200 here (consider a store URL + probably added {0}-{1} below)
            seName = CommonHelper.EnsureMaximumLength(seName, 200);

            if (string.IsNullOrWhiteSpace(seName))
            {
                if (ensureNotEmpty)
                {
                    //use entity identifier as sename if empty
                    seName = entity.Id.ToString();
                }
                else
                {
                    //return. no need for further processing
                    return seName;
                }
            }

            //ensure this sename is not reserved yet
            var entityName = typeof(T).Name;
            var urlRecordService = EngineContext.Current.Resolve<IUrlRecordService>();
            var seoSettings = EngineContext.Current.Resolve<SeoSettings>();
            var languageService = EngineContext.Current.Resolve<ILanguageService>();
            var i = 2;
            var tempSeName = seName;
            while (true)
            {
                //check whether such slug already exists (and that is not the current entity)
                var urlRecord = urlRecordService.GetBySlug(tempSeName);
                var reserved1 = urlRecord != null && !(urlRecord.EntityId == entity.Id
                                                       && urlRecord.EntityName.Equals(entityName, StringComparison.OrdinalIgnoreCase));
                //and it's not in the list of reserved slugs
                var reserved2 = seoSettings.ReservedUrlRecordSlugs.Contains(tempSeName, StringComparer.OrdinalIgnoreCase);
                var reserved3 = languageService.GetAllLanguages(true).Any(language => language.UniqueSeoCode.Equals(
                    tempSeName, StringComparison.OrdinalIgnoreCase));
                if (!reserved1 && !reserved2 && !reserved3)
                    break;

                tempSeName = $"{seName}-{i}";
                i++;
            }
            seName = tempSeName;

            return seName;
        }


        /// <summary>
        /// Get SE name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Result</returns>
        public static string GetSeName(string name)
        {
            var seoSettings = EngineContext.Current.Resolve<SeoSettings>();
            return GetSeName(name, seoSettings.ConvertNonWesternChars, seoSettings.AllowUnicodeCharsInUrls);
        }

        /// <summary>
        /// Get SE name
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="convertNonWesternChars">A value indicating whether non western chars should be converted</param>
        /// <param name="allowUnicodeCharsInUrls">A value indicating whether Unicode chars are allowed</param>
        /// <returns>Result</returns>
        public static string GetSeName(string name, bool convertNonWesternChars, bool allowUnicodeCharsInUrls)
        {
            if (string.IsNullOrEmpty(name))
                return name;
            const string okChars = "abcdefghijklmnopqrstuvwxyz1234567890 _-";
            name = name.Trim().ToLowerInvariant();

            if (convertNonWesternChars)
            {
                if (_seoCharacterTable == null)
                    InitializeSeoCharacterTable();
            }

            var sb = new StringBuilder();
            foreach (var c in name.ToCharArray())
            {
                var c2 = c.ToString();
                if (convertNonWesternChars)
                {
                    if (_seoCharacterTable != null && _seoCharacterTable.ContainsKey(c2))
                        c2 = _seoCharacterTable[c2];
                }

                if (allowUnicodeCharsInUrls)
                {
                    if (char.IsLetterOrDigit(c) || okChars.Contains(c2))
                        sb.Append(c2);
                }
                else if (okChars.Contains(c2))
                {
                    sb.Append(c2);
                }
            }
            var name2 = sb.ToString();
            name2 = name2.Replace(" ", "-");
            while (name2.Contains("--"))
                name2 = name2.Replace("--", "-");
            while (name2.Contains("__"))
                name2 = name2.Replace("__", "_");
            return name2;
        }

        /// <summary>
        /// Stores Unicode characters and their "normalized"
        /// values to a hash table. Character codes are referenced
        /// by hex numbers because that's the most common way to
        /// refer to them.
        /// 
        /// Upper-case comments are identifiers from the Unicode database. 
        /// Lower- and mixed-case comments are the author's
        /// </summary>
        private static void InitializeSeoCharacterTable()
        {
            lock (SLock)
            {
                if (_seoCharacterTable == null)
                {
                    _seoCharacterTable = new Dictionary<string, string>
                    {
                        {ToUnichar("0041"), "A"},
                        {ToUnichar("0042"), "B"},
                        {ToUnichar("0043"), "C"},
                        {ToUnichar("0044"), "D"},
                        {ToUnichar("0045"), "E"},
                        {ToUnichar("0046"), "F"},
                        {ToUnichar("0047"), "G"},
                        {ToUnichar("0048"), "H"},
                        {ToUnichar("0049"), "I"},
                        {ToUnichar("004A"), "J"},
                        {ToUnichar("004B"), "K"},
                        {ToUnichar("004C"), "L"},
                        {ToUnichar("004D"), "M"},
                        {ToUnichar("004E"), "N"},
                        {ToUnichar("004F"), "O"},
                        {ToUnichar("0050"), "P"},
                        {ToUnichar("0051"), "Q"},
                        {ToUnichar("0052"), "R"},
                        {ToUnichar("0053"), "S"},
                        {ToUnichar("0054"), "T"},
                        {ToUnichar("0055"), "U"},
                        {ToUnichar("0056"), "V"},
                        {ToUnichar("0057"), "W"},
                        {ToUnichar("0058"), "X"},
                        {ToUnichar("0059"), "Y"},
                        {ToUnichar("005A"), "Z"},
                        {ToUnichar("0061"), "a"},
                        {ToUnichar("0062"), "b"},
                        {ToUnichar("0063"), "c"},
                        {ToUnichar("0064"), "d"},
                        {ToUnichar("0065"), "e"},
                        {ToUnichar("0066"), "f"},
                        {ToUnichar("0067"), "g"},
                        {ToUnichar("0068"), "h"},
                        {ToUnichar("0069"), "i"},
                        {ToUnichar("006A"), "j"},
                        {ToUnichar("006B"), "k"},
                        {ToUnichar("006C"), "l"},
                        {ToUnichar("006D"), "m"},
                        {ToUnichar("006E"), "n"},
                        {ToUnichar("006F"), "o"},
                        {ToUnichar("0070"), "p"},
                        {ToUnichar("0071"), "q"},
                        {ToUnichar("0072"), "r"},
                        {ToUnichar("0073"), "s"},
                        {ToUnichar("0074"), "t"},
                        {ToUnichar("0075"), "u"},
                        {ToUnichar("0076"), "v"},
                        {ToUnichar("0077"), "w"},
                        {ToUnichar("0078"), "x"},
                        {ToUnichar("0079"), "y"},
                        {ToUnichar("007A"), "z"},
                        {ToUnichar("00AA"), "a"},
                        {ToUnichar("00BA"), "o"},
                        {ToUnichar("00C0"), "A"},
                        {ToUnichar("00C1"), "A"},
                        {ToUnichar("00C2"), "A"},
                        {ToUnichar("00C3"), "A"},
                        {ToUnichar("00C4"), "A"},
                        {ToUnichar("00C5"), "A"},
                        {ToUnichar("00C6"), "AE"},
                        {ToUnichar("00C7"), "C"},
                        {ToUnichar("00C8"), "E"},
                        {ToUnichar("00C9"), "E"},
                        {ToUnichar("00CA"), "E"},
                        {ToUnichar("00CB"), "E"},
                        {ToUnichar("00CC"), "I"},
                        {ToUnichar("00CD"), "I"},
                        {ToUnichar("00CE"), "I"},
                        {ToUnichar("00CF"), "I"},
                        {ToUnichar("00D0"), "D"},
                        {ToUnichar("00D1"), "N"},
                        {ToUnichar("00D2"), "O"},
                        {ToUnichar("00D3"), "O"},
                        {ToUnichar("00D4"), "O"},
                        {ToUnichar("00D5"), "O"},
                        {ToUnichar("00D6"), "O"},
                        {ToUnichar("00D8"), "O"},
                        {ToUnichar("00D9"), "U"},
                        {ToUnichar("00DA"), "U"},
                        {ToUnichar("00DB"), "U"},
                        {ToUnichar("00DC"), "U"},
                        {ToUnichar("00DD"), "Y"},
                        {ToUnichar("00DE"), "Th"},
                        {ToUnichar("00DF"), "s"},
                        {ToUnichar("00E0"), "a"},
                        {ToUnichar("00E1"), "a"},
                        {ToUnichar("00E2"), "a"},
                        {ToUnichar("00E3"), "a"},
                        {ToUnichar("00E4"), "a"},
                        {ToUnichar("00E5"), "a"},
                        {ToUnichar("00E6"), "ae"},
                        {ToUnichar("00E7"), "c"},
                        {ToUnichar("00E8"), "e"},
                        {ToUnichar("00E9"), "e"},
                        {ToUnichar("00EA"), "e"},
                        {ToUnichar("00EB"), "e"},
                        {ToUnichar("00EC"), "i"},
                        {ToUnichar("00ED"), "i"},
                        {ToUnichar("00EE"), "i"},
                        {ToUnichar("00EF"), "i"},
                        {ToUnichar("00F0"), "d"},
                        {ToUnichar("00F1"), "n"},
                        {ToUnichar("00F2"), "o"},
                        {ToUnichar("00F3"), "o"},
                        {ToUnichar("00F4"), "o"},
                        {ToUnichar("00F5"), "o"},
                        {ToUnichar("00F6"), "o"},
                        {ToUnichar("00F8"), "o"},
                        {ToUnichar("00F9"), "u"},
                        {ToUnichar("00FA"), "u"},
                        {ToUnichar("00FB"), "u"},
                        {ToUnichar("00FC"), "u"},
                        {ToUnichar("00FD"), "y"},
                        {ToUnichar("00FE"), "th"},
                        {ToUnichar("00FF"), "y"},
                        {ToUnichar("0100"), "A"},
                        {ToUnichar("0101"), "a"},
                        {ToUnichar("0102"), "A"},
                        {ToUnichar("0103"), "a"},
                        {ToUnichar("0104"), "A"},
                        {ToUnichar("0105"), "a"},
                        {ToUnichar("0106"), "C"},
                        {ToUnichar("0107"), "c"},
                        {ToUnichar("0108"), "C"},
                        {ToUnichar("0109"), "c"},
                        {ToUnichar("010A"), "C"},
                        {ToUnichar("010B"), "c"},
                        {ToUnichar("010C"), "C"},
                        {ToUnichar("010D"), "c"},
                        {ToUnichar("010E"), "D"},
                        {ToUnichar("010F"), "d"},
                        {ToUnichar("0110"), "D"},
                        {ToUnichar("0111"), "d"},
                        {ToUnichar("0112"), "E"},
                        {ToUnichar("0113"), "e"},
                        {ToUnichar("0114"), "E"},
                        {ToUnichar("0115"), "e"},
                        {ToUnichar("0116"), "E"},
                        {ToUnichar("0117"), "e"},
                        {ToUnichar("0118"), "E"},
                        {ToUnichar("0119"), "e"},
                        {ToUnichar("011A"), "E"},
                        {ToUnichar("011B"), "e"},
                        {ToUnichar("011C"), "G"},
                        {ToUnichar("011D"), "g"},
                        {ToUnichar("011E"), "G"},
                        {ToUnichar("011F"), "g"},
                        {ToUnichar("0120"), "G"},
                        {ToUnichar("0121"), "g"},
                        {ToUnichar("0122"), "G"},
                        {ToUnichar("0123"), "g"},
                        {ToUnichar("0124"), "H"},
                        {ToUnichar("0125"), "h"},
                        {ToUnichar("0126"), "H"},
                        {ToUnichar("0127"), "h"},
                        {ToUnichar("0128"), "I"},
                        {ToUnichar("0129"), "i"},
                        {ToUnichar("012A"), "I"},
                        {ToUnichar("012B"), "i"},
                        {ToUnichar("012C"), "I"},
                        {ToUnichar("012D"), "i"},
                        {ToUnichar("012E"), "I"},
                        {ToUnichar("012F"), "i"},
                        {ToUnichar("0130"), "I"},
                        {ToUnichar("0131"), "i"},
                        {ToUnichar("0132"), "I"},
                        {ToUnichar("0133"), "i"},
                        {ToUnichar("0134"), "J"},
                        {ToUnichar("0135"), "j"},
                        {ToUnichar("0136"), "K"},
                        {ToUnichar("0137"), "k"},
                        {ToUnichar("0138"), "k"},
                        {ToUnichar("0139"), "L"},
                        {ToUnichar("013A"), "l"},
                        {ToUnichar("013B"), "L"},
                        {ToUnichar("013C"), "l"},
                        {ToUnichar("013D"), "L"},
                        {ToUnichar("013E"), "l"},
                        {ToUnichar("013F"), "L"},
                        {ToUnichar("0140"), "l"},
                        {ToUnichar("0141"), "L"},
                        {ToUnichar("0142"), "l"},
                        {ToUnichar("0143"), "N"},
                        {ToUnichar("0144"), "n"},
                        {ToUnichar("0145"), "N"},
                        {ToUnichar("0146"), "n"},
                        {ToUnichar("0147"), "N"},
                        {ToUnichar("0148"), "n"},
                        {ToUnichar("0149"), "'n"},
                        {ToUnichar("014A"), "NG"},
                        {ToUnichar("014B"), "ng"},
                        {ToUnichar("014C"), "O"},
                        {ToUnichar("014D"), "o"},
                        {ToUnichar("014E"), "O"},
                        {ToUnichar("014F"), "o"},
                        {ToUnichar("0150"), "O"},
                        {ToUnichar("0151"), "o"},
                        {ToUnichar("0152"), "OE"},
                        {ToUnichar("0153"), "oe"},
                        {ToUnichar("0154"), "R"},
                        {ToUnichar("0155"), "r"},
                        {ToUnichar("0156"), "R"},
                        {ToUnichar("0157"), "r"},
                        {ToUnichar("0158"), "R"},
                        {ToUnichar("0159"), "r"},
                        {ToUnichar("015A"), "S"},
                        {ToUnichar("015B"), "s"},
                        {ToUnichar("015C"), "S"},
                        {ToUnichar("015D"), "s"},
                        {ToUnichar("015E"), "S"},
                        {ToUnichar("015F"), "s"},
                        {ToUnichar("0160"), "S"},
                        {ToUnichar("0161"), "s"},
                        {ToUnichar("0162"), "T"},
                        {ToUnichar("0163"), "t"},
                        {ToUnichar("0164"), "T"},
                        {ToUnichar("0165"), "t"},
                        {ToUnichar("0166"), "T"},
                        {ToUnichar("0167"), "t"},
                        {ToUnichar("0168"), "U"},
                        {ToUnichar("0169"), "u"},
                        {ToUnichar("016A"), "U"},
                        {ToUnichar("016B"), "u"},
                        {ToUnichar("016C"), "U"},
                        {ToUnichar("016D"), "u"},
                        {ToUnichar("016E"), "U"},
                        {ToUnichar("016F"), "u"},
                        {ToUnichar("0170"), "U"},
                        {ToUnichar("0171"), "u"},
                        {ToUnichar("0172"), "U"},
                        {ToUnichar("0173"), "u"},
                        {ToUnichar("0174"), "W"},
                        {ToUnichar("0175"), "w"},
                        {ToUnichar("0176"), "Y"},
                        {ToUnichar("0177"), "y"},
                        {ToUnichar("0178"), "Y"},
                        {ToUnichar("0179"), "Z"},
                        {ToUnichar("017A"), "z"},
                        {ToUnichar("017B"), "Z"},
                        {ToUnichar("017C"), "z"},
                        {ToUnichar("017D"), "Z"},
                        {ToUnichar("017E"), "z"},
                        {ToUnichar("017F"), "s"},
                        {ToUnichar("0180"), "b"},
                        {ToUnichar("0181"), "B"},
                        {ToUnichar("0182"), "B"},
                        {ToUnichar("0183"), "b"},
                        {ToUnichar("0184"), "6"},
                        {ToUnichar("0185"), "6"},
                        {ToUnichar("0186"), "O"},
                        {ToUnichar("0187"), "C"},
                        {ToUnichar("0188"), "c"},
                        {ToUnichar("0189"), "D"},
                        {ToUnichar("018A"), "D"},
                        {ToUnichar("018B"), "D"},
                        {ToUnichar("018C"), "d"},
                        {ToUnichar("018D"), "d"},
                        {ToUnichar("018E"), "E"},
                        {ToUnichar("018F"), "E"},
                        {ToUnichar("0190"), "E"},
                        {ToUnichar("0191"), "F"},
                        {ToUnichar("0192"), "f"},
                        {ToUnichar("0193"), "G"},
                        {ToUnichar("0194"), "G"},
                        {ToUnichar("0195"), "hv"},
                        {ToUnichar("0196"), "I"},
                        {ToUnichar("0197"), "I"},
                        {ToUnichar("0198"), "K"},
                        {ToUnichar("0199"), "k"},
                        {ToUnichar("019A"), "l"},
                        {ToUnichar("019B"), "l"},
                        {ToUnichar("019C"), "M"},
                        {ToUnichar("019D"), "N"},
                        {ToUnichar("019E"), "n"},
                        {ToUnichar("019F"), "O"},
                        {ToUnichar("01A0"), "O"},
                        {ToUnichar("01A1"), "o"},
                        {ToUnichar("01A2"), "OI"},
                        {ToUnichar("01A3"), "oi"},
                        {ToUnichar("01A4"), "P"},
                        {ToUnichar("01A5"), "p"},
                        {ToUnichar("01A6"), "YR"},
                        {ToUnichar("01A7"), "2"},
                        {ToUnichar("01A8"), "2"},
                        {ToUnichar("01A9"), "S"},
                        {ToUnichar("01AA"), "s"},
                        {ToUnichar("01AB"), "t"},
                        {ToUnichar("01AC"), "T"},
                        {ToUnichar("01AD"), "t"},
                        {ToUnichar("01AE"), "T"},
                        {ToUnichar("01AF"), "U"},
                        {ToUnichar("01B0"), "u"},
                        {ToUnichar("01B1"), "u"},
                        {ToUnichar("01B2"), "V"},
                        {ToUnichar("01B3"), "Y"},
                        {ToUnichar("01B4"), "y"},
                        {ToUnichar("01B5"), "Z"},
                        {ToUnichar("01B6"), "z"},
                        {ToUnichar("01B7"), "Z"},
                        {ToUnichar("01B8"), "Z"},
                        {ToUnichar("01B9"), "Z"},
                        {ToUnichar("01BA"), "z"},
                        {ToUnichar("01BB"), "2"},
                        {ToUnichar("01BC"), "5"},
                        {ToUnichar("01BD"), "5"},
                        {ToUnichar("01BF"), "w"},
                        {ToUnichar("01C0"), "!"},
                        {ToUnichar("01C1"), "!"},
                        {ToUnichar("01C2"), "!"},
                        {ToUnichar("01C3"), "!"},
                        {ToUnichar("01C4"), "DZ"},
                        {ToUnichar("01C5"), "DZ"},
                        {ToUnichar("01C6"), "d"},
                        {ToUnichar("01C7"), "Lj"},
                        {ToUnichar("01C8"), "Lj"},
                        {ToUnichar("01C9"), "lj"},
                        {ToUnichar("01CA"), "NJ"},
                        {ToUnichar("01CB"), "NJ"},
                        {ToUnichar("01CC"), "nj"},
                        {ToUnichar("01CD"), "A"},
                        {ToUnichar("01CE"), "a"},
                        {ToUnichar("01CF"), "I"},
                        {ToUnichar("01D0"), "i"},
                        {ToUnichar("01D1"), "O"},
                        {ToUnichar("01D2"), "o"},
                        {ToUnichar("01D3"), "U"},
                        {ToUnichar("01D4"), "u"},
                        {ToUnichar("01D5"), "U"},
                        {ToUnichar("01D6"), "u"},
                        {ToUnichar("01D7"), "U"},
                        {ToUnichar("01D8"), "u"},
                        {ToUnichar("01D9"), "U"},
                        {ToUnichar("01DA"), "u"},
                        {ToUnichar("01DB"), "U"},
                        {ToUnichar("01DC"), "u"},
                        {ToUnichar("01DD"), "e"},
                        {ToUnichar("01DE"), "A"},
                        {ToUnichar("01DF"), "a"},
                        {ToUnichar("01E0"), "A"},
                        {ToUnichar("01E1"), "a"},
                        {ToUnichar("01E2"), "AE"},
                        {ToUnichar("01E3"), "ae"},
                        {ToUnichar("01E4"), "G"},
                        {ToUnichar("01E5"), "g"},
                        {ToUnichar("01E6"), "G"},
                        {ToUnichar("01E7"), "g"},
                        {ToUnichar("01E8"), "K"},
                        {ToUnichar("01E9"), "k"},
                        {ToUnichar("01EA"), "O"},
                        {ToUnichar("01EB"), "o"},
                        {ToUnichar("01EC"), "O"},
                        {ToUnichar("01ED"), "o"},
                        {ToUnichar("01EE"), "Z"},
                        {ToUnichar("01EF"), "Z"},
                        {ToUnichar("01F0"), "j"},
                        {ToUnichar("01F1"), "DZ"},
                        {ToUnichar("01F2"), "DZ"},
                        {ToUnichar("01F3"), "dz"},
                        {ToUnichar("01F4"), "G"},
                        {ToUnichar("01F5"), "g"},
                        {ToUnichar("01F6"), "hv"},
                        {ToUnichar("01F7"), "w"},
                        {ToUnichar("01F8"), "N"},
                        {ToUnichar("01F9"), "n"},
                        {ToUnichar("01FA"), "A"},
                        {ToUnichar("01FB"), "a"},
                        {ToUnichar("01FC"), "AE"},
                        {ToUnichar("01FD"), "ae"},
                        {ToUnichar("01FE"), "O"},
                        {ToUnichar("01FF"), "o"},
                        {ToUnichar("0200"), "A"},
                        {ToUnichar("0201"), "a"},
                        {ToUnichar("0202"), "A"},
                        {ToUnichar("0203"), "a"},
                        {ToUnichar("0204"), "E"},
                        {ToUnichar("0205"), "e"},
                        {ToUnichar("0206"), "E"},
                        {ToUnichar("0207"), "e"},
                        {ToUnichar("0208"), "I"},
                        {ToUnichar("0209"), "i"},
                        {ToUnichar("020A"), "I"},
                        {ToUnichar("020B"), "i"},
                        {ToUnichar("020C"), "O"},
                        {ToUnichar("020D"), "o"},
                        {ToUnichar("020E"), "O"},
                        {ToUnichar("020F"), "o"},
                        {ToUnichar("0210"), "R"},
                        {ToUnichar("0211"), "r"},
                        {ToUnichar("0212"), "R"},
                        {ToUnichar("0213"), "r"},
                        {ToUnichar("0214"), "U"},
                        {ToUnichar("0215"), "u"},
                        {ToUnichar("0216"), "U"},
                        {ToUnichar("0217"), "u"},
                        {ToUnichar("0218"), "S"},
                        {ToUnichar("0219"), "s"},
                        {ToUnichar("021A"), "T"},
                        {ToUnichar("021B"), "t"},
                        {ToUnichar("021C"), "Z"},
                        {ToUnichar("021D"), "z"},
                        {ToUnichar("021E"), "H"},
                        {ToUnichar("021F"), "h"},
                        {ToUnichar("0220"), "N"},
                        {ToUnichar("0221"), "d"},
                        {ToUnichar("0222"), "OU"},
                        {ToUnichar("0223"), "ou"},
                        {ToUnichar("0224"), "Z"},
                        {ToUnichar("0225"), "z"},
                        {ToUnichar("0226"), "A"},
                        {ToUnichar("0227"), "a"},
                        {ToUnichar("0228"), "E"},
                        {ToUnichar("0229"), "e"},
                        {ToUnichar("022A"), "O"},
                        {ToUnichar("022B"), "o"},
                        {ToUnichar("022C"), "O"},
                        {ToUnichar("022D"), "o"},
                        {ToUnichar("022E"), "O"},
                        {ToUnichar("022F"), "o"},
                        {ToUnichar("0230"), "O"},
                        {ToUnichar("0231"), "o"},
                        {ToUnichar("0232"), "Y"},
                        {ToUnichar("0233"), "y"},
                        {ToUnichar("0234"), "l"},
                        {ToUnichar("0235"), "n"},
                        {ToUnichar("0236"), "t"},
                        {ToUnichar("0250"), "a"},
                        {ToUnichar("0251"), "a"},
                        {ToUnichar("0252"), "a"},
                        {ToUnichar("0253"), "b"},
                        {ToUnichar("0254"), "o"},
                        {ToUnichar("0255"), "c"},
                        {ToUnichar("0256"), "d"},
                        {ToUnichar("0257"), "d"},
                        {ToUnichar("0258"), "e"},
                        {ToUnichar("0259"), "e"},
                        {ToUnichar("025A"), "e"},
                        {ToUnichar("025B"), "e"},
                        {ToUnichar("025C"), "e"},
                        {ToUnichar("025D"), "e"},
                        {ToUnichar("025E"), "e"},
                        {ToUnichar("025F"), "j"},
                        {ToUnichar("0260"), "g"},
                        {ToUnichar("0261"), "g"},
                        {ToUnichar("0262"), "G"},
                        {ToUnichar("0263"), "g"},
                        {ToUnichar("0264"), "y"},
                        {ToUnichar("0265"), "h"},
                        {ToUnichar("0266"), "h"},
                        {ToUnichar("0267"), "h"},
                        {ToUnichar("0268"), "i"},
                        {ToUnichar("0269"), "i"},
                        {ToUnichar("026A"), "I"},
                        {ToUnichar("026B"), "l"},
                        {ToUnichar("026C"), "l"},
                        {ToUnichar("026D"), "l"},
                        {ToUnichar("026E"), "lz"},
                        {ToUnichar("026F"), "m"},
                        {ToUnichar("0270"), "m"},
                        {ToUnichar("0271"), "m"},
                        {ToUnichar("0272"), "n"},
                        {ToUnichar("0273"), "n"},
                        {ToUnichar("0274"), "N"},
                        {ToUnichar("0275"), "o"},
                        {ToUnichar("0276"), "OE"},
                        {ToUnichar("0277"), "o"},
                        {ToUnichar("0278"), "ph"},
                        {ToUnichar("0279"), "r"},
                        {ToUnichar("027A"), "r"},
                        {ToUnichar("027B"), "r"},
                        {ToUnichar("027C"), "r"},
                        {ToUnichar("027D"), "r"},
                        {ToUnichar("027E"), "r"},
                        {ToUnichar("027F"), "r"},
                        {ToUnichar("0280"), "R"},
                        {ToUnichar("0281"), "r"},
                        {ToUnichar("0282"), "s"},
                        {ToUnichar("0283"), "s"},
                        {ToUnichar("0284"), "j"},
                        {ToUnichar("0285"), "s"},
                        {ToUnichar("0286"), "s"},
                        {ToUnichar("0287"), "y"},
                        {ToUnichar("0288"), "t"},
                        {ToUnichar("0289"), "u"},
                        {ToUnichar("028A"), "u"},
                        {ToUnichar("028B"), "u"},
                        {ToUnichar("028C"), "v"},
                        {ToUnichar("028D"), "w"},
                        {ToUnichar("028E"), "y"},
                        {ToUnichar("028F"), "Y"},
                        {ToUnichar("0290"), "z"},
                        {ToUnichar("0291"), "z"},
                        {ToUnichar("0292"), "z"},
                        {ToUnichar("0293"), "z"},
                        {ToUnichar("0294"), "'"},
                        {ToUnichar("0295"), "'"},
                        {ToUnichar("0296"), "'"},
                        {ToUnichar("0297"), "C"},
                        {ToUnichar("0299"), "B"},
                        {ToUnichar("029A"), "e"},
                        {ToUnichar("029B"), "G"},
                        {ToUnichar("029C"), "H"},
                        {ToUnichar("029D"), "j"},
                        {ToUnichar("029E"), "k"},
                        {ToUnichar("029F"), "L"},
                        {ToUnichar("02A0"), "q"},
                        {ToUnichar("02A1"), "'"},
                        {ToUnichar("02A2"), "'"},
                        {ToUnichar("02A3"), "dz"},
                        {ToUnichar("02A4"), "dz"},
                        {ToUnichar("02A5"), "dz"},
                        {ToUnichar("02A6"), "ts"},
                        {ToUnichar("02A7"), "ts"},
                        {ToUnichar("02A8"), ""},
                        {ToUnichar("02A9"), "fn"},
                        {ToUnichar("02AA"), "ls"},
                        {ToUnichar("02AB"), "lz"},
                        {ToUnichar("02AC"), "w"},
                        {ToUnichar("02AD"), "t"},
                        {ToUnichar("02AE"), "h"},
                        {ToUnichar("02AF"), "h"},
                        {ToUnichar("02B0"), "h"},
                        {ToUnichar("02B1"), "h"},
                        {ToUnichar("02B2"), "j"},
                        {ToUnichar("02B3"), "r"},
                        {ToUnichar("02B4"), "r"},
                        {ToUnichar("02B5"), "r"},
                        {ToUnichar("02B6"), "R"},
                        {ToUnichar("02B7"), "w"},
                        {ToUnichar("02B8"), "y"},
                        {ToUnichar("02E1"), "l"},
                        {ToUnichar("02E2"), "s"},
                        {ToUnichar("02E3"), "x"},
                        {ToUnichar("02E4"), "'"},
                        {ToUnichar("1D00"), "A"},
                        {ToUnichar("1D01"), "AE"},
                        {ToUnichar("1D02"), "ae"},
                        {ToUnichar("1D03"), "B"},
                        {ToUnichar("1D04"), "C"},
                        {ToUnichar("1D05"), "D"},
                        {ToUnichar("1D06"), "TH"},
                        {ToUnichar("1D07"), "E"},
                        {ToUnichar("1D08"), "e"},
                        {ToUnichar("1D09"), "i"},
                        {ToUnichar("1D0A"), "J"},
                        {ToUnichar("1D0B"), "K"},
                        {ToUnichar("1D0C"), "L"},
                        {ToUnichar("1D0D"), "M"},
                        {ToUnichar("1D0E"), "N"},
                        {ToUnichar("1D0F"), "O"},
                        {ToUnichar("1D10"), "O"},
                        {ToUnichar("1D11"), "o"},
                        {ToUnichar("1D12"), "o"},
                        {ToUnichar("1D13"), "o"},
                        {ToUnichar("1D14"), "oe"},
                        {ToUnichar("1D15"), "ou"},
                        {ToUnichar("1D16"), "o"},
                        {ToUnichar("1D17"), "o"},
                        {ToUnichar("1D18"), "P"},
                        {ToUnichar("1D19"), "R"},
                        {ToUnichar("1D1A"), "R"},
                        {ToUnichar("1D1B"), "T"},
                        {ToUnichar("1D1C"), "U"},
                        {ToUnichar("1D1D"), "u"},
                        {ToUnichar("1D1E"), "u"},
                        {ToUnichar("1D1F"), "m"},
                        {ToUnichar("1D20"), "V"},
                        {ToUnichar("1D21"), "W"},
                        {ToUnichar("1D22"), "Z"},
                        {ToUnichar("1D23"), "EZH"},
                        {ToUnichar("1D24"), "'"},
                        {ToUnichar("1D25"), "L"},
                        {ToUnichar("1D2C"), "A"},
                        {ToUnichar("1D2D"), "AE"},
                        {ToUnichar("1D2E"), "B"},
                        {ToUnichar("1D2F"), "B"},
                        {ToUnichar("1D30"), "D"},
                        {ToUnichar("1D31"), "E"},
                        {ToUnichar("1D32"), "E"},
                        {ToUnichar("1D33"), "G"},
                        {ToUnichar("1D34"), "H"},
                        {ToUnichar("1D35"), "I"},
                        {ToUnichar("1D36"), "J"},
                        {ToUnichar("1D37"), "K"},
                        {ToUnichar("1D38"), "L"},
                        {ToUnichar("1D39"), "M"},
                        {ToUnichar("1D3A"), "N"},
                        {ToUnichar("1D3B"), "N"},
                        {ToUnichar("1D3C"), "O"},
                        {ToUnichar("1D3D"), "OU"},
                        {ToUnichar("1D3E"), "P"},
                        {ToUnichar("1D3F"), "R"},
                        {ToUnichar("1D40"), "T"},
                        {ToUnichar("1D41"), "U"},
                        {ToUnichar("1D42"), "W"},
                        {ToUnichar("1D43"), "a"},
                        {ToUnichar("1D44"), "a"},
                        {ToUnichar("1D46"), "ae"},
                        {ToUnichar("1D47"), "b"},
                        {ToUnichar("1D48"), "d"},
                        {ToUnichar("1D49"), "e"},
                        {ToUnichar("1D4A"), "e"},
                        {ToUnichar("1D4B"), "e"},
                        {ToUnichar("1D4C"), "e"},
                        {ToUnichar("1D4D"), "g"},
                        {ToUnichar("1D4E"), "i"},
                        {ToUnichar("1D4F"), "k"},
                        {ToUnichar("1D50"), "m"},
                        {ToUnichar("1D51"), "g"},
                        {ToUnichar("1D52"), "o"},
                        {ToUnichar("1D53"), "o"},
                        {ToUnichar("1D54"), "o"},
                        {ToUnichar("1D55"), "o"},
                        {ToUnichar("1D56"), "p"},
                        {ToUnichar("1D57"), "t"},
                        {ToUnichar("1D58"), "u"},
                        {ToUnichar("1D59"), "u"},
                        {ToUnichar("1D5A"), "m"},
                        {ToUnichar("1D5B"), "v"},
                        {ToUnichar("1D62"), "i"},
                        {ToUnichar("1D63"), "r"},
                        {ToUnichar("1D64"), "u"},
                        {ToUnichar("1D65"), "v"},
                        {ToUnichar("1D6B"), "ue"},
                        {ToUnichar("1E00"), "A"},
                        {ToUnichar("1E01"), "a"},
                        {ToUnichar("1E02"), "B"},
                        {ToUnichar("1E03"), "b"},
                        {ToUnichar("1E04"), "B"},
                        {ToUnichar("1E05"), "b"},
                        {ToUnichar("1E06"), "B"},
                        {ToUnichar("1E07"), "b"},
                        {ToUnichar("1E08"), "C"},
                        {ToUnichar("1E09"), "c"},
                        {ToUnichar("1E0A"), "D"},
                        {ToUnichar("1E0B"), "d"},
                        {ToUnichar("1E0C"), "D"},
                        {ToUnichar("1E0D"), "d"},
                        {ToUnichar("1E0E"), "D"},
                        {ToUnichar("1E0F"), "d"},
                        {ToUnichar("1E10"), "D"},
                        {ToUnichar("1E11"), "d"},
                        {ToUnichar("1E12"), "D"},
                        {ToUnichar("1E13"), "d"},
                        {ToUnichar("1E14"), "E"},
                        {ToUnichar("1E15"), "e"},
                        {ToUnichar("1E16"), "E"},
                        {ToUnichar("1E17"), "e"},
                        {ToUnichar("1E18"), "E"},
                        {ToUnichar("1E19"), "e"},
                        {ToUnichar("1E1A"), "E"},
                        {ToUnichar("1E1B"), "e"},
                        {ToUnichar("1E1C"), "E"},
                        {ToUnichar("1E1D"), "e"},
                        {ToUnichar("1E1E"), "F"},
                        {ToUnichar("1E1F"), "f"},
                        {ToUnichar("1E20"), "G"},
                        {ToUnichar("1E21"), "g"},
                        {ToUnichar("1E22"), "H"},
                        {ToUnichar("1E23"), "h"},
                        {ToUnichar("1E24"), "H"},
                        {ToUnichar("1E25"), "h"},
                        {ToUnichar("1E26"), "H"},
                        {ToUnichar("1E27"), "h"},
                        {ToUnichar("1E28"), "H"},
                        {ToUnichar("1E29"), "h"},
                        {ToUnichar("1E2A"), "H"},
                        {ToUnichar("1E2B"), "h"},
                        {ToUnichar("1E2C"), "I"},
                        {ToUnichar("1E2D"), "i"},
                        {ToUnichar("1E2E"), "I"},
                        {ToUnichar("1E2F"), "i"},
                        {ToUnichar("1E30"), "K"},
                        {ToUnichar("1E31"), "k"},
                        {ToUnichar("1E32"), "K"},
                        {ToUnichar("1E33"), "k"},
                        {ToUnichar("1E34"), "K"},
                        {ToUnichar("1E35"), "k"},
                        {ToUnichar("1E36"), "L"},
                        {ToUnichar("1E37"), "l"},
                        {ToUnichar("1E38"), "L"},
                        {ToUnichar("1E39"), "l"},
                        {ToUnichar("1E3A"), "L"},
                        {ToUnichar("1E3B"), "l"},
                        {ToUnichar("1E3C"), "L"},
                        {ToUnichar("1E3D"), "l"},
                        {ToUnichar("1E3E"), "M"},
                        {ToUnichar("1E3F"), "m"},
                        {ToUnichar("1E40"), "M"},
                        {ToUnichar("1E41"), "m"},
                        {ToUnichar("1E42"), "M"},
                        {ToUnichar("1E43"), "m"},
                        {ToUnichar("1E44"), "N"},
                        {ToUnichar("1E45"), "n"},
                        {ToUnichar("1E46"), "N"},
                        {ToUnichar("1E47"), "n"},
                        {ToUnichar("1E48"), "N"},
                        {ToUnichar("1E49"), "n"},
                        {ToUnichar("1E4A"), "N"},
                        {ToUnichar("1E4B"), "n"},
                        {ToUnichar("1E4C"), "O"},
                        {ToUnichar("1E4D"), "o"},
                        {ToUnichar("1E4E"), "O"},
                        {ToUnichar("1E4F"), "o"},
                        {ToUnichar("1E50"), "O"},
                        {ToUnichar("1E51"), "o"},
                        {ToUnichar("1E52"), "O"},
                        {ToUnichar("1E53"), "o"},
                        {ToUnichar("1E54"), "P"},
                        {ToUnichar("1E55"), "p"},
                        {ToUnichar("1E56"), "P"},
                        {ToUnichar("1E57"), "p"},
                        {ToUnichar("1E58"), "R"},
                        {ToUnichar("1E59"), "r"},
                        {ToUnichar("1E5A"), "R"},
                        {ToUnichar("1E5B"), "r"},
                        {ToUnichar("1E5C"), "R"},
                        {ToUnichar("1E5D"), "r"},
                        {ToUnichar("1E5E"), "R"},
                        {ToUnichar("1E5F"), "r"},
                        {ToUnichar("1E60"), "S"},
                        {ToUnichar("1E61"), "s"},
                        {ToUnichar("1E62"), "S"},
                        {ToUnichar("1E63"), "s"},
                        {ToUnichar("1E64"), "S"},
                        {ToUnichar("1E65"), "s"},
                        {ToUnichar("1E66"), "S"},
                        {ToUnichar("1E67"), "s"},
                        {ToUnichar("1E68"), "S"},
                        {ToUnichar("1E69"), "s"},
                        {ToUnichar("1E6A"), "T"},
                        {ToUnichar("1E6B"), "t"},
                        {ToUnichar("1E6C"), "T"},
                        {ToUnichar("1E6D"), "t"},
                        {ToUnichar("1E6E"), "T"},
                        {ToUnichar("1E6F"), "t"},
                        {ToUnichar("1E70"), "T"},
                        {ToUnichar("1E71"), "t"},
                        {ToUnichar("1E72"), "U"},
                        {ToUnichar("1E73"), "u"},
                        {ToUnichar("1E74"), "U"},
                        {ToUnichar("1E75"), "u"},
                        {ToUnichar("1E76"), "U"},
                        {ToUnichar("1E77"), "u"},
                        {ToUnichar("1E78"), "U"},
                        {ToUnichar("1E79"), "u"},
                        {ToUnichar("1E7A"), "U"},
                        {ToUnichar("1E7B"), "u"},
                        {ToUnichar("1E7C"), "V"},
                        {ToUnichar("1E7D"), "v"},
                        {ToUnichar("1E7E"), "V"},
                        {ToUnichar("1E7F"), "v"},
                        {ToUnichar("1E80"), "W"},
                        {ToUnichar("1E81"), "w"},
                        {ToUnichar("1E82"), "W"},
                        {ToUnichar("1E83"), "w"},
                        {ToUnichar("1E84"), "W"},
                        {ToUnichar("1E85"), "w"},
                        {ToUnichar("1E86"), "W"},
                        {ToUnichar("1E87"), "w"},
                        {ToUnichar("1E88"), "W"},
                        {ToUnichar("1E89"), "w"},
                        {ToUnichar("1E8A"), "X"},
                        {ToUnichar("1E8B"), "x"},
                        {ToUnichar("1E8C"), "X"},
                        {ToUnichar("1E8D"), "x"},
                        {ToUnichar("1E8E"), "Y"},
                        {ToUnichar("1E8F"), "y"},
                        {ToUnichar("1E90"), "Z"},
                        {ToUnichar("1E91"), "z"},
                        {ToUnichar("1E92"), "Z"},
                        {ToUnichar("1E93"), "z"},
                        {ToUnichar("1E94"), "Z"},
                        {ToUnichar("1E95"), "z"},
                        {ToUnichar("1E96"), "h"},
                        {ToUnichar("1E97"), "t"},
                        {ToUnichar("1E98"), "w"},
                        {ToUnichar("1E99"), "y"},
                        {ToUnichar("1E9A"), "a"},
                        {ToUnichar("1E9B"), "s"},
                        {ToUnichar("1EA0"), "A"},
                        {ToUnichar("1EA1"), "a"},
                        {ToUnichar("1EA2"), "A"},
                        {ToUnichar("1EA3"), "a"},
                        {ToUnichar("1EA4"), "A"},
                        {ToUnichar("1EA5"), "a"},
                        {ToUnichar("1EA6"), "A"},
                        {ToUnichar("1EA7"), "a"},
                        {ToUnichar("1EA8"), "A"},
                        {ToUnichar("1EA9"), "a"},
                        {ToUnichar("1EAA"), "A"},
                        {ToUnichar("1EAB"), "a"},
                        {ToUnichar("1EAC"), "A"},
                        {ToUnichar("1EAD"), "a"},
                        {ToUnichar("1EAE"), "A"},
                        {ToUnichar("1EAF"), "a"},
                        {ToUnichar("1EB0"), "A"},
                        {ToUnichar("1EB1"), "a"},
                        {ToUnichar("1EB2"), "A"},
                        {ToUnichar("1EB3"), "a"},
                        {ToUnichar("1EB4"), "A"},
                        {ToUnichar("1EB5"), "a"},
                        {ToUnichar("1EB6"), "A"},
                        {ToUnichar("1EB7"), "a"},
                        {ToUnichar("1EB8"), "E"},
                        {ToUnichar("1EB9"), "e"},
                        {ToUnichar("1EBA"), "E"},
                        {ToUnichar("1EBB"), "e"},
                        {ToUnichar("1EBC"), "E"},
                        {ToUnichar("1EBD"), "e"},
                        {ToUnichar("1EBE"), "E"},
                        {ToUnichar("1EBF"), "e"},
                        {ToUnichar("1EC0"), "E"},
                        {ToUnichar("1EC1"), "e"},
                        {ToUnichar("1EC2"), "E"},
                        {ToUnichar("1EC3"), "e"},
                        {ToUnichar("1EC4"), "E"},
                        {ToUnichar("1EC5"), "e"},
                        {ToUnichar("1EC6"), "E"},
                        {ToUnichar("1EC7"), "e"},
                        {ToUnichar("1EC8"), "I"},
                        {ToUnichar("1EC9"), "i"},
                        {ToUnichar("1ECA"), "I"},
                        {ToUnichar("1ECB"), "i"},
                        {ToUnichar("1ECC"), "O"},
                        {ToUnichar("1ECD"), "o"},
                        {ToUnichar("1ECE"), "O"},
                        {ToUnichar("1ECF"), "o"},
                        {ToUnichar("1ED0"), "O"},
                        {ToUnichar("1ED1"), "o"},
                        {ToUnichar("1ED2"), "O"},
                        {ToUnichar("1ED3"), "o"},
                        {ToUnichar("1ED4"), "O"},
                        {ToUnichar("1ED5"), "o"},
                        {ToUnichar("1ED6"), "O"},
                        {ToUnichar("1ED7"), "o"},
                        {ToUnichar("1ED8"), "O"},
                        {ToUnichar("1ED9"), "o"},
                        {ToUnichar("1EDA"), "O"},
                        {ToUnichar("1EDB"), "o"},
                        {ToUnichar("1EDC"), "O"},
                        {ToUnichar("1EDD"), "o"},
                        {ToUnichar("1EDE"), "O"},
                        {ToUnichar("1EDF"), "o"},
                        {ToUnichar("1EE0"), "O"},
                        {ToUnichar("1EE1"), "o"},
                        {ToUnichar("1EE2"), "O"},
                        {ToUnichar("1EE3"), "o"},
                        {ToUnichar("1EE4"), "U"},
                        {ToUnichar("1EE5"), "u"},
                        {ToUnichar("1EE6"), "U"},
                        {ToUnichar("1EE7"), "u"},
                        {ToUnichar("1EE8"), "U"},
                        {ToUnichar("1EE9"), "u"},
                        {ToUnichar("1EEA"), "U"},
                        {ToUnichar("1EEB"), "u"},
                        {ToUnichar("1EEC"), "U"},
                        {ToUnichar("1EED"), "u"},
                        {ToUnichar("1EEE"), "U"},
                        {ToUnichar("1EEF"), "u"},
                        {ToUnichar("1EF0"), "U"},
                        {ToUnichar("1EF1"), "u"},
                        {ToUnichar("1EF2"), "Y"},
                        {ToUnichar("1EF3"), "y"},
                        {ToUnichar("1EF4"), "Y"},
                        {ToUnichar("1EF5"), "y"},
                        {ToUnichar("1EF6"), "Y"},
                        {ToUnichar("1EF7"), "y"},
                        {ToUnichar("1EF8"), "Y"},
                        {ToUnichar("1EF9"), "y"},
                        {ToUnichar("2071"), "i"},
                        {ToUnichar("207F"), "n"},
                        {ToUnichar("212A"), "K"},
                        {ToUnichar("212B"), "A"},
                        {ToUnichar("212C"), "B"},
                        {ToUnichar("212D"), "C"},
                        {ToUnichar("212F"), "e"},
                        {ToUnichar("2130"), "E"},
                        {ToUnichar("2131"), "F"},
                        {ToUnichar("2132"), "F"},
                        {ToUnichar("2133"), "M"},
                        {ToUnichar("2134"), "0"},
                        {ToUnichar("213A"), "0"},
                        {ToUnichar("2141"), "G"},
                        {ToUnichar("2142"), "L"},
                        {ToUnichar("2143"), "L"},
                        {ToUnichar("2144"), "Y"},
                        {ToUnichar("2145"), "D"},
                        {ToUnichar("2146"), "d"},
                        {ToUnichar("2147"), "e"},
                        {ToUnichar("2148"), "i"},
                        {ToUnichar("2149"), "j"},
                        {ToUnichar("FB00"), "ff"},
                        {ToUnichar("FB01"), "fi"},
                        {ToUnichar("FB02"), "fl"},
                        {ToUnichar("FB03"), "ffi"},
                        {ToUnichar("FB04"), "ffl"},
                        {ToUnichar("FB05"), "st"},
                        {ToUnichar("FB06"), "st"},
                        {ToUnichar("FF21"), "A"},
                        {ToUnichar("FF22"), "B"},
                        {ToUnichar("FF23"), "C"},
                        {ToUnichar("FF24"), "D"},
                        {ToUnichar("FF25"), "E"},
                        {ToUnichar("FF26"), "F"},
                        {ToUnichar("FF27"), "G"},
                        {ToUnichar("FF28"), "H"},
                        {ToUnichar("FF29"), "I"},
                        {ToUnichar("FF2A"), "J"},
                        {ToUnichar("FF2B"), "K"},
                        {ToUnichar("FF2C"), "L"},
                        {ToUnichar("FF2D"), "M"},
                        {ToUnichar("FF2E"), "N"},
                        {ToUnichar("FF2F"), "O"},
                        {ToUnichar("FF30"), "P"},
                        {ToUnichar("FF31"), "Q"},
                        {ToUnichar("FF32"), "R"},
                        {ToUnichar("FF33"), "S"},
                        {ToUnichar("FF34"), "T"},
                        {ToUnichar("FF35"), "U"},
                        {ToUnichar("FF36"), "V"},
                        {ToUnichar("FF37"), "W"},
                        {ToUnichar("FF38"), "X"},
                        {ToUnichar("FF39"), "Y"},
                        {ToUnichar("FF3A"), "Z"},
                        {ToUnichar("FF41"), "a"},
                        {ToUnichar("FF42"), "b"},
                        {ToUnichar("FF43"), "c"},
                        {ToUnichar("FF44"), "d"},
                        {ToUnichar("FF45"), "e"},
                        {ToUnichar("FF46"), "f"},
                        {ToUnichar("FF47"), "g"},
                        {ToUnichar("FF48"), "h"},
                        {ToUnichar("FF49"), "i"},
                        {ToUnichar("FF4A"), "j"},
                        {ToUnichar("FF4B"), "k"},
                        {ToUnichar("FF4C"), "l"},
                        {ToUnichar("FF4D"), "m"},
                        {ToUnichar("FF4E"), "n"},
                        {ToUnichar("FF4F"), "o"},
                        {ToUnichar("FF50"), "p"},
                        {ToUnichar("FF51"), "q"},
                        {ToUnichar("FF52"), "r"},
                        {ToUnichar("FF53"), "s"},
                        {ToUnichar("FF54"), "t"},
                        {ToUnichar("FF55"), "u"},
                        {ToUnichar("FF56"), "v"},
                        {ToUnichar("FF57"), "w"},
                        {ToUnichar("FF58"), "x"},
                        {ToUnichar("FF59"), "y"},
                        {ToUnichar("FF5A"), "z"},
                        {ToUnichar("0410"), "A"},
                        {ToUnichar("0411"), "B"},
                        {ToUnichar("0412"), "V"},
                        {ToUnichar("0413"), "G"},
                        {ToUnichar("0414"), "D"},
                        {ToUnichar("0415"), "E"},
                        {ToUnichar("0401"), "YO"},
                        {ToUnichar("0416"), "ZH"},
                        {ToUnichar("0417"), "Z"},
                        {ToUnichar("0418"), "I"},
                        {ToUnichar("0419"), "J"},
                        {ToUnichar("041A"), "K"},
                        {ToUnichar("041B"), "L"},
                        {ToUnichar("041C"), "M"},
                        {ToUnichar("041D"), "N"},
                        {ToUnichar("041E"), "O"},
                        {ToUnichar("041F"), "P"},
                        {ToUnichar("0420"), "R"},
                        {ToUnichar("0421"), "S"},
                        {ToUnichar("0422"), "T"},
                        {ToUnichar("0423"), "U"},
                        {ToUnichar("0424"), "F"},
                        {ToUnichar("0425"), "H"},
                        {ToUnichar("0426"), "C"},
                        {ToUnichar("0427"), "CH"},
                        {ToUnichar("0428"), "SH"},
                        {ToUnichar("0429"), "SHH"},
                        {ToUnichar("042A"), ""},
                        {ToUnichar("042B"), "Y"},
                        {ToUnichar("042C"), ""},
                        {ToUnichar("042D"), "E"},
                        {ToUnichar("042E"), "YU"},
                        {ToUnichar("042F"), "YA"},
                        {ToUnichar("0430"), "a"},
                        {ToUnichar("0431"), "b"},
                        {ToUnichar("0432"), "v"},
                        {ToUnichar("0433"), "g"},
                        {ToUnichar("0434"), "d"},
                        {ToUnichar("0435"), "e"},
                        {ToUnichar("0451"), "yo"},
                        {ToUnichar("0436"), "zh"},
                        {ToUnichar("0437"), "z"},
                        {ToUnichar("0438"), "i"},
                        {ToUnichar("0439"), "j"},
                        {ToUnichar("043A"), "k"},
                        {ToUnichar("043B"), "l"},
                        {ToUnichar("043C"), "m"},
                        {ToUnichar("043D"), "n"},
                        {ToUnichar("043E"), "o"},
                        {ToUnichar("043F"), "p"},
                        {ToUnichar("0440"), "r"},
                        {ToUnichar("0441"), "s"},
                        {ToUnichar("0442"), "t"},
                        {ToUnichar("0443"), "u"},
                        {ToUnichar("0444"), "f"},
                        {ToUnichar("0445"), "h"},
                        {ToUnichar("0446"), "c"},
                        {ToUnichar("0447"), "ch"},
                        {ToUnichar("0448"), "sh"},
                        {ToUnichar("0449"), "shh"},
                        {ToUnichar("044A"), ""},
                        {ToUnichar("044B"), "y"},
                        {ToUnichar("044C"), ""},
                        {ToUnichar("044D"), "e"},
                        {ToUnichar("044E"), "yu"},
                        {ToUnichar("044F"), "ya"},
                        {ToUnichar("0406"), "I"},
                        {ToUnichar("0456"), "i"},
                        {ToUnichar("0407"), "I"},
                        {ToUnichar("0457"), "i"},
                        {ToUnichar("0404"), "Ie"},
                        {ToUnichar("0454"), "ie"},
                        {ToUnichar("0490"), "G"},
                        {ToUnichar("0491"), "g"},
                        {ToUnichar("040E"), "U"},
                        {ToUnichar("045E"), "u"}
                    };
                    // FEMININE ORDINAL INDICATOR
                    // MASCULINE ORDINAL INDICATOR
                    // LATIN CAPITAL LETTER A WITH GRAVE
                    // LATIN CAPITAL LETTER A WITH ACUTE
                    // LATIN CAPITAL LETTER A WITH CIRCUMFLEX
                    // LATIN CAPITAL LETTER A WITH TILDE
                    // LATIN CAPITAL LETTER A WITH DIAERESIS
                    // LATIN CAPITAL LETTER A WITH RING ABOVE
                    // LATIN CAPITAL LETTER AE -- no decomposition
                    // LATIN CAPITAL LETTER C WITH CEDILLA
                    // LATIN CAPITAL LETTER E WITH GRAVE
                    // LATIN CAPITAL LETTER E WITH ACUTE
                    // LATIN CAPITAL LETTER E WITH CIRCUMFLEX
                    // LATIN CAPITAL LETTER E WITH DIAERESIS
                    // LATIN CAPITAL LETTER I WITH GRAVE
                    // LATIN CAPITAL LETTER I WITH ACUTE
                    // LATIN CAPITAL LETTER I WITH CIRCUMFLEX
                    // LATIN CAPITAL LETTER I WITH DIAERESIS
                    // LATIN CAPITAL LETTER ETH -- no decomposition  	// Eth [D for Vietnamese]
                    // LATIN CAPITAL LETTER N WITH TILDE
                    // LATIN CAPITAL LETTER O WITH GRAVE
                    // LATIN CAPITAL LETTER O WITH ACUTE
                    // LATIN CAPITAL LETTER O WITH CIRCUMFLEX
                    // LATIN CAPITAL LETTER O WITH TILDE
                    // LATIN CAPITAL LETTER O WITH DIAERESIS
                    // LATIN CAPITAL LETTER O WITH STROKE -- no decom
                    // LATIN CAPITAL LETTER U WITH GRAVE
                    // LATIN CAPITAL LETTER U WITH ACUTE
                    // LATIN CAPITAL LETTER U WITH CIRCUMFLEX
                    // LATIN CAPITAL LETTER U WITH DIAERESIS
                    // LATIN CAPITAL LETTER Y WITH ACUTE
                    // LATIN CAPITAL LETTER THORN -- no decomposition; // Thorn - Could be nothing other than thorn
                    // LATIN SMALL LETTER SHARP S -- no decomposition
                    // LATIN SMALL LETTER A WITH GRAVE
                    // LATIN SMALL LETTER A WITH ACUTE
                    // LATIN SMALL LETTER A WITH CIRCUMFLEX
                    // LATIN SMALL LETTER A WITH TILDE
                    // LATIN SMALL LETTER A WITH DIAERESIS
                    // LATIN SMALL LETTER A WITH RING ABOVE
                    // LATIN SMALL LETTER AE -- no decomposition
                    // LATIN SMALL LETTER C WITH CEDILLA
                    // LATIN SMALL LETTER E WITH GRAVE
                    // LATIN SMALL LETTER E WITH ACUTE
                    // LATIN SMALL LETTER E WITH CIRCUMFLEX
                    // LATIN SMALL LETTER E WITH DIAERESIS
                    // LATIN SMALL LETTER I WITH GRAVE
                    // LATIN SMALL LETTER I WITH ACUTE
                    // LATIN SMALL LETTER I WITH CIRCUMFLEX
                    // LATIN SMALL LETTER I WITH DIAERESIS
                    // LATIN SMALL LETTER ETH -- no decomposition         // small eth, "d" for benefit of Vietnamese
                    // LATIN SMALL LETTER N WITH TILDE
                    // LATIN SMALL LETTER O WITH GRAVE
                    // LATIN SMALL LETTER O WITH ACUTE
                    // LATIN SMALL LETTER O WITH CIRCUMFLEX
                    // LATIN SMALL LETTER O WITH TILDE
                    // LATIN SMALL LETTER O WITH DIAERESIS
                    // LATIN SMALL LETTER O WITH STROKE -- no decompo
                    // LATIN SMALL LETTER U WITH GRAVE
                    // LATIN SMALL LETTER U WITH ACUTE
                    // LATIN SMALL LETTER U WITH CIRCUMFLEX
                    // LATIN SMALL LETTER U WITH DIAERESIS
                    // LATIN SMALL LETTER Y WITH ACUTE
                    // LATIN SMALL LETTER THORN -- no decomposition  // Small thorn
                    // LATIN SMALL LETTER Y WITH DIAERESIS
                    // LATIN CAPITAL LETTER A WITH MACRON
                    // LATIN SMALL LETTER A WITH MACRON
                    // LATIN CAPITAL LETTER A WITH BREVE
                    // LATIN SMALL LETTER A WITH BREVE
                    // LATIN CAPITAL LETTER A WITH OGONEK
                    // LATIN SMALL LETTER A WITH OGONEK
                    // LATIN CAPITAL LETTER C WITH ACUTE
                    // LATIN SMALL LETTER C WITH ACUTE
                    // LATIN CAPITAL LETTER C WITH CIRCUMFLEX
                    // LATIN SMALL LETTER C WITH CIRCUMFLEX
                    // LATIN CAPITAL LETTER C WITH DOT ABOVE
                    // LATIN SMALL LETTER C WITH DOT ABOVE
                    // LATIN CAPITAL LETTER C WITH CARON
                    // LATIN SMALL LETTER C WITH CARON
                    // LATIN CAPITAL LETTER D WITH CARON
                    // LATIN SMALL LETTER D WITH CARON
                    // LATIN CAPITAL LETTER D WITH STROKE -- no decomposition                     // Capital D with stroke
                    // LATIN SMALL LETTER D WITH STROKE -- no decomposition                       // small D with stroke
                    // LATIN CAPITAL LETTER E WITH MACRON
                    // LATIN SMALL LETTER E WITH MACRON
                    // LATIN CAPITAL LETTER E WITH BREVE
                    // LATIN SMALL LETTER E WITH BREVE
                    // LATIN CAPITAL LETTER E WITH DOT ABOVE
                    // LATIN SMALL LETTER E WITH DOT ABOVE
                    // LATIN CAPITAL LETTER E WITH OGONEK
                    // LATIN SMALL LETTER E WITH OGONEK
                    // LATIN CAPITAL LETTER E WITH CARON
                    // LATIN SMALL LETTER E WITH CARON
                    // LATIN CAPITAL LETTER G WITH CIRCUMFLEX
                    // LATIN SMALL LETTER G WITH CIRCUMFLEX
                    // LATIN CAPITAL LETTER G WITH BREVE
                    // LATIN SMALL LETTER G WITH BREVE
                    // LATIN CAPITAL LETTER G WITH DOT ABOVE
                    // LATIN SMALL LETTER G WITH DOT ABOVE
                    // LATIN CAPITAL LETTER G WITH CEDILLA
                    // LATIN SMALL LETTER G WITH CEDILLA
                    // LATIN CAPITAL LETTER H WITH CIRCUMFLEX
                    // LATIN SMALL LETTER H WITH CIRCUMFLEX
                    // LATIN CAPITAL LETTER H WITH STROKE -- no decomposition
                    // LATIN SMALL LETTER H WITH STROKE -- no decomposition
                    // LATIN CAPITAL LETTER I WITH TILDE
                    // LATIN SMALL LETTER I WITH TILDE
                    // LATIN CAPITAL LETTER I WITH MACRON
                    // LATIN SMALL LETTER I WITH MACRON
                    // LATIN CAPITAL LETTER I WITH BREVE
                    // LATIN SMALL LETTER I WITH BREVE
                    // LATIN CAPITAL LETTER I WITH OGONEK
                    // LATIN SMALL LETTER I WITH OGONEK
                    // LATIN CAPITAL LETTER I WITH DOT ABOVE
                    // LATIN SMALL LETTER DOTLESS I -- no decomposition
                    // LATIN CAPITAL LIGATURE IJ    
                    // LATIN SMALL LIGATURE IJ      
                    // LATIN CAPITAL LETTER J WITH CIRCUMFLEX
                    // LATIN SMALL LETTER J WITH CIRCUMFLEX
                    // LATIN CAPITAL LETTER K WITH CEDILLA
                    // LATIN SMALL LETTER K WITH CEDILLA
                    // LATIN SMALL LETTER KRA -- no decomposition
                    // LATIN CAPITAL LETTER L WITH ACUTE
                    // LATIN SMALL LETTER L WITH ACUTE
                    // LATIN CAPITAL LETTER L WITH CEDILLA
                    // LATIN SMALL LETTER L WITH CEDILLA
                    // LATIN CAPITAL LETTER L WITH CARON
                    // LATIN SMALL LETTER L WITH CARON
                    // LATIN CAPITAL LETTER L WITH MIDDLE DOT
                    // LATIN SMALL LETTER L WITH MIDDLE DOT
                    // LATIN CAPITAL LETTER L WITH STROKE -- no decomposition
                    // LATIN SMALL LETTER L WITH STROKE -- no decomposition
                    // LATIN CAPITAL LETTER N WITH ACUTE
                    // LATIN SMALL LETTER N WITH ACUTE
                    // LATIN CAPITAL LETTER N WITH CEDILLA
                    // LATIN SMALL LETTER N WITH CEDILLA
                    // LATIN CAPITAL LETTER N WITH CARON
                    // LATIN SMALL LETTER N WITH CARON
                    // LATIN SMALL LETTER N PRECEDED BY APOSTROPHE                              ;
                    // LATIN CAPITAL LETTER ENG -- no decomposition                             ;
                    // LATIN SMALL LETTER ENG -- no decomposition                               ;
                    // LATIN CAPITAL LETTER O WITH MACRON
                    // LATIN SMALL LETTER O WITH MACRON
                    // LATIN CAPITAL LETTER O WITH BREVE
                    // LATIN SMALL LETTER O WITH BREVE
                    // LATIN CAPITAL LETTER O WITH DOUBLE ACUTE
                    // LATIN SMALL LETTER O WITH DOUBLE ACUTE
                    // LATIN CAPITAL LIGATURE OE -- no decomposition
                    // LATIN SMALL LIGATURE OE -- no decomposition
                    // LATIN CAPITAL LETTER R WITH ACUTE
                    // LATIN SMALL LETTER R WITH ACUTE
                    // LATIN CAPITAL LETTER R WITH CEDILLA
                    // LATIN SMALL LETTER R WITH CEDILLA
                    // LATIN CAPITAL LETTER R WITH CARON
                    // LATIN SMALL LETTER R WITH CARON
                    // LATIN CAPITAL LETTER S WITH ACUTE
                    // LATIN SMALL LETTER S WITH ACUTE
                    // LATIN CAPITAL LETTER S WITH CIRCUMFLEX
                    // LATIN SMALL LETTER S WITH CIRCUMFLEX
                    // LATIN CAPITAL LETTER S WITH CEDILLA
                    // LATIN SMALL LETTER S WITH CEDILLA
                    // LATIN CAPITAL LETTER S WITH CARON
                    // LATIN SMALL LETTER S WITH CARON
                    // LATIN CAPITAL LETTER T WITH CEDILLA
                    // LATIN SMALL LETTER T WITH CEDILLA
                    // LATIN CAPITAL LETTER T WITH CARON
                    // LATIN SMALL LETTER T WITH CARON
                    // LATIN CAPITAL LETTER T WITH STROKE -- no decomposition
                    // LATIN SMALL LETTER T WITH STROKE -- no decomposition
                    // LATIN CAPITAL LETTER U WITH TILDE
                    // LATIN SMALL LETTER U WITH TILDE
                    // LATIN CAPITAL LETTER U WITH MACRON
                    // LATIN SMALL LETTER U WITH MACRON
                    // LATIN CAPITAL LETTER U WITH BREVE
                    // LATIN SMALL LETTER U WITH BREVE
                    // LATIN CAPITAL LETTER U WITH RING ABOVE
                    // LATIN SMALL LETTER U WITH RING ABOVE
                    // LATIN CAPITAL LETTER U WITH DOUBLE ACUTE
                    // LATIN SMALL LETTER U WITH DOUBLE ACUTE
                    // LATIN CAPITAL LETTER U WITH OGONEK
                    // LATIN SMALL LETTER U WITH OGONEK
                    // LATIN CAPITAL LETTER W WITH CIRCUMFLEX
                    // LATIN SMALL LETTER W WITH CIRCUMFLEX
                    // LATIN CAPITAL LETTER Y WITH CIRCUMFLEX
                    // LATIN SMALL LETTER Y WITH CIRCUMFLEX
                    // LATIN CAPITAL LETTER Y WITH DIAERESIS
                    // LATIN CAPITAL LETTER Z WITH ACUTE
                    // LATIN SMALL LETTER Z WITH ACUTE
                    // LATIN CAPITAL LETTER Z WITH DOT ABOVE
                    // LATIN SMALL LETTER Z WITH DOT ABOVE
                    // LATIN CAPITAL LETTER Z WITH CARON
                    // LATIN SMALL LETTER Z WITH CARON
                    // LATIN SMALL LETTER LONG S    
                    // LATIN SMALL LETTER B WITH STROKE -- no decomposition
                    // LATIN CAPITAL LETTER B WITH HOOK -- no decomposition
                    // LATIN CAPITAL LETTER B WITH TOPBAR -- no decomposition
                    // LATIN SMALL LETTER B WITH TOPBAR -- no decomposition
                    // LATIN CAPITAL LETTER TONE SIX -- no decomposition
                    // LATIN SMALL LETTER TONE SIX -- no decomposition
                    // LATIN CAPITAL LETTER OPEN O -- no decomposition
                    // LATIN CAPITAL LETTER C WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER C WITH HOOK -- no decomposition
                    // LATIN CAPITAL LETTER AFRICAN D -- no decomposition
                    // LATIN CAPITAL LETTER D WITH HOOK -- no decomposition
                    // LATIN CAPITAL LETTER D WITH TOPBAR -- no decomposition
                    // LATIN SMALL LETTER D WITH TOPBAR -- no decomposition
                    // LATIN SMALL LETTER TURNED DELTA -- no decomposition
                    // LATIN CAPITAL LETTER REVERSED E -- no decomposition
                    // LATIN CAPITAL LETTER SCHWA -- no decomposition
                    // LATIN CAPITAL LETTER OPEN E -- no decomposition
                    // LATIN CAPITAL LETTER F WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER F WITH HOOK -- no decomposition
                    // LATIN CAPITAL LETTER G WITH HOOK -- no decomposition
                    // LATIN CAPITAL LETTER GAMMA -- no decomposition
                    // LATIN SMALL LETTER HV -- no decomposition
                    // LATIN CAPITAL LETTER IOTA -- no decomposition
                    // LATIN CAPITAL LETTER I WITH STROKE -- no decomposition
                    // LATIN CAPITAL LETTER K WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER K WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER L WITH BAR -- no decomposition
                    // LATIN SMALL LETTER LAMBDA WITH STROKE -- no decomposition
                    // LATIN CAPITAL LETTER TURNED M -- no decomposition
                    // LATIN CAPITAL LETTER N WITH LEFT HOOK -- no decomposition
                    // LATIN SMALL LETTER N WITH LONG RIGHT LEG -- no decomposition
                    // LATIN CAPITAL LETTER O WITH MIDDLE TILDE -- no decomposition
                    // LATIN CAPITAL LETTER O WITH HORN
                    // LATIN SMALL LETTER O WITH HORN
                    // LATIN CAPITAL LETTER OI -- no decomposition
                    // LATIN SMALL LETTER OI -- no decomposition
                    // LATIN CAPITAL LETTER P WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER P WITH HOOK -- no decomposition
                    // LATIN LETTER YR -- no decomposition
                    // LATIN CAPITAL LETTER TONE TWO -- no decomposition
                    // LATIN SMALL LETTER TONE TWO -- no decomposition
                    // LATIN CAPITAL LETTER ESH -- no decomposition
                    // LATIN LETTER REVERSED ESH LOOP -- no decomposition
                    // LATIN SMALL LETTER T WITH PALATAL HOOK -- no decomposition
                    // LATIN CAPITAL LETTER T WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER T WITH HOOK -- no decomposition
                    // LATIN CAPITAL LETTER T WITH RETROFLEX HOOK -- no decomposition
                    // LATIN CAPITAL LETTER U WITH HORN
                    // LATIN SMALL LETTER U WITH HORN
                    // LATIN CAPITAL LETTER UPSILON -- no decomposition
                    // LATIN CAPITAL LETTER V WITH HOOK -- no decomposition
                    // LATIN CAPITAL LETTER Y WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER Y WITH HOOK -- no decomposition
                    // LATIN CAPITAL LETTER Z WITH STROKE -- no decomposition
                    // LATIN SMALL LETTER Z WITH STROKE -- no decomposition
                    // LATIN CAPITAL LETTER EZH -- no decomposition
                    // LATIN CAPITAL LETTER EZH REVERSED -- no decomposition
                    // LATIN SMALL LETTER EZH REVERSED -- no decomposition
                    // LATIN SMALL LETTER EZH WITH TAIL -- no decomposition
                    // LATIN LETTER TWO WITH STROKE -- no decomposition
                    // LATIN CAPITAL LETTER TONE FIVE -- no decomposition
                    // LATIN SMALL LETTER TONE FIVE -- no decomposition
                    //_seoCharacterTable.Add(ToUnichar("01BE"), "´");	// LATIN LETTER INVERTED GLOTTAL STOP WITH STROKE -- no decomposition
                    // LATIN LETTER WYNN -- no decomposition
                    // LATIN LETTER DENTAL CLICK -- no decomposition
                    // LATIN LETTER LATERAL CLICK -- no decomposition
                    // LATIN LETTER ALVEOLAR CLICK -- no decomposition
                    // LATIN LETTER RETROFLEX CLICK -- no decomposition
                    // LATIN CAPITAL LETTER DZ WITH CARON
                    // LATIN CAPITAL LETTER D WITH SMALL LETTER Z WITH CARON
                    // LATIN SMALL LETTER DZ WITH CARON
                    // LATIN CAPITAL LETTER LJ
                    // LATIN CAPITAL LETTER L WITH SMALL LETTER J
                    // LATIN SMALL LETTER LJ
                    // LATIN CAPITAL LETTER NJ
                    // LATIN CAPITAL LETTER N WITH SMALL LETTER J
                    // LATIN SMALL LETTER NJ
                    // LATIN CAPITAL LETTER A WITH CARON
                    // LATIN SMALL LETTER A WITH CARON
                    // LATIN CAPITAL LETTER I WITH CARON
                    // LATIN SMALL LETTER I WITH CARON
                    // LATIN CAPITAL LETTER O WITH CARON
                    // LATIN SMALL LETTER O WITH CARON
                    // LATIN CAPITAL LETTER U WITH CARON
                    // LATIN SMALL LETTER U WITH CARON
                    // LATIN CAPITAL LETTER U WITH DIAERESIS AND MACRON
                    // LATIN SMALL LETTER U WITH DIAERESIS AND MACRON
                    // LATIN CAPITAL LETTER U WITH DIAERESIS AND ACUTE
                    // LATIN SMALL LETTER U WITH DIAERESIS AND ACUTE
                    // LATIN CAPITAL LETTER U WITH DIAERESIS AND CARON
                    // LATIN SMALL LETTER U WITH DIAERESIS AND CARON
                    // LATIN CAPITAL LETTER U WITH DIAERESIS AND GRAVE
                    // LATIN SMALL LETTER U WITH DIAERESIS AND GRAVE
                    // LATIN SMALL LETTER TURNED E -- no decomposition
                    // LATIN CAPITAL LETTER A WITH DIAERESIS AND MACRON
                    // LATIN SMALL LETTER A WITH DIAERESIS AND MACRON
                    // LATIN CAPITAL LETTER A WITH DOT ABOVE AND MACRON
                    // LATIN SMALL LETTER A WITH DOT ABOVE AND MACRON
                    // LATIN CAPITAL LETTER AE WITH MACRON
                    // LATIN SMALL LETTER AE WITH MACRON
                    // LATIN CAPITAL LETTER G WITH STROKE -- no decomposition
                    // LATIN SMALL LETTER G WITH STROKE -- no decomposition
                    // LATIN CAPITAL LETTER G WITH CARON
                    // LATIN SMALL LETTER G WITH CARON
                    // LATIN CAPITAL LETTER K WITH CARON
                    // LATIN SMALL LETTER K WITH CARON
                    // LATIN CAPITAL LETTER O WITH OGONEK
                    // LATIN SMALL LETTER O WITH OGONEK
                    // LATIN CAPITAL LETTER O WITH OGONEK AND MACRON
                    // LATIN SMALL LETTER O WITH OGONEK AND MACRON
                    // LATIN CAPITAL LETTER EZH WITH CARON
                    // LATIN SMALL LETTER EZH WITH CARON
                    // LATIN SMALL LETTER J WITH CARON
                    // LATIN CAPITAL LETTER DZ
                    // LATIN CAPITAL LETTER D WITH SMALL LETTER Z
                    // LATIN SMALL LETTER DZ
                    // LATIN CAPITAL LETTER G WITH ACUTE
                    // LATIN SMALL LETTER G WITH ACUTE
                    // LATIN CAPITAL LETTER HWAIR -- no decomposition
                    // LATIN CAPITAL LETTER WYNN -- no decomposition
                    // LATIN CAPITAL LETTER N WITH GRAVE
                    // LATIN SMALL LETTER N WITH GRAVE
                    // LATIN CAPITAL LETTER A WITH RING ABOVE AND ACUTE
                    // LATIN SMALL LETTER A WITH RING ABOVE AND ACUTE
                    // LATIN CAPITAL LETTER AE WITH ACUTE
                    // LATIN SMALL LETTER AE WITH ACUTE
                    // LATIN CAPITAL LETTER O WITH STROKE AND ACUTE
                    // LATIN SMALL LETTER O WITH STROKE AND ACUTE
                    // LATIN CAPITAL LETTER A WITH DOUBLE GRAVE
                    // LATIN SMALL LETTER A WITH DOUBLE GRAVE
                    // LATIN CAPITAL LETTER A WITH INVERTED BREVE
                    // LATIN SMALL LETTER A WITH INVERTED BREVE
                    // LATIN CAPITAL LETTER E WITH DOUBLE GRAVE
                    // LATIN SMALL LETTER E WITH DOUBLE GRAVE
                    // LATIN CAPITAL LETTER E WITH INVERTED BREVE
                    // LATIN SMALL LETTER E WITH INVERTED BREVE
                    // LATIN CAPITAL LETTER I WITH DOUBLE GRAVE
                    // LATIN SMALL LETTER I WITH DOUBLE GRAVE
                    // LATIN CAPITAL LETTER I WITH INVERTED BREVE
                    // LATIN SMALL LETTER I WITH INVERTED BREVE
                    // LATIN CAPITAL LETTER O WITH DOUBLE GRAVE
                    // LATIN SMALL LETTER O WITH DOUBLE GRAVE
                    // LATIN CAPITAL LETTER O WITH INVERTED BREVE
                    // LATIN SMALL LETTER O WITH INVERTED BREVE
                    // LATIN CAPITAL LETTER R WITH DOUBLE GRAVE
                    // LATIN SMALL LETTER R WITH DOUBLE GRAVE
                    // LATIN CAPITAL LETTER R WITH INVERTED BREVE
                    // LATIN SMALL LETTER R WITH INVERTED BREVE
                    // LATIN CAPITAL LETTER U WITH DOUBLE GRAVE
                    // LATIN SMALL LETTER U WITH DOUBLE GRAVE
                    // LATIN CAPITAL LETTER U WITH INVERTED BREVE
                    // LATIN SMALL LETTER U WITH INVERTED BREVE
                    // LATIN CAPITAL LETTER S WITH COMMA BELOW
                    // LATIN SMALL LETTER S WITH COMMA BELOW
                    // LATIN CAPITAL LETTER T WITH COMMA BELOW
                    // LATIN SMALL LETTER T WITH COMMA BELOW
                    // LATIN CAPITAL LETTER YOGH -- no decomposition
                    // LATIN SMALL LETTER YOGH -- no decomposition
                    // LATIN CAPITAL LETTER H WITH CARON
                    // LATIN SMALL LETTER H WITH CARON
                    // LATIN CAPITAL LETTER N WITH LONG RIGHT LEG -- no decomposition
                    // LATIN SMALL LETTER D WITH CURL -- no decomposition
                    // LATIN CAPITAL LETTER OU -- no decomposition
                    // LATIN SMALL LETTER OU -- no decomposition
                    // LATIN CAPITAL LETTER Z WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER Z WITH HOOK -- no decomposition
                    // LATIN CAPITAL LETTER A WITH DOT ABOVE
                    // LATIN SMALL LETTER A WITH DOT ABOVE
                    // LATIN CAPITAL LETTER E WITH CEDILLA
                    // LATIN SMALL LETTER E WITH CEDILLA
                    // LATIN CAPITAL LETTER O WITH DIAERESIS AND MACRON
                    // LATIN SMALL LETTER O WITH DIAERESIS AND MACRON
                    // LATIN CAPITAL LETTER O WITH TILDE AND MACRON
                    // LATIN SMALL LETTER O WITH TILDE AND MACRON
                    // LATIN CAPITAL LETTER O WITH DOT ABOVE
                    // LATIN SMALL LETTER O WITH DOT ABOVE
                    // LATIN CAPITAL LETTER O WITH DOT ABOVE AND MACRON
                    // LATIN SMALL LETTER O WITH DOT ABOVE AND MACRON
                    // LATIN CAPITAL LETTER Y WITH MACRON
                    // LATIN SMALL LETTER Y WITH MACRON
                    // LATIN SMALL LETTER L WITH CURL -- no decomposition
                    // LATIN SMALL LETTER N WITH CURL -- no decomposition
                    // LATIN SMALL LETTER T WITH CURL -- no decomposition
                    // LATIN SMALL LETTER TURNED A -- no decomposition
                    // LATIN SMALL LETTER ALPHA -- no decomposition
                    // LATIN SMALL LETTER TURNED ALPHA -- no decomposition
                    // LATIN SMALL LETTER B WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER OPEN O -- no decomposition
                    // LATIN SMALL LETTER C WITH CURL -- no decomposition
                    // LATIN SMALL LETTER D WITH TAIL -- no decomposition
                    // LATIN SMALL LETTER D WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER REVERSED E -- no decomposition
                    // LATIN SMALL LETTER SCHWA -- no decomposition
                    // LATIN SMALL LETTER SCHWA WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER OPEN E -- no decomposition
                    // LATIN SMALL LETTER REVERSED OPEN E -- no decomposition
                    // LATIN SMALL LETTER REVERSED OPEN E WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER CLOSED REVERSED OPEN E -- no decomposition
                    // LATIN SMALL LETTER DOTLESS J WITH STROKE -- no decomposition
                    // LATIN SMALL LETTER G WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER SCRIPT G -- no decomposition
                    // LATIN LETTER SMALL CAPITAL G -- no decomposition
                    // LATIN SMALL LETTER GAMMA -- no decomposition
                    // LATIN SMALL LETTER RAMS HORN -- no decomposition
                    // LATIN SMALL LETTER TURNED H -- no decomposition
                    // LATIN SMALL LETTER H WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER HENG WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER I WITH STROKE -- no decomposition
                    // LATIN SMALL LETTER IOTA -- no decomposition
                    // LATIN LETTER SMALL CAPITAL I -- no decomposition
                    // LATIN SMALL LETTER L WITH MIDDLE TILDE -- no decomposition
                    // LATIN SMALL LETTER L WITH BELT -- no decomposition
                    // LATIN SMALL LETTER L WITH RETROFLEX HOOK -- no decomposition
                    // LATIN SMALL LETTER LEZH -- no decomposition
                    // LATIN SMALL LETTER TURNED M -- no decomposition
                    // LATIN SMALL LETTER TURNED M WITH LONG LEG -- no decomposition
                    // LATIN SMALL LETTER M WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER N WITH LEFT HOOK -- no decomposition
                    // LATIN SMALL LETTER N WITH RETROFLEX HOOK -- no decomposition
                    // LATIN LETTER SMALL CAPITAL N -- no decomposition
                    // LATIN SMALL LETTER BARRED O -- no decomposition
                    // LATIN LETTER SMALL CAPITAL OE -- no decomposition
                    // LATIN SMALL LETTER CLOSED OMEGA -- no decomposition
                    // LATIN SMALL LETTER PHI -- no decomposition
                    // LATIN SMALL LETTER TURNED R -- no decomposition
                    // LATIN SMALL LETTER TURNED R WITH LONG LEG -- no decomposition
                    // LATIN SMALL LETTER TURNED R WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER R WITH LONG LEG -- no decomposition
                    // LATIN SMALL LETTER R WITH TAIL -- no decomposition
                    // LATIN SMALL LETTER R WITH FISHHOOK -- no decomposition
                    // LATIN SMALL LETTER REVERSED R WITH FISHHOOK -- no decomposition
                    // LATIN LETTER SMALL CAPITAL R -- no decomposition
                    // LATIN LETTER SMALL CAPITAL INVERTED R -- no decomposition
                    // LATIN SMALL LETTER S WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER ESH -- no decomposition
                    // LATIN SMALL LETTER DOTLESS J WITH STROKE AND HOOK -- no decomposition
                    // LATIN SMALL LETTER SQUAT REVERSED ESH -- no decomposition
                    // LATIN SMALL LETTER ESH WITH CURL -- no decomposition
                    // LATIN SMALL LETTER TURNED T -- no decomposition
                    // LATIN SMALL LETTER T WITH RETROFLEX HOOK -- no decomposition
                    // LATIN SMALL LETTER U BAR -- no decomposition
                    // LATIN SMALL LETTER UPSILON -- no decomposition
                    // LATIN SMALL LETTER V WITH HOOK -- no decomposition
                    // LATIN SMALL LETTER TURNED V -- no decomposition
                    // LATIN SMALL LETTER TURNED W -- no decomposition
                    // LATIN SMALL LETTER TURNED Y -- no decomposition
                    // LATIN LETTER SMALL CAPITAL Y -- no decomposition
                    // LATIN SMALL LETTER Z WITH RETROFLEX HOOK -- no decomposition
                    // LATIN SMALL LETTER Z WITH CURL -- no decomposition
                    // LATIN SMALL LETTER EZH -- no decomposition
                    // LATIN SMALL LETTER EZH WITH CURL -- no decomposition
                    // LATIN LETTER GLOTTAL STOP -- no decomposition
                    // LATIN LETTER PHARYNGEAL VOICED FRICATIVE -- no decomposition
                    // LATIN LETTER INVERTED GLOTTAL STOP -- no decomposition
                    // LATIN LETTER STRETCHED C -- no decomposition
                    //_seoCharacterTable.Add(ToUnichar("0298"), "O˜");	// LATIN LETTER BILABIAL CLICK -- no decomposition
                    // LATIN LETTER SMALL CAPITAL B -- no decomposition
                    // LATIN SMALL LETTER CLOSED OPEN E -- no decomposition
                    // LATIN LETTER SMALL CAPITAL G WITH HOOK -- no decomposition
                    // LATIN LETTER SMALL CAPITAL H -- no decomposition
                    // LATIN SMALL LETTER J WITH CROSSED-TAIL -- no decomposition
                    // LATIN SMALL LETTER TURNED K -- no decomposition
                    // LATIN LETTER SMALL CAPITAL L -- no decomposition
                    // LATIN SMALL LETTER Q WITH HOOK -- no decomposition
                    // LATIN LETTER GLOTTAL STOP WITH STROKE -- no decomposition
                    // LATIN LETTER REVERSED GLOTTAL STOP WITH STROKE -- no decomposition
                    // LATIN SMALL LETTER DZ DIGRAPH -- no decomposition
                    // LATIN SMALL LETTER DEZH DIGRAPH -- no decomposition
                    // LATIN SMALL LETTER DZ DIGRAPH WITH CURL -- no decomposition
                    // LATIN SMALL LETTER TS DIGRAPH -- no decomposition
                    // LATIN SMALL LETTER TESH DIGRAPH -- no decomposition
                    // LATIN SMALL LETTER TC DIGRAPH WITH CURL -- no decomposition
                    // LATIN SMALL LETTER FENG DIGRAPH -- no decomposition
                    // LATIN SMALL LETTER LS DIGRAPH -- no decomposition
                    // LATIN SMALL LETTER LZ DIGRAPH -- no decomposition
                    // LATIN LETTER BILABIAL PERCUSSIVE -- no decomposition
                    // LATIN LETTER BIDENTAL PERCUSSIVE -- no decomposition
                    // LATIN SMALL LETTER TURNED H WITH FISHHOOK -- no decomposition
                    // LATIN SMALL LETTER TURNED H WITH FISHHOOK AND TAIL -- no decomposition
                    // MODIFIER LETTER SMALL H
                    // MODIFIER LETTER SMALL H WITH HOOK
                    // MODIFIER LETTER SMALL J
                    // MODIFIER LETTER SMALL R
                    // MODIFIER LETTER SMALL TURNED R
                    // MODIFIER LETTER SMALL TURNED R WITH HOOK
                    // MODIFIER LETTER SMALL CAPITAL INVERTED R
                    // MODIFIER LETTER SMALL W
                    // MODIFIER LETTER SMALL Y
                    // MODIFIER LETTER SMALL L
                    // MODIFIER LETTER SMALL S
                    // MODIFIER LETTER SMALL X
                    // MODIFIER LETTER SMALL REVERSED GLOTTAL STOP
                    // LATIN LETTER SMALL CAPITAL A -- no decomposition
                    // LATIN LETTER SMALL CAPITAL AE -- no decomposition
                    // LATIN SMALL LETTER TURNED AE -- no decomposition
                    // LATIN LETTER SMALL CAPITAL BARRED B -- no decomposition
                    // LATIN LETTER SMALL CAPITAL C -- no decomposition
                    // LATIN LETTER SMALL CAPITAL D -- no decomposition
                    // LATIN LETTER SMALL CAPITAL ETH -- no decomposition
                    // LATIN LETTER SMALL CAPITAL E -- no decomposition
                    // LATIN SMALL LETTER TURNED OPEN E -- no decomposition
                    // LATIN SMALL LETTER TURNED I -- no decomposition
                    // LATIN LETTER SMALL CAPITAL J -- no decomposition
                    // LATIN LETTER SMALL CAPITAL K -- no decomposition
                    // LATIN LETTER SMALL CAPITAL L WITH STROKE -- no decomposition
                    // LATIN LETTER SMALL CAPITAL M -- no decomposition
                    // LATIN LETTER SMALL CAPITAL REVERSED N -- no decomposition
                    // LATIN LETTER SMALL CAPITAL O -- no decomposition
                    // LATIN LETTER SMALL CAPITAL OPEN O -- no decomposition
                    // LATIN SMALL LETTER SIDEWAYS O -- no decomposition
                    // LATIN SMALL LETTER SIDEWAYS OPEN O -- no decomposition
                    // LATIN SMALL LETTER SIDEWAYS O WITH STROKE -- no decomposition
                    // LATIN SMALL LETTER TURNED OE -- no decomposition
                    // LATIN LETTER SMALL CAPITAL OU -- no decomposition
                    // LATIN SMALL LETTER TOP HALF O -- no decomposition
                    // LATIN SMALL LETTER BOTTOM HALF O -- no decomposition
                    // LATIN LETTER SMALL CAPITAL P -- no decomposition
                    // LATIN LETTER SMALL CAPITAL REVERSED R -- no decomposition
                    // LATIN LETTER SMALL CAPITAL TURNED R -- no decomposition
                    // LATIN LETTER SMALL CAPITAL T -- no decomposition
                    // LATIN LETTER SMALL CAPITAL U -- no decomposition
                    // LATIN SMALL LETTER SIDEWAYS U -- no decomposition
                    // LATIN SMALL LETTER SIDEWAYS DIAERESIZED U -- no decomposition
                    // LATIN SMALL LETTER SIDEWAYS TURNED M -- no decomposition
                    // LATIN LETTER SMALL CAPITAL V -- no decomposition
                    // LATIN LETTER SMALL CAPITAL W -- no decomposition
                    // LATIN LETTER SMALL CAPITAL Z -- no decomposition
                    // LATIN LETTER SMALL CAPITAL EZH -- no decomposition
                    // LATIN LETTER VOICED LARYNGEAL SPIRANT -- no decomposition
                    // LATIN LETTER AIN -- no decomposition
                    // MODIFIER LETTER CAPITAL A
                    // MODIFIER LETTER CAPITAL AE
                    // MODIFIER LETTER CAPITAL B
                    // MODIFIER LETTER CAPITAL BARRED B -- no decomposition
                    // MODIFIER LETTER CAPITAL D
                    // MODIFIER LETTER CAPITAL E
                    // MODIFIER LETTER CAPITAL REVERSED E
                    // MODIFIER LETTER CAPITAL G
                    // MODIFIER LETTER CAPITAL H
                    // MODIFIER LETTER CAPITAL I
                    // MODIFIER LETTER CAPITAL J
                    // MODIFIER LETTER CAPITAL K
                    // MODIFIER LETTER CAPITAL L
                    // MODIFIER LETTER CAPITAL M
                    // MODIFIER LETTER CAPITAL N
                    // MODIFIER LETTER CAPITAL REVERSED N -- no decomposition
                    // MODIFIER LETTER CAPITAL O
                    // MODIFIER LETTER CAPITAL OU
                    // MODIFIER LETTER CAPITAL P
                    // MODIFIER LETTER CAPITAL R
                    // MODIFIER LETTER CAPITAL T
                    // MODIFIER LETTER CAPITAL U
                    // MODIFIER LETTER CAPITAL W
                    // MODIFIER LETTER SMALL A
                    // MODIFIER LETTER SMALL TURNED A
                    // MODIFIER LETTER SMALL TURNED AE
                    // MODIFIER LETTER SMALL B
                    // MODIFIER LETTER SMALL D
                    // MODIFIER LETTER SMALL E
                    // MODIFIER LETTER SMALL SCHWA
                    // MODIFIER LETTER SMALL OPEN E
                    // MODIFIER LETTER SMALL TURNED OPEN E
                    // MODIFIER LETTER SMALL G
                    // MODIFIER LETTER SMALL TURNED I -- no decomposition
                    // MODIFIER LETTER SMALL K
                    // MODIFIER LETTER SMALL M
                    // MODIFIER LETTER SMALL ENG
                    // MODIFIER LETTER SMALL O
                    // MODIFIER LETTER SMALL OPEN O
                    // MODIFIER LETTER SMALL TOP HALF O
                    // MODIFIER LETTER SMALL BOTTOM HALF O
                    // MODIFIER LETTER SMALL P
                    // MODIFIER LETTER SMALL T
                    // MODIFIER LETTER SMALL U
                    // MODIFIER LETTER SMALL SIDEWAYS U
                    // MODIFIER LETTER SMALL TURNED M
                    // MODIFIER LETTER SMALL V
                    // LATIN SUBSCRIPT SMALL LETTER I
                    // LATIN SUBSCRIPT SMALL LETTER R
                    // LATIN SUBSCRIPT SMALL LETTER U
                    // LATIN SUBSCRIPT SMALL LETTER V
                    // LATIN SMALL LETTER UE -- no decomposition
                    // LATIN CAPITAL LETTER A WITH RING BELOW
                    // LATIN SMALL LETTER A WITH RING BELOW
                    // LATIN CAPITAL LETTER B WITH DOT ABOVE
                    // LATIN SMALL LETTER B WITH DOT ABOVE
                    // LATIN CAPITAL LETTER B WITH DOT BELOW
                    // LATIN SMALL LETTER B WITH DOT BELOW
                    // LATIN CAPITAL LETTER B WITH LINE BELOW
                    // LATIN SMALL LETTER B WITH LINE BELOW
                    // LATIN CAPITAL LETTER C WITH CEDILLA AND ACUTE
                    // LATIN SMALL LETTER C WITH CEDILLA AND ACUTE
                    // LATIN CAPITAL LETTER D WITH DOT ABOVE
                    // LATIN SMALL LETTER D WITH DOT ABOVE
                    // LATIN CAPITAL LETTER D WITH DOT BELOW
                    // LATIN SMALL LETTER D WITH DOT BELOW
                    // LATIN CAPITAL LETTER D WITH LINE BELOW
                    // LATIN SMALL LETTER D WITH LINE BELOW
                    // LATIN CAPITAL LETTER D WITH CEDILLA
                    // LATIN SMALL LETTER D WITH CEDILLA
                    // LATIN CAPITAL LETTER D WITH CIRCUMFLEX BELOW
                    // LATIN SMALL LETTER D WITH CIRCUMFLEX BELOW
                    // LATIN CAPITAL LETTER E WITH MACRON AND GRAVE
                    // LATIN SMALL LETTER E WITH MACRON AND GRAVE
                    // LATIN CAPITAL LETTER E WITH MACRON AND ACUTE
                    // LATIN SMALL LETTER E WITH MACRON AND ACUTE
                    // LATIN CAPITAL LETTER E WITH CIRCUMFLEX BELOW
                    // LATIN SMALL LETTER E WITH CIRCUMFLEX BELOW
                    // LATIN CAPITAL LETTER E WITH TILDE BELOW
                    // LATIN SMALL LETTER E WITH TILDE BELOW
                    // LATIN CAPITAL LETTER E WITH CEDILLA AND BREVE
                    // LATIN SMALL LETTER E WITH CEDILLA AND BREVE
                    // LATIN CAPITAL LETTER F WITH DOT ABOVE
                    // LATIN SMALL LETTER F WITH DOT ABOVE
                    // LATIN CAPITAL LETTER G WITH MACRON
                    // LATIN SMALL LETTER G WITH MACRON
                    // LATIN CAPITAL LETTER H WITH DOT ABOVE
                    // LATIN SMALL LETTER H WITH DOT ABOVE
                    // LATIN CAPITAL LETTER H WITH DOT BELOW
                    // LATIN SMALL LETTER H WITH DOT BELOW
                    // LATIN CAPITAL LETTER H WITH DIAERESIS
                    // LATIN SMALL LETTER H WITH DIAERESIS
                    // LATIN CAPITAL LETTER H WITH CEDILLA
                    // LATIN SMALL LETTER H WITH CEDILLA
                    // LATIN CAPITAL LETTER H WITH BREVE BELOW
                    // LATIN SMALL LETTER H WITH BREVE BELOW
                    // LATIN CAPITAL LETTER I WITH TILDE BELOW
                    // LATIN SMALL LETTER I WITH TILDE BELOW
                    // LATIN CAPITAL LETTER I WITH DIAERESIS AND ACUTE
                    // LATIN SMALL LETTER I WITH DIAERESIS AND ACUTE
                    // LATIN CAPITAL LETTER K WITH ACUTE
                    // LATIN SMALL LETTER K WITH ACUTE
                    // LATIN CAPITAL LETTER K WITH DOT BELOW
                    // LATIN SMALL LETTER K WITH DOT BELOW
                    // LATIN CAPITAL LETTER K WITH LINE BELOW
                    // LATIN SMALL LETTER K WITH LINE BELOW
                    // LATIN CAPITAL LETTER L WITH DOT BELOW
                    // LATIN SMALL LETTER L WITH DOT BELOW
                    // LATIN CAPITAL LETTER L WITH DOT BELOW AND MACRON
                    // LATIN SMALL LETTER L WITH DOT BELOW AND MACRON
                    // LATIN CAPITAL LETTER L WITH LINE BELOW
                    // LATIN SMALL LETTER L WITH LINE BELOW
                    // LATIN CAPITAL LETTER L WITH CIRCUMFLEX BELOW
                    // LATIN SMALL LETTER L WITH CIRCUMFLEX BELOW
                    // LATIN CAPITAL LETTER M WITH ACUTE
                    // LATIN SMALL LETTER M WITH ACUTE
                    // LATIN CAPITAL LETTER M WITH DOT ABOVE
                    // LATIN SMALL LETTER M WITH DOT ABOVE
                    // LATIN CAPITAL LETTER M WITH DOT BELOW
                    // LATIN SMALL LETTER M WITH DOT BELOW
                    // LATIN CAPITAL LETTER N WITH DOT ABOVE
                    // LATIN SMALL LETTER N WITH DOT ABOVE
                    // LATIN CAPITAL LETTER N WITH DOT BELOW
                    // LATIN SMALL LETTER N WITH DOT BELOW
                    // LATIN CAPITAL LETTER N WITH LINE BELOW
                    // LATIN SMALL LETTER N WITH LINE BELOW
                    // LATIN CAPITAL LETTER N WITH CIRCUMFLEX BELOW
                    // LATIN SMALL LETTER N WITH CIRCUMFLEX BELOW
                    // LATIN CAPITAL LETTER O WITH TILDE AND ACUTE
                    // LATIN SMALL LETTER O WITH TILDE AND ACUTE
                    // LATIN CAPITAL LETTER O WITH TILDE AND DIAERESIS
                    // LATIN SMALL LETTER O WITH TILDE AND DIAERESIS
                    // LATIN CAPITAL LETTER O WITH MACRON AND GRAVE
                    // LATIN SMALL LETTER O WITH MACRON AND GRAVE
                    // LATIN CAPITAL LETTER O WITH MACRON AND ACUTE
                    // LATIN SMALL LETTER O WITH MACRON AND ACUTE
                    // LATIN CAPITAL LETTER P WITH ACUTE
                    // LATIN SMALL LETTER P WITH ACUTE
                    // LATIN CAPITAL LETTER P WITH DOT ABOVE
                    // LATIN SMALL LETTER P WITH DOT ABOVE
                    // LATIN CAPITAL LETTER R WITH DOT ABOVE
                    // LATIN SMALL LETTER R WITH DOT ABOVE
                    // LATIN CAPITAL LETTER R WITH DOT BELOW
                    // LATIN SMALL LETTER R WITH DOT BELOW
                    // LATIN CAPITAL LETTER R WITH DOT BELOW AND MACRON
                    // LATIN SMALL LETTER R WITH DOT BELOW AND MACRON
                    // LATIN CAPITAL LETTER R WITH LINE BELOW
                    // LATIN SMALL LETTER R WITH LINE BELOW
                    // LATIN CAPITAL LETTER S WITH DOT ABOVE
                    // LATIN SMALL LETTER S WITH DOT ABOVE
                    // LATIN CAPITAL LETTER S WITH DOT BELOW
                    // LATIN SMALL LETTER S WITH DOT BELOW
                    // LATIN CAPITAL LETTER S WITH ACUTE AND DOT ABOVE
                    // LATIN SMALL LETTER S WITH ACUTE AND DOT ABOVE
                    // LATIN CAPITAL LETTER S WITH CARON AND DOT ABOVE
                    // LATIN SMALL LETTER S WITH CARON AND DOT ABOVE
                    // LATIN CAPITAL LETTER S WITH DOT BELOW AND DOT ABOVE
                    // LATIN SMALL LETTER S WITH DOT BELOW AND DOT ABOVE
                    // LATIN CAPITAL LETTER T WITH DOT ABOVE
                    // LATIN SMALL LETTER T WITH DOT ABOVE
                    // LATIN CAPITAL LETTER T WITH DOT BELOW
                    // LATIN SMALL LETTER T WITH DOT BELOW
                    // LATIN CAPITAL LETTER T WITH LINE BELOW
                    // LATIN SMALL LETTER T WITH LINE BELOW
                    // LATIN CAPITAL LETTER T WITH CIRCUMFLEX BELOW
                    // LATIN SMALL LETTER T WITH CIRCUMFLEX BELOW
                    // LATIN CAPITAL LETTER U WITH DIAERESIS BELOW
                    // LATIN SMALL LETTER U WITH DIAERESIS BELOW
                    // LATIN CAPITAL LETTER U WITH TILDE BELOW
                    // LATIN SMALL LETTER U WITH TILDE BELOW
                    // LATIN CAPITAL LETTER U WITH CIRCUMFLEX BELOW
                    // LATIN SMALL LETTER U WITH CIRCUMFLEX BELOW
                    // LATIN CAPITAL LETTER U WITH TILDE AND ACUTE
                    // LATIN SMALL LETTER U WITH TILDE AND ACUTE
                    // LATIN CAPITAL LETTER U WITH MACRON AND DIAERESIS
                    // LATIN SMALL LETTER U WITH MACRON AND DIAERESIS
                    // LATIN CAPITAL LETTER V WITH TILDE
                    // LATIN SMALL LETTER V WITH TILDE
                    // LATIN CAPITAL LETTER V WITH DOT BELOW
                    // LATIN SMALL LETTER V WITH DOT BELOW
                    // LATIN CAPITAL LETTER W WITH GRAVE
                    // LATIN SMALL LETTER W WITH GRAVE
                    // LATIN CAPITAL LETTER W WITH ACUTE
                    // LATIN SMALL LETTER W WITH ACUTE
                    // LATIN CAPITAL LETTER W WITH DIAERESIS
                    // LATIN SMALL LETTER W WITH DIAERESIS
                    // LATIN CAPITAL LETTER W WITH DOT ABOVE
                    // LATIN SMALL LETTER W WITH DOT ABOVE
                    // LATIN CAPITAL LETTER W WITH DOT BELOW
                    // LATIN SMALL LETTER W WITH DOT BELOW
                    // LATIN CAPITAL LETTER X WITH DOT ABOVE
                    // LATIN SMALL LETTER X WITH DOT ABOVE
                    // LATIN CAPITAL LETTER X WITH DIAERESIS
                    // LATIN SMALL LETTER X WITH DIAERESIS
                    // LATIN CAPITAL LETTER Y WITH DOT ABOVE
                    // LATIN SMALL LETTER Y WITH DOT ABOVE
                    // LATIN CAPITAL LETTER Z WITH CIRCUMFLEX
                    // LATIN SMALL LETTER Z WITH CIRCUMFLEX
                    // LATIN CAPITAL LETTER Z WITH DOT BELOW
                    // LATIN SMALL LETTER Z WITH DOT BELOW
                    // LATIN CAPITAL LETTER Z WITH LINE BELOW
                    // LATIN SMALL LETTER Z WITH LINE BELOW
                    // LATIN SMALL LETTER H WITH LINE BELOW
                    // LATIN SMALL LETTER T WITH DIAERESIS
                    // LATIN SMALL LETTER W WITH RING ABOVE
                    // LATIN SMALL LETTER Y WITH RING ABOVE
                    // LATIN SMALL LETTER A WITH RIGHT HALF RING
                    // LATIN SMALL LETTER LONG S WITH DOT ABOVE
                    // LATIN CAPITAL LETTER A WITH DOT BELOW
                    // LATIN SMALL LETTER A WITH DOT BELOW
                    // LATIN CAPITAL LETTER A WITH HOOK ABOVE
                    // LATIN SMALL LETTER A WITH HOOK ABOVE
                    // LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND ACUTE
                    // LATIN SMALL LETTER A WITH CIRCUMFLEX AND ACUTE
                    // LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND GRAVE
                    // LATIN SMALL LETTER A WITH CIRCUMFLEX AND GRAVE
                    // LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND HOOK ABOVE
                    // LATIN SMALL LETTER A WITH CIRCUMFLEX AND HOOK ABOVE
                    // LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND TILDE
                    // LATIN SMALL LETTER A WITH CIRCUMFLEX AND TILDE
                    // LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND DOT BELOW
                    // LATIN SMALL LETTER A WITH CIRCUMFLEX AND DOT BELOW
                    // LATIN CAPITAL LETTER A WITH BREVE AND ACUTE
                    // LATIN SMALL LETTER A WITH BREVE AND ACUTE
                    // LATIN CAPITAL LETTER A WITH BREVE AND GRAVE
                    // LATIN SMALL LETTER A WITH BREVE AND GRAVE
                    // LATIN CAPITAL LETTER A WITH BREVE AND HOOK ABOVE
                    // LATIN SMALL LETTER A WITH BREVE AND HOOK ABOVE
                    // LATIN CAPITAL LETTER A WITH BREVE AND TILDE
                    // LATIN SMALL LETTER A WITH BREVE AND TILDE
                    // LATIN CAPITAL LETTER A WITH BREVE AND DOT BELOW
                    // LATIN SMALL LETTER A WITH BREVE AND DOT BELOW
                    // LATIN CAPITAL LETTER E WITH DOT BELOW
                    // LATIN SMALL LETTER E WITH DOT BELOW
                    // LATIN CAPITAL LETTER E WITH HOOK ABOVE
                    // LATIN SMALL LETTER E WITH HOOK ABOVE
                    // LATIN CAPITAL LETTER E WITH TILDE
                    // LATIN SMALL LETTER E WITH TILDE
                    // LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND ACUTE
                    // LATIN SMALL LETTER E WITH CIRCUMFLEX AND ACUTE
                    // LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND GRAVE
                    // LATIN SMALL LETTER E WITH CIRCUMFLEX AND GRAVE
                    // LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND HOOK ABOVE
                    // LATIN SMALL LETTER E WITH CIRCUMFLEX AND HOOK ABOVE
                    // LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND TILDE
                    // LATIN SMALL LETTER E WITH CIRCUMFLEX AND TILDE
                    // LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND DOT BELOW
                    // LATIN SMALL LETTER E WITH CIRCUMFLEX AND DOT BELOW
                    // LATIN CAPITAL LETTER I WITH HOOK ABOVE
                    // LATIN SMALL LETTER I WITH HOOK ABOVE
                    // LATIN CAPITAL LETTER I WITH DOT BELOW
                    // LATIN SMALL LETTER I WITH DOT BELOW
                    // LATIN CAPITAL LETTER O WITH DOT BELOW
                    // LATIN SMALL LETTER O WITH DOT BELOW
                    // LATIN CAPITAL LETTER O WITH HOOK ABOVE
                    // LATIN SMALL LETTER O WITH HOOK ABOVE
                    // LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND ACUTE
                    // LATIN SMALL LETTER O WITH CIRCUMFLEX AND ACUTE
                    // LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND GRAVE
                    // LATIN SMALL LETTER O WITH CIRCUMFLEX AND GRAVE
                    // LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND HOOK ABOVE
                    // LATIN SMALL LETTER O WITH CIRCUMFLEX AND HOOK ABOVE
                    // LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND TILDE
                    // LATIN SMALL LETTER O WITH CIRCUMFLEX AND TILDE
                    // LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND DOT BELOW
                    // LATIN SMALL LETTER O WITH CIRCUMFLEX AND DOT BELOW
                    // LATIN CAPITAL LETTER O WITH HORN AND ACUTE
                    // LATIN SMALL LETTER O WITH HORN AND ACUTE
                    // LATIN CAPITAL LETTER O WITH HORN AND GRAVE
                    // LATIN SMALL LETTER O WITH HORN AND GRAVE
                    // LATIN CAPITAL LETTER O WITH HORN AND HOOK ABOVE
                    // LATIN SMALL LETTER O WITH HORN AND HOOK ABOVE
                    // LATIN CAPITAL LETTER O WITH HORN AND TILDE
                    // LATIN SMALL LETTER O WITH HORN AND TILDE
                    // LATIN CAPITAL LETTER O WITH HORN AND DOT BELOW
                    // LATIN SMALL LETTER O WITH HORN AND DOT BELOW
                    // LATIN CAPITAL LETTER U WITH DOT BELOW
                    // LATIN SMALL LETTER U WITH DOT BELOW
                    // LATIN CAPITAL LETTER U WITH HOOK ABOVE
                    // LATIN SMALL LETTER U WITH HOOK ABOVE
                    // LATIN CAPITAL LETTER U WITH HORN AND ACUTE
                    // LATIN SMALL LETTER U WITH HORN AND ACUTE
                    // LATIN CAPITAL LETTER U WITH HORN AND GRAVE
                    // LATIN SMALL LETTER U WITH HORN AND GRAVE
                    // LATIN CAPITAL LETTER U WITH HORN AND HOOK ABOVE
                    // LATIN SMALL LETTER U WITH HORN AND HOOK ABOVE
                    // LATIN CAPITAL LETTER U WITH HORN AND TILDE
                    // LATIN SMALL LETTER U WITH HORN AND TILDE
                    // LATIN CAPITAL LETTER U WITH HORN AND DOT BELOW
                    // LATIN SMALL LETTER U WITH HORN AND DOT BELOW
                    // LATIN CAPITAL LETTER Y WITH GRAVE
                    // LATIN SMALL LETTER Y WITH GRAVE
                    // LATIN CAPITAL LETTER Y WITH DOT BELOW
                    // LATIN SMALL LETTER Y WITH DOT BELOW
                    // LATIN CAPITAL LETTER Y WITH HOOK ABOVE
                    // LATIN SMALL LETTER Y WITH HOOK ABOVE
                    // LATIN CAPITAL LETTER Y WITH TILDE
                    // LATIN SMALL LETTER Y WITH TILDE
                    // SUPERSCRIPT LATIN SMALL LETTER I
                    // SUPERSCRIPT LATIN SMALL LETTER N
                    // KELVIN SIGN
                    // ANGSTROM SIGN
                    // SCRIPT CAPITAL B
                    // BLACK-LETTER CAPITAL C
                    // SCRIPT SMALL E
                    // SCRIPT CAPITAL E
                    // SCRIPT CAPITAL F
                    // TURNED CAPITAL F -- no decomposition
                    // SCRIPT CAPITAL M
                    // SCRIPT SMALL O
                    // ROTATED CAPITAL Q -- no decomposition
                    // TURNED SANS-SERIF CAPITAL G -- no decomposition
                    // TURNED SANS-SERIF CAPITAL L -- no decomposition
                    // REVERSED SANS-SERIF CAPITAL L -- no decomposition
                    // TURNED SANS-SERIF CAPITAL Y -- no decomposition
                    // DOUBLE-STRUCK ITALIC CAPITAL D
                    // DOUBLE-STRUCK ITALIC SMALL D
                    // DOUBLE-STRUCK ITALIC SMALL E
                    // DOUBLE-STRUCK ITALIC SMALL I
                    // DOUBLE-STRUCK ITALIC SMALL J
                    // LATIN SMALL LIGATURE FF
                    // LATIN SMALL LIGATURE FI
                    // LATIN SMALL LIGATURE FL
                    // LATIN SMALL LIGATURE FFI
                    // LATIN SMALL LIGATURE FFL
                    // LATIN SMALL LIGATURE LONG S T
                    // LATIN SMALL LIGATURE ST
                    // FULLWIDTH LATIN CAPITAL LETTER B
                    // FULLWIDTH LATIN CAPITAL LETTER B
                    // FULLWIDTH LATIN CAPITAL LETTER C
                    // FULLWIDTH LATIN CAPITAL LETTER D
                    // FULLWIDTH LATIN CAPITAL LETTER E
                    // FULLWIDTH LATIN CAPITAL LETTER F
                    // FULLWIDTH LATIN CAPITAL LETTER G
                    // FULLWIDTH LATIN CAPITAL LETTER H
                    // FULLWIDTH LATIN CAPITAL LETTER I
                    // FULLWIDTH LATIN CAPITAL LETTER J
                    // FULLWIDTH LATIN CAPITAL LETTER K
                    // FULLWIDTH LATIN CAPITAL LETTER L
                    // FULLWIDTH LATIN CAPITAL LETTER M
                    // FULLWIDTH LATIN CAPITAL LETTER N
                    // FULLWIDTH LATIN CAPITAL LETTER O
                    // FULLWIDTH LATIN CAPITAL LETTER P
                    // FULLWIDTH LATIN CAPITAL LETTER Q
                    // FULLWIDTH LATIN CAPITAL LETTER R
                    // FULLWIDTH LATIN CAPITAL LETTER S
                    // FULLWIDTH LATIN CAPITAL LETTER T
                    // FULLWIDTH LATIN CAPITAL LETTER U
                    // FULLWIDTH LATIN CAPITAL LETTER V
                    // FULLWIDTH LATIN CAPITAL LETTER W
                    // FULLWIDTH LATIN CAPITAL LETTER X
                    // FULLWIDTH LATIN CAPITAL LETTER Y
                    // FULLWIDTH LATIN CAPITAL LETTER Z
                    // FULLWIDTH LATIN SMALL LETTER A
                    // FULLWIDTH LATIN SMALL LETTER B
                    // FULLWIDTH LATIN SMALL LETTER C
                    // FULLWIDTH LATIN SMALL LETTER D
                    // FULLWIDTH LATIN SMALL LETTER E
                    // FULLWIDTH LATIN SMALL LETTER F
                    // FULLWIDTH LATIN SMALL LETTER G
                    // FULLWIDTH LATIN SMALL LETTER H
                    // FULLWIDTH LATIN SMALL LETTER I
                    // FULLWIDTH LATIN SMALL LETTER J
                    // FULLWIDTH LATIN SMALL LETTER K
                    // FULLWIDTH LATIN SMALL LETTER L
                    // FULLWIDTH LATIN SMALL LETTER M
                    // FULLWIDTH LATIN SMALL LETTER N
                    // FULLWIDTH LATIN SMALL LETTER O
                    // FULLWIDTH LATIN SMALL LETTER P
                    // FULLWIDTH LATIN SMALL LETTER Q
                    // FULLWIDTH LATIN SMALL LETTER R
                    // FULLWIDTH LATIN SMALL LETTER S
                    // FULLWIDTH LATIN SMALL LETTER T
                    // FULLWIDTH LATIN SMALL LETTER U
                    // FULLWIDTH LATIN SMALL LETTER V
                    // FULLWIDTH LATIN SMALL LETTER W
                    // FULLWIDTH LATIN SMALL LETTER X
                    // FULLWIDTH LATIN SMALL LETTER Y
                    // FULLWIDTH LATIN SMALL LETTER Z
                    // RUSSIAN CAPITAL LETTER А 
                    // RUSSIAN CAPITAL LETTER Б
                    // RUSSIAN CAPITAL LETTER В
                    // RUSSIAN CAPITAL LETTER Г
                    // RUSSIAN CAPITAL LETTER Д
                    // RUSSIAN CAPITAL LETTER Е
                    // RUSSIAN CAPITAL LETTER Ё
                    // RUSSIAN CAPITAL LETTER Ж
                    // RUSSIAN CAPITAL LETTER З
                    // RUSSIAN CAPITAL LETTER И
                    // RUSSIAN CAPITAL LETTER Й
                    // RUSSIAN CAPITAL LETTER К
                    // RUSSIAN CAPITAL LETTER Л
                    // RUSSIAN CAPITAL LETTER М
                    // RUSSIAN CAPITAL LETTER Н
                    // RUSSIAN CAPITAL LETTER О
                    // RUSSIAN CAPITAL LETTER П
                    // RUSSIAN CAPITAL LETTER Р
                    // RUSSIAN CAPITAL LETTER С
                    // RUSSIAN CAPITAL LETTER Т
                    // RUSSIAN CAPITAL LETTER У
                    // RUSSIAN CAPITAL LETTER Ф
                    // RUSSIAN CAPITAL LETTER Х
                    // RUSSIAN CAPITAL LETTER Ц
                    // RUSSIAN CAPITAL LETTER Ч
                    // RUSSIAN CAPITAL LETTER Ш
                    // RUSSIAN CAPITAL LETTER Щ
                    // RUSSIAN CAPITAL LETTER Ъ
                    // RUSSIAN CAPITAL LETTER Ы
                    // RUSSIAN CAPITAL LETTER Ь
                    // RUSSIAN CAPITAL LETTER Э
                    // RUSSIAN CAPITAL LETTER Ю
                    // RUSSIAN CAPITAL LETTER Я
                    // RUSSIAN SMALL LETTER а
                    // RUSSIAN SMALL LETTER б
                    // RUSSIAN SMALL LETTER в
                    // RUSSIAN SMALL LETTER г
                    // RUSSIAN SMALL LETTER д
                    // RUSSIAN SMALL LETTER е
                    // RUSSIAN SMALL LETTER ё
                    // RUSSIAN SMALL LETTER ж
                    // RUSSIAN SMALL LETTER з
                    // RUSSIAN SMALL LETTER и
                    // RUSSIAN SMALL LETTER й
                    // RUSSIAN SMALL LETTER к
                    // RUSSIAN SMALL LETTER л
                    // RUSSIAN SMALL LETTER м
                    // RUSSIAN SMALL LETTER н
                    // RUSSIAN SMALL LETTER о
                    // RUSSIAN SMALL LETTER п
                    // RUSSIAN SMALL LETTER р
                    // RUSSIAN SMALL LETTER с
                    // RUSSIAN SMALL LETTER т
                    // RUSSIAN SMALL LETTER у
                    // RUSSIAN SMALL LETTER ф
                    // RUSSIAN SMALL LETTER х
                    // RUSSIAN SMALL LETTER ц
                    // RUSSIAN SMALL LETTER ч
                    // RUSSIAN SMALL LETTER ш
                    // RUSSIAN SMALL LETTER щ
                    // RUSSIAN SMALL LETTER ъ
                    // RUSSIAN SMALL LETTER ы
                    // RUSSIAN SMALL LETTER ь
                    // RUSSIAN SMALL LETTER э
                    // RUSSIAN SMALL LETTER ю
                    // RUSSIAN SMALL LETTER я
                    // Ukraine-Byelorussian CAPITAL LETTER І
                    // Ukraine-Byelorussian SMALL LETTER і
                    // Ukraine CAPITAL LETTER Ї
                    // Ukraine SMALL LETTER ї
                    // Ukraine CAPITAL LETTER Є
                    // Ukraine SMALL LETTER є
                    // Ukraine CAPITAL LETTER Ґ
                    // Ukraine SMALL LETTER ґ
                    // Byelorussian CAPITAL LETTER Ў
                    // Byelorussian SMALL LETTER ў
                }
            }

        }

        /// <summary>
        /// Takes a hexadecimal string and converts it to an Unicode character
        /// </summary>
        /// <param name="hexString">A four-digit number in hex notation (eg, 00E7).</param>
        /// <returns>A unicode character, as string.</returns>
        private static string ToUnichar(string hexString)
        {
            var b = new byte[2];
            var ue = new UnicodeEncoding();

            // Take hexadecimal as text and make a Unicode char number
            b[0] = Convert.ToByte(hexString.Substring(2, 2), 16);
            b[1] = Convert.ToByte(hexString.Substring(0, 2), 16);
            // Get the character the number represents
            var returnChar = ue.GetString(b);
            return returnChar;
        }

        #endregion
    }
}
