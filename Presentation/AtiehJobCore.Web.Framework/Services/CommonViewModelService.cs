using AtiehJobCore.Core.Caching;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Domain.Common;
using AtiehJobCore.Core.Domain.Localization;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Services.Extensions;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Services.Media;
using AtiehJobCore.Services.Messages;
using AtiehJobCore.Web.Framework.Infrastructure.Cache;
using AtiehJobCore.Web.Framework.Models;
using AtiehJobCore.Web.Framework.Models.Common;
using AtiehJobCore.Web.Framework.Mvc.Captcha;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AtiehJobCore.Web.Framework.Services
{
    public partial class CommonViewModelService : ICommonViewModelService
    {
        private readonly ICacheManager _cacheManager;
        private readonly ILanguageService _languageService;
        private readonly IWorkContext _workContext;
        private readonly ILocalizationService _localizationService;
        private readonly IContactAttributeService _contactAttributeService;
        private readonly IContactAttributeParser _contactAttributeParser;
        private readonly CommonSettings _commonSettings;
        private readonly CaptchaSettings _captchaSettings;
        private readonly LocalizationSettings _localizationSettings;

        public CommonViewModelService(ICacheManager cacheManager,
            ILanguageService languageService,
            IWorkContext workContext,
            LocalizationSettings localizationSettings,
            CommonSettings commonSettings, CaptchaSettings captchaSettings,
            ILocalizationService localizationService,
            IContactAttributeService contactAttributeService,
            IContactAttributeParser contactAttributeParser)
        {
            _cacheManager = cacheManager;
            _languageService = languageService;
            _workContext = workContext;
            _localizationSettings = localizationSettings;
            _commonSettings = commonSettings;
            _captchaSettings = captchaSettings;
            _localizationService = localizationService;
            _contactAttributeService = contactAttributeService;
            _contactAttributeParser = contactAttributeParser;
        }

        public virtual LanguageSelectorModel PrepareLanguageSelector()
        {
            var availableLanguages = _cacheManager.Get(
                string.Format(ModelCacheEventConsumer.AvailableLanguagesModelKey),
                () =>
                {
                    var result = _languageService.GetAllLanguages()
                       .Select(x => new LanguageModel
                       {
                           Id = x.Id,
                           Name = x.Name,
                           FlagImageFileName = x.FlagImageFileName,
                       }).ToList();

                    return result;
                });

            var model = new LanguageSelectorModel
            {
                CurrentLanguageId = _workContext.WorkingLanguage.Id,
                AvailableLanguages = availableLanguages,
                UseImages = _localizationSettings.UseImagesForLanguageSelection
            };

            return model;
        }
        public virtual void SetLanguage(string langId)
        {
            var language = _languageService.GetLanguageById(langId);
            if (language != null && language.Published)
            {
                _workContext.WorkingLanguage = language;
            }
        }

        public ContactUsModel PrepareContactUs()
        {
            var model = new ContactUsModel
            {
                Email = _workContext.CurrentUser.Email,
                FullName = _workContext.CurrentUser.GetFullName(),
                SubjectEnabled = _commonSettings.SubjectFieldOnContactUsForm,
                DisplayCaptcha = _captchaSettings.Enabled && _captchaSettings.ShowOnContactUsPage,
                ContactAttributes = PrepareContactAttributeModel("")
            };
            return model;
        }

        public ContactUsModel SendContactUs(ContactUsModel model)
        {
            var email = model.Email.Trim();
            var fullName = model.FullName;
            var subject = _commonSettings.SubjectFieldOnContactUsForm ? model.Subject : null;
            var body = Core.Html.HtmlHelper.FormatText(model.Enquiry, false, true, false, false, false, false);

            model.SuccessfullySent = true;
            model.Result = _localizationService.GetResource("ContactUs.YourEnquiryHasBeenSent");

            return model;
        }

        public IList<ContactUsModel.ContactAttributeModel> PrepareContactAttributeModel(string selectedContactAttributes)
        {
            var model = new List<ContactUsModel.ContactAttributeModel>();

            var contactAttributes = _contactAttributeService.GetAllContactAttributes();
            foreach (var attribute in contactAttributes)
            {
                var attributeModel = new ContactUsModel.ContactAttributeModel
                {
                    Id = attribute.Id,
                    Name = attribute.GetLocalized(x => x.Name),
                    TextPrompt = attribute.GetLocalized(x => x.TextPrompt),
                    IsRequired = attribute.IsRequired,
                    AttributeControlType = attribute.AttributeControlType,
                    DefaultValue = attribute.DefaultValue
                };
                if (!string.IsNullOrEmpty(attribute.ValidationFileAllowedExtensions))
                {
                    attributeModel.AllowedFileExtensions = attribute.ValidationFileAllowedExtensions
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList();
                }

                if (attribute.ShouldHaveValues())
                {
                    //values
                    var attributeValues = attribute.ContactAttributeValues;
                    foreach (var attributeValue in attributeValues)
                    {
                        var attributeValueModel = new ContactUsModel.ContactAttributeValueModel
                        {
                            Id = attributeValue.Id,
                            Name = attributeValue.GetLocalized(x => x.Name),
                            ColorSquaresRgb = attributeValue.ColorSquaresRgb,
                            IsPreSelected = attributeValue.IsPreSelected,
                            DisplayOrder = attributeValue.DisplayOrder,
                        };
                        attributeModel.Values.Add(attributeValueModel);
                    }
                }

                switch (attribute.AttributeControlType)
                {
                    case AttributeControlType.DropdownList:
                    case AttributeControlType.RadioList:
                    case AttributeControlType.Checkboxes:
                    case AttributeControlType.ColorSquares:
                    case AttributeControlType.ImageSquares:
                        {
                            if (!string.IsNullOrEmpty(selectedContactAttributes))
                            {
                                //clear default selection
                                foreach (var item in attributeModel.Values)
                                    item.IsPreSelected = false;

                                //select new values
                                var selectedValues = _contactAttributeParser.ParseContactAttributeValues(selectedContactAttributes);
                                foreach (var attributeValue in selectedValues)
                                    if (attributeModel.Id == attributeValue.ContactAttributeId)
                                        foreach (var item in attributeModel.Values)
                                            if (attributeValue.Id == item.Id)
                                                item.IsPreSelected = true;
                            }
                        }
                        break;
                    case AttributeControlType.ReadonlyCheckboxes:
                        {
                            //do nothing
                            //values are already pre-set
                        }
                        break;
                    case AttributeControlType.TextBox:
                    case AttributeControlType.MultilineTextBox:
                        {
                            if (!string.IsNullOrEmpty(selectedContactAttributes))
                            {
                                var enteredText = _contactAttributeParser.ParseValues(selectedContactAttributes, attribute.Id);
                                if (enteredText.Any())
                                    attributeModel.DefaultValue = enteredText[0];
                            }
                        }
                        break;
                    case AttributeControlType.Datepicker:
                        {
                            //keep in mind my that the code below works only in the current culture
                            var selectedDateStr = _contactAttributeParser.ParseValues(selectedContactAttributes, attribute.Id);
                            if (selectedDateStr.Any())
                            {
                                if (DateTime.TryParseExact(selectedDateStr[0], "D", CultureInfo.CurrentCulture,
                                                       DateTimeStyles.None, out var selectedDate))
                                {
                                    //successfully parsed
                                    attributeModel.SelectedDay = selectedDate.Day;
                                    attributeModel.SelectedMonth = selectedDate.Month;
                                    attributeModel.SelectedYear = selectedDate.Year;
                                }
                            }

                        }
                        break;
                    case AttributeControlType.FileUpload:
                        {
                            if (!string.IsNullOrEmpty(selectedContactAttributes))
                            {
                                var downloadGuidStr = _contactAttributeParser.ParseValues(selectedContactAttributes, attribute.Id).FirstOrDefault();
                                Guid.TryParse(downloadGuidStr, out var downloadGuid);
                                var download = EngineContext.Current.Resolve<IDownloadService>().GetDownloadByGuid(downloadGuid);
                                if (download != null)
                                    attributeModel.DefaultValue = download.DownloadGuid.ToString();
                            }
                        }
                        break;
                    default:
                        break;
                }

                model.Add(attributeModel);
            }

            return model;
        }
        public virtual string ParseContactAttributes(IFormCollection form)
        {

            if (form == null)
                throw new ArgumentNullException(nameof(form));

            var attributesXml = "";
            var checkoutAttributes = _contactAttributeService.GetAllContactAttributes();
            foreach (var attribute in checkoutAttributes)
            {
                var controlId = $"contact_attribute_{attribute.Id}";
                switch (attribute.AttributeControlType)
                {
                    case AttributeControlType.DropdownList:
                    case AttributeControlType.RadioList:
                    case AttributeControlType.ColorSquares:
                    case AttributeControlType.ImageSquares:
                        {
                            var ctrlAttributes = form[controlId];
                            if (!string.IsNullOrEmpty(ctrlAttributes))
                            {
                                attributesXml = _contactAttributeParser.AddContactAttribute(attributesXml,
                                        attribute, ctrlAttributes);

                            }
                        }
                        break;
                    case AttributeControlType.Checkboxes:
                        {
                            var cblAttributes = form[controlId].ToString();
                            if (!string.IsNullOrEmpty(cblAttributes))
                            {
                                attributesXml = cblAttributes.Split(new[] { ',' },
                                    StringSplitOptions.RemoveEmptyEntries).Aggregate(attributesXml,
                                    (current, item) => _contactAttributeParser.AddContactAttribute(current, attribute, item));
                            }
                        }
                        break;
                    case AttributeControlType.ReadonlyCheckboxes:
                        {
                            //load read-only (already server-side selected) values
                            var attributeValues = attribute.ContactAttributeValues;
                            foreach (var selectedAttributeId in attributeValues
                                .Where(v => v.IsPreSelected)
                                .Select(v => v.Id)
                                .ToList())
                            {
                                attributesXml = _contactAttributeParser.AddContactAttribute(attributesXml,
                                            attribute, selectedAttributeId.ToString());
                            }
                        }
                        break;
                    case AttributeControlType.TextBox:
                    case AttributeControlType.MultilineTextBox:
                        {
                            var ctrlAttributes = form[controlId].ToString();
                            if (!string.IsNullOrEmpty(ctrlAttributes))
                            {
                                var enteredText = ctrlAttributes.Trim();
                                attributesXml = _contactAttributeParser.AddContactAttribute(attributesXml,
                                    attribute, enteredText);
                            }
                        }
                        break;
                    case AttributeControlType.Datepicker:
                        {
                            var date = form[controlId + "_day"];
                            var month = form[controlId + "_month"];
                            var year = form[controlId + "_year"];
                            DateTime? selectedDate = null;
                            try
                            {
                                selectedDate = new DateTime(int.Parse(year), int.Parse(month), int.Parse(date));
                            }
                            catch { }
                            if (selectedDate.HasValue)
                            {
                                attributesXml = _contactAttributeParser.AddContactAttribute(attributesXml,
                                    attribute, selectedDate.Value.ToString("D"));
                            }
                        }
                        break;
                    case AttributeControlType.FileUpload:
                        {
                            Guid.TryParse(form[controlId], out var downloadGuid);
                            var downloadService = EngineContext.Current.Resolve<IDownloadService>();
                            var download = downloadService.GetDownloadByGuid(downloadGuid);
                            if (download != null)
                            {
                                attributesXml = _contactAttributeParser.AddContactAttribute(attributesXml,
                                           attribute, download.DownloadGuid.ToString());
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            //save checkout attributes
            //validate conditional attributes (if specified)
            foreach (var attribute in checkoutAttributes)
            {
                var conditionMet = _contactAttributeParser.IsConditionMet(attribute, attributesXml);
                if (conditionMet.HasValue && !conditionMet.Value)
                    attributesXml = _contactAttributeParser.RemoveContactAttribute(attributesXml, attribute);
            }

            return attributesXml;
        }
        public virtual IList<string> GetContactAttributesWarnings(string contactAttributesXml)
        {
            var warnings = new List<string>();

            //selected attributes
            var attributes1 = _contactAttributeParser.ParseContactAttributes(contactAttributesXml);

            //existing checkout attributes
            var attributes2 = _contactAttributeService.GetAllContactAttributes();
            foreach (var a2 in attributes2)
            {
                if (!a2.IsRequired)
                {
                    continue;
                }

                var found = false;
                //selected checkout attributes
                foreach (var a1 in attributes1)
                {
                    if (a1.Id != a2.Id)
                    {
                        continue;
                    }

                    var attributeValuesStr = _contactAttributeParser.ParseValues(contactAttributesXml, a1.Id);
                    if (attributeValuesStr.Any(str1 => !string.IsNullOrEmpty(str1.Trim())))
                    {
                        found = true;
                    }
                }

                //if not found
                if (!found)
                {
                    if (!string.IsNullOrEmpty(a2.GetLocalized(a => a.TextPrompt)))
                        warnings.Add(a2.GetLocalized(a => a.TextPrompt));
                    else
                        warnings.Add(string.Format(
                            _localizationService.GetResource("ContactUs.SelectAttribute"), a2.GetLocalized(a => a.Name)));
                }
            }

            //now validation rules

            //minimum length
            foreach (var ca in attributes2)
            {
                if (ca.ValidationMinLength.HasValue)
                {
                    if (ca.AttributeControlType == AttributeControlType.TextBox ||
                        ca.AttributeControlType == AttributeControlType.MultilineTextBox)
                    {
                        var valuesStr = _contactAttributeParser.ParseValues(contactAttributesXml, ca.Id);
                        var enteredText = valuesStr.FirstOrDefault();
                        var enteredTextLength = string.IsNullOrEmpty(enteredText) ? 0 : enteredText.Length;

                        if (ca.ValidationMinLength.Value > enteredTextLength)
                        {
                            if (ca.ValidationMinLength != null)
                            {
                                warnings.Add(string.Format(
                                    _localizationService.GetResource("ContactUs.TextboxMinimumLength"),
                                    ca.GetLocalized(a => a.Name), ca.ValidationMinLength.Value));
                            }
                        }
                    }
                }

                //maximum length
                if (!ca.ValidationMaxLength.HasValue)
                {
                    continue;
                }

                {
                    if (ca.AttributeControlType != AttributeControlType.TextBox &&
                        ca.AttributeControlType != AttributeControlType.MultilineTextBox)
                    {
                        continue;
                    }

                    var valuesStr = _contactAttributeParser.ParseValues(contactAttributesXml, ca.Id);
                    var enteredText = valuesStr.FirstOrDefault();
                    var enteredTextLength = string.IsNullOrEmpty(enteredText) ? 0 : enteredText.Length;

                    if (ca.ValidationMaxLength.Value >= enteredTextLength)
                    {
                        continue;
                    }

                    if (ca.ValidationMaxLength != null)
                    {
                        warnings.Add(string.Format(
                            _localizationService.GetResource("ContactUs.TextboxMaximumLength"),
                            ca.GetLocalized(a => a.Name), ca.ValidationMaxLength.Value));
                    }
                }
            }

            return warnings;
        }
    }
}
