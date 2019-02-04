﻿using AtiehJobCore.Common.MongoDb.Domain.Seo;
using AtiehJobCore.Services.Seo;
using AtiehJobCore.Web.Framework.UI;
using BundlerMinifier;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AtiehJobCore.Web.Framework.Services
{
    /// <inheritdoc />
    /// <summary>
    /// Page head builder
    /// </summary>
    public partial class PageHeadBuilder : IPageHeadBuilder
    {
        #region Fields

        private static readonly object SLock = new object();

        private readonly SeoSettings _seoSettings;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly BundleFileProcessor _bundleFileProcessor;

        private readonly List<string> _titleParts;
        private readonly List<string> _metaDescriptionParts;
        private readonly List<string> _metaKeywordParts;
        private readonly Dictionary<ResourceLocation, List<ScriptReferenceMeta>> _scriptParts;
        private readonly Dictionary<ResourceLocation, List<CssReferenceMeta>> _cssParts;
        private readonly List<string> _canonicalUrlParts;
        private readonly List<string> _headCustomParts;
        private readonly List<string> _pageCssClassParts;
        private string _editPageUrl;
        private string _activeAdminMenuSystemName;

        #endregion

        #region Ctor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="seoSettings">SEO settings</param>
        /// <param name="hostingEnvironment">Hosting environment</param>
        public PageHeadBuilder(SeoSettings seoSettings, IHostingEnvironment hostingEnvironment)
        {
            this._seoSettings = seoSettings;
            this._hostingEnvironment = hostingEnvironment;
            this._bundleFileProcessor = new BundleFileProcessor();

            this._titleParts = new List<string>();
            this._metaDescriptionParts = new List<string>();
            this._metaKeywordParts = new List<string>();
            this._scriptParts = new Dictionary<ResourceLocation, List<ScriptReferenceMeta>>();
            this._cssParts = new Dictionary<ResourceLocation, List<CssReferenceMeta>>();
            this._canonicalUrlParts = new List<string>();
            this._headCustomParts = new List<string>();
            this._pageCssClassParts = new List<string>();
        }

        #endregion

        #region Utilities

        protected virtual string GetBundleFileNameBasingOnModifyTime(string[] filePaths)
        {
            if (filePaths == null || filePaths.Length == 0)
                throw new ArgumentException("parts");

            var ticks = filePaths.Where(File.Exists).Select(filePath => File.GetLastWriteTimeUtc(filePath).Ticks);

            //calculate hash
            var hash = "";
            using (var sha = SHA256.Create())
            {
                // string concatenation
                var hashInput = "";
                foreach (var tick in ticks)
                {
                    hashInput += tick;
                    hashInput += ",";
                }

                var input = sha.ComputeHash(Encoding.Unicode.GetBytes(hashInput));
                hash = WebEncoders.Base64UrlEncode(input);
            }
            //ensure only valid chars
            hash = SeoExtensions.GetSeName(hash);
            return hash;
        }

        #endregion

        #region Methods

        public virtual void AddTitleParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            _titleParts.Add(part);
        }
        public virtual void AppendTitleParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            _titleParts.Insert(0, part);
        }
        public virtual string GenerateTitle(bool addDefaultTitle)
        {
            var result = "";
            var specificTitle = string.Join(_seoSettings.PageTitleSeparator, _titleParts.AsEnumerable().Reverse().ToArray());
            if (!string.IsNullOrEmpty(specificTitle))
            {
                if (addDefaultTitle)
                {
                    //store name + page title
                    switch (_seoSettings.PageTitleSeoAdjustment)
                    {
                        case PageTitleSeoAdjustment.PagenameAfterSiteName:
                            {
                                result = string.Join(_seoSettings.PageTitleSeparator, _seoSettings.DefaultTitle, specificTitle);
                            }
                            break;
                        case PageTitleSeoAdjustment.SiteNameAfterPagename:
                        default:
                            {
                                result = string.Join(_seoSettings.PageTitleSeparator, specificTitle, _seoSettings.DefaultTitle);
                            }
                            break;
                    }
                }
                else
                {
                    //page title only
                    result = specificTitle;
                }
            }
            else
            {
                //store name only
                result = _seoSettings.DefaultTitle;
            }
            return result;
        }
        public virtual void AddMetaDescriptionParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            _metaDescriptionParts.Add(part);
        }
        public virtual void AppendMetaDescriptionParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            _metaDescriptionParts.Insert(0, part);
        }
        public virtual string GenerateMetaDescription()
        {
            var metaDescription = string.Join(", ", _metaDescriptionParts.AsEnumerable().Reverse().ToArray());
            var result = !string.IsNullOrEmpty(metaDescription) ? metaDescription : _seoSettings.DefaultMetaDescription;
            return result;
        }
        public virtual void AddMetaKeywordParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            _metaKeywordParts.Add(part);
        }
        public virtual void AppendMetaKeywordParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            _metaKeywordParts.Insert(0, part);
        }
        public virtual string GenerateMetaKeywords()
        {
            var metaKeyword = string.Join(", ", _metaKeywordParts.AsEnumerable().Reverse().ToArray());
            var result = !string.IsNullOrEmpty(metaKeyword) ? metaKeyword : _seoSettings.DefaultMetaKeywords;
            return result;
        }
        public virtual void AddScriptParts(ResourceLocation location, string src, string debugSrc, bool excludeFromBundle, bool isAsync)
        {
            if (!_scriptParts.ContainsKey(location))
                _scriptParts.Add(location, new List<ScriptReferenceMeta>());

            if (string.IsNullOrEmpty(src))
                return;

            if (string.IsNullOrEmpty(debugSrc))
                debugSrc = src;

            _scriptParts[location].Add(new ScriptReferenceMeta
            {
                ExcludeFromBundle = excludeFromBundle,
                IsAsync = isAsync,
                Src = src,
                DebugSrc = debugSrc
            });
        }
        public virtual void AppendScriptParts(ResourceLocation location, string src, string debugSrc, bool excludeFromBundle, bool isAsync)
        {
            if (!_scriptParts.ContainsKey(location))
                _scriptParts.Add(location, new List<ScriptReferenceMeta>());

            if (string.IsNullOrEmpty(src))
                return;

            if (string.IsNullOrEmpty(debugSrc))
                debugSrc = src;

            _scriptParts[location].Insert(0, new ScriptReferenceMeta
            {
                ExcludeFromBundle = excludeFromBundle,
                IsAsync = isAsync,
                Src = src,
                DebugSrc = debugSrc
            });
        }
        public virtual string GenerateScripts(IUrlHelper urlHelper, ResourceLocation location, bool? bundleFiles = null)
        {
            if (!_scriptParts.ContainsKey(location) || _scriptParts[location] == null)
                return "";

            if (!_scriptParts.Any())
                return "";

            var debugModel = _hostingEnvironment.IsDevelopment();

            if (!bundleFiles.HasValue)
            {
                //use setting if no value is specified
                bundleFiles = _seoSettings.EnableJsBundling;
            }

            if (bundleFiles.Value)
            {
                var partsToBundle = _scriptParts[location]
                    .Where(x => !x.ExcludeFromBundle)
                    .Distinct()
                    .ToArray();
                var partsToDontBundle = _scriptParts[location]
                    .Where(x => x.ExcludeFromBundle)
                    .Distinct()
                    .ToArray();

                var result = new StringBuilder();

                //parts to  bundle
                if (partsToBundle.Any())
                {
                    //ensure \bundles directory exists
                    Directory.CreateDirectory(Path.Combine(_hostingEnvironment.WebRootPath, "bundles"));

                    var srcPath = string.Empty;

                    var bundle = new Bundle();
                    foreach (var item in partsToBundle)
                    {
                        var src = debugModel ? item.DebugSrc : item.Src;
                        src = urlHelper.Content(src);
                        //check whether this file exists. 
                        srcPath = Path.Combine(_hostingEnvironment.ContentRootPath,
                            Common.Infrastructure.OperatingSystem.IsWindows()
                                ? src.Remove(0, 1).Replace("/", "\\") : src.Remove(0, 1));

                        if (File.Exists(srcPath))
                        {
                            //remove starting /
                            src = src.Remove(0, 1);
                            if (File.Exists(srcPath))
                                item.FilePath = srcPath;
                        }
                        else
                        {
                            //if not, it should be stored into /wwwroot directory
                            src = "wwwroot/" + src;
                            srcPath = Path.Combine(_hostingEnvironment.ContentRootPath, Common.Infrastructure.OperatingSystem.IsWindows()
                                ? src.Replace("/", "\\").Replace("\\\\", "\\") : src);
                            if (File.Exists(srcPath))
                                item.FilePath = srcPath;
                        }
                        bundle.InputFiles.Add(src);
                    }
                    //output file name
                    var outputFileName = GetBundleFileNameBasingOnModifyTime(partsToBundle.Select(x => x.FilePath).ToArray());
                    bundle.OutputFileName = "wwwroot/bundles/" + outputFileName + ".js";

                    //save
                    var configFilePath = _hostingEnvironment.ContentRootPath + (Common.Infrastructure.OperatingSystem.IsWindows()
                                             ? "\\" : "/") + outputFileName + ".json";
                    bundle.FileName = configFilePath;

                    var bundleDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "bundles");
                    var filePaths = Directory.EnumerateFiles(bundleDirectory);
                    var fileNames = filePaths.Select(x => x.Substring(x.LastIndexOf((Common.Infrastructure.OperatingSystem.IsWindows()
                                                                          ? "\\" : "/"), StringComparison.Ordinal) + 1));

                    if (!fileNames.Any(x => x.Contains(outputFileName)))
                    {
                        lock (SLock)
                        {
                            //process
                            _bundleFileProcessor.Process(configFilePath, new List<Bundle> { bundle });
                        }
                    }

                    //render
                    result.AppendFormat("<script src=\"{0}\"></script>",
                        File.Exists(bundleDirectory + (Common.Infrastructure.OperatingSystem.IsWindows() ? "\\" : "/") +
                                    outputFileName + ".min.js")
                            ? urlHelper.Content("~/bundles/" + outputFileName + ".min.js")
                            : urlHelper.Content("~/bundles/" + outputFileName + ".js"));

                    result.Append(Environment.NewLine);
                }


                //parts to not bundle
                foreach (var item in partsToDontBundle)
                {
                    var src = debugModel ? item.DebugSrc : item.Src;
                    result.AppendFormat("<script {1} src=\"{0}\"></script>", urlHelper.Content(src), item.IsAsync ? "async" : "");
                    result.Append(Environment.NewLine);
                }
                return result.ToString();
            }
            else
            {
                //bundling is disabled
                var result = new StringBuilder();
                foreach (var item in _scriptParts[location].Distinct())
                {
                    var src = debugModel ? item.DebugSrc : item.Src;
                    result.AppendFormat("<script {1} src=\"{0}\"></script>", urlHelper.Content(src), item.IsAsync ? "async " : "");
                    result.Append(Environment.NewLine);
                }
                return result.ToString();
            }
        }
        public virtual void AddCssFileParts(ResourceLocation location, string src, string debugSrc, bool excludeFromBundle = false)
        {
            if (!_cssParts.ContainsKey(location))
                _cssParts.Add(location, new List<CssReferenceMeta>());

            if (string.IsNullOrEmpty(src))
                return;

            if (string.IsNullOrEmpty(debugSrc))
                debugSrc = src;

            _cssParts[location].Add(new CssReferenceMeta
            {
                ExcludeFromBundle = excludeFromBundle,
                Src = src,
                DebugSrc = debugSrc
            });
        }
        public virtual void AppendCssFileParts(ResourceLocation location, string src, string debugSrc, bool excludeFromBundle = false)
        {
            if (!_cssParts.ContainsKey(location))
                _cssParts.Add(location, new List<CssReferenceMeta>());

            if (string.IsNullOrEmpty(src))
                return;

            if (string.IsNullOrEmpty(debugSrc))
                debugSrc = src;

            _cssParts[location].Insert(0, new CssReferenceMeta
            {
                ExcludeFromBundle = excludeFromBundle,
                Src = src,
                DebugSrc = debugSrc
            });
        }
        public virtual string GenerateCssFiles(IUrlHelper urlHelper, ResourceLocation location, bool? bundleFiles = null)
        {
            if (!_cssParts.ContainsKey(location) || _cssParts[location] == null)
                return "";

            if (!_cssParts.Any())
                return "";


            var debugModel = _hostingEnvironment.IsDevelopment();

            if (!bundleFiles.HasValue)
            {
                //use setting if no value is specified
                bundleFiles = _seoSettings.EnableCssBundling;
            }

            //CSS bundling is not allowed in virtual directories
            if (urlHelper.ActionContext.HttpContext.Request.PathBase.HasValue)
                bundleFiles = false;

            if (bundleFiles.Value)
            {
                var partsToBundle = _cssParts[location]
                    .Where(x => !x.ExcludeFromBundle)
                    .Distinct()
                    .ToArray();
                var partsToDontBundle = _cssParts[location]
                    .Where(x => x.ExcludeFromBundle)
                    .Distinct()
                    .ToArray();

                var result = new StringBuilder();

                //parts to  bundle
                if (partsToBundle.Any())
                {
                    //ensure \bundles directory exists
                    Directory.CreateDirectory(Path.Combine(_hostingEnvironment.WebRootPath, "bundles"));

                    var bundle = new Bundle();
                    foreach (var item in partsToBundle)
                    {
                        var src = debugModel ? item.DebugSrc : item.Src;
                        src = urlHelper.Content(src);
                        //check whether this file exists 
                        var srcPath = Common.Infrastructure.OperatingSystem.IsWindows() ?
                            Path.Combine(_hostingEnvironment.ContentRootPath, src.Remove(0, 1).Replace("/", "\\")) :
                            Path.Combine(_hostingEnvironment.ContentRootPath, src.Remove(0, 1));

                        if (File.Exists(srcPath))
                        {
                            //remove starting /
                            src = src.Remove(0, 1);
                            if (File.Exists(srcPath))
                                item.FilePath = srcPath;
                        }
                        else
                        {
                            //if not, it should be stored into /wwwroot directory
                            src = "wwwroot" + src;
                            srcPath = Path.Combine(_hostingEnvironment.ContentRootPath, Common.Infrastructure.OperatingSystem.IsWindows()
                                ? src.Replace("/", "\\").Replace("\\\\", "\\") : src);

                            if (File.Exists(srcPath))
                                item.FilePath = srcPath;
                        }
                        bundle.InputFiles.Add(src);
                    }

                    //output file name
                    var outputFileName = GetBundleFileNameBasingOnModifyTime(partsToBundle.Select(x => x.FilePath).ToArray());
                    bundle.OutputFileName = "wwwroot/bundles/" + outputFileName + ".css";

                    //save
                    var configFilePath = _hostingEnvironment.ContentRootPath + (Common.Infrastructure.OperatingSystem.IsWindows()
                                             ? "\\" : "/") + outputFileName + ".json";
                    bundle.FileName = configFilePath;

                    var bundleDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "bundles");
                    var filePaths = Directory.EnumerateFiles(bundleDirectory);
                    var fileNames = filePaths.Select(x => x.Substring(x.LastIndexOf((Common.Infrastructure.OperatingSystem.IsWindows()
                                                                          ? "\\" : "/"), StringComparison.Ordinal) + 1));

                    if (!fileNames.Any(x => x.Contains(outputFileName)))
                    {
                        lock (SLock)
                        {
                            //process
                            _bundleFileProcessor.Process(configFilePath, new List<Bundle> { bundle });
                        }
                    }
                    //render
                    result.AppendFormat("<link href=\"{0}\" rel=\"stylesheet\" type=\"{1}\" />",
                        File.Exists(bundleDirectory + (Common.Infrastructure.OperatingSystem.IsWindows()
                                        ? "\\"
                                        : "/") + outputFileName + ".min.css")
                            ? urlHelper.Content("~/bundles/" + outputFileName + ".min.css")
                            : urlHelper.Content("~/bundles/" + outputFileName + ".css"), "text/css");

                    result.Append(Environment.NewLine);
                }

                //parts not to bundle
                foreach (var item in partsToDontBundle)
                {
                    var src = debugModel ? item.DebugSrc : item.Src;
                    result.AppendFormat("<link href=\"{0}\" rel=\"stylesheet\" type=\"{1}\" />", urlHelper.Content(src), "text/css");
                    result.Append(Environment.NewLine);
                }
                return result.ToString();
            }
            else
            {
                //bundling is disabled
                var result = new StringBuilder();
                foreach (var item in _cssParts[location].Distinct())
                {
                    var src = debugModel ? item.DebugSrc : item.Src;
                    result.AppendFormat("<link href=\"{0}\" rel=\"stylesheet\" type=\"{1}\" />", urlHelper.Content(src), "text/css");
                    result.AppendLine();
                }
                return result.ToString();
            }
        }
        public virtual void AddCanonicalUrlParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            _canonicalUrlParts.Add(part);
        }
        public virtual void AppendCanonicalUrlParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            _canonicalUrlParts.Insert(0, part);
        }
        public virtual string GenerateCanonicalUrls()
        {
            var result = new StringBuilder();
            foreach (var canonicalUrl in _canonicalUrlParts)
            {
                result.AppendFormat("<link rel=\"canonical\" href=\"{0}\" />", canonicalUrl);
                result.Append(Environment.NewLine);
            }
            return result.ToString();
        }
        public virtual void AddHeadCustomParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            _headCustomParts.Add(part);
        }
        public virtual void AppendHeadCustomParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            _headCustomParts.Insert(0, part);
        }
        public virtual string GenerateHeadCustom()
        {
            //use only distinct rows
            var distinctParts = _headCustomParts.Distinct().ToList();
            if (!distinctParts.Any())
                return "";

            var result = new StringBuilder();
            foreach (var path in distinctParts)
            {
                result.Append(path);
                result.Append(Environment.NewLine);
            }
            return result.ToString();
        }
        public virtual void AddPageCssClassParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            _pageCssClassParts.Add(part);
        }
        public virtual void AppendPageCssClassParts(string part)
        {
            if (string.IsNullOrEmpty(part))
                return;

            _pageCssClassParts.Insert(0, part);
        }
        public virtual string GeneratePageCssClasses()
        {
            var result = string.Join(" ", _pageCssClassParts.AsEnumerable().Reverse().ToArray());
            return result;
        }
        /// <inheritdoc />
        /// <summary>
        /// Specify "edit page" URL
        /// </summary>
        /// <param name="url">URL</param>
        public virtual void AddEditPageUrl(string url)
        {
            _editPageUrl = url;
        }

        /// <inheritdoc />
        /// <summary>
        /// Get "edit page" URL
        /// </summary>
        /// <returns>URL</returns>
        public virtual string GetEditPageUrl()
        {
            return _editPageUrl;
        }

        /// <inheritdoc />
        /// <summary>
        /// Specify system name of admin menu item that should be selected (expanded)
        /// </summary>
        /// <param name="systemName">System name</param>
        public virtual void SetActiveMenuItemSystemName(string systemName)
        {
            _activeAdminMenuSystemName = systemName;
        }

        /// <inheritdoc />
        /// <summary>
        /// Get system name of admin menu item that should be selected (expanded)
        /// </summary>
        /// <returns>System name</returns>
        public virtual string GetActiveMenuItemSystemName()
        {
            return _activeAdminMenuSystemName;
        }

        #endregion

        #region Nested classes

        private class ScriptReferenceMeta
        {
            public bool ExcludeFromBundle { get; set; }

            public bool IsAsync { get; set; }

            public string Src { get; set; }

            public string DebugSrc { get; set; }

            public string FilePath { get; set; }
        }

        private class CssReferenceMeta
        {
            public bool ExcludeFromBundle { get; set; }

            public string Src { get; set; }

            public string DebugSrc { get; set; }

            public string FilePath { get; set; }
        }
        #endregion
    }
}
