using AtiehJobCore.Core.Caching;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Extensions;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Services.Seo;
using AtiehJobCore.Services.Topics;
using AtiehJobCore.Web.Framework.Infrastructure.Cache;
using AtiehJobCore.Web.Framework.Localization;
using AtiehJobCore.Web.Framework.Models;
using AtiehJobCore.Web.Framework.Models.Common;
using AtiehJobCore.Web.Framework.UI.Paging;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;

namespace AtiehJobCore.Web.Framework.Infrastructure.Extensions
{
    public static class HtmlExtensions
    {
        #region Admin area extensions

        public static IHtmlContent LocalizedEditor<T, TLocalizedModelLocal>(this IHtmlHelper<T> helper,
            string name,
            Func<int, HelperResult> localizedTemplate,
            Func<T, HelperResult> standardTemplate,
            bool ignoreIfSeveralStores = false)
            where T : ILocalizedModel<TLocalizedModelLocal>
            where TLocalizedModelLocal : ILocalizedModelLocal
        {
            var localizationSupported = helper.ViewData.Model.Locales.Count > 1;
            if (localizationSupported)
            {
                var tabStrip = new StringBuilder();
                tabStrip.AppendLine($"<div id='{name}'>");
                tabStrip.AppendLine("<ul>");

                //default tab
                tabStrip.AppendLine("<li class='k-state-active'>");
                tabStrip.AppendLine("Standard");
                tabStrip.AppendLine("</li>");

                foreach (var locale in helper.ViewData.Model.Locales)
                {
                    //languages
                    var language = EngineContext.Current.Resolve<ILanguageService>().GetLanguageById(locale.LanguageId);

                    tabStrip.AppendLine("<li>");
                    var urlHelper = new UrlHelper(helper.ViewContext);
                    var iconUrl = urlHelper.Content("~/content/images/flags/" + language.FlagImageFileName);
                    tabStrip.AppendLine($"<img class='k-image' alt='' src='{iconUrl}'>");
                    tabStrip.AppendLine(WebUtility.HtmlEncode(language.Name));
                    tabStrip.AppendLine("</li>");
                }
                tabStrip.AppendLine("</ul>");



                //default tab
                tabStrip.AppendLine("<div>");
                tabStrip.AppendLine(standardTemplate(helper.ViewData.Model).ToHtmlString());
                tabStrip.AppendLine("</div>");

                for (var i = 0; i < helper.ViewData.Model.Locales.Count; i++)
                {
                    //languages
                    tabStrip.AppendLine("<div>");
                    tabStrip.AppendLine(localizedTemplate(i).ToHtmlString());
                    tabStrip.AppendLine("</div>");
                }
                tabStrip.AppendLine("</div>");
                tabStrip.AppendLine("<script>");
                tabStrip.AppendLine("$(document).ready(function() {");
                tabStrip.AppendLine($"$('#{name}').kendoTabStrip(");
                tabStrip.AppendLine("{");
                tabStrip.AppendLine("animation:  {");
                tabStrip.AppendLine("open: {");
                tabStrip.AppendLine("effects: \"fadeIn\"");
                tabStrip.AppendLine("}");
                tabStrip.AppendLine("}");
                tabStrip.AppendLine("});");
                tabStrip.AppendLine("});");
                tabStrip.AppendLine("</script>");
                return new HtmlString(tabStrip.ToString());
            }
            else
            {
                return standardTemplate(helper.ViewData.Model);
            }
        }

        public static IHtmlContent DeleteConfirmation<T>(this IHtmlHelper<T> helper,
            string buttonsSelector) where T : BaseMongoEntityModel
        {
            return DeleteConfirmation(helper, "", buttonsSelector);
        }

        public static IHtmlContent DeleteConfirmation<T>(this IHtmlHelper<T> helper, string actionName,
                    string buttonsSelector) where T : BaseMongoEntityModel
        {
            if (string.IsNullOrEmpty(actionName))
                actionName = "Delete";

            var modalId = new HtmlString(helper.ViewData.ModelMetadata.ModelType.Name.ToLower()
                                       + "-delete-confirmation").ToHtmlString();


            var deleteConfirmationModel = new DeleteConfirmationModel
            {
                Id = helper.ViewData.Model.Id,
                ControllerName = helper.ViewContext.RouteData.Values["controller"].ToString(),
                ActionName = actionName,
                WindowId = modalId
            };

            var window = new StringBuilder();
            window.AppendLine($"<div id='{modalId}' style='display:none;'>");
            window.AppendLine(helper.PartialAsync("Delete",
                deleteConfirmationModel).Result.ToHtmlString());
            window.AppendLine("</div>");
            window.AppendLine("<script>");
            window.AppendLine("$(document).ready(function() {");
            window.AppendLine($"$('#{buttonsSelector}').click(function (e) ");
            window.AppendLine("{");
            window.AppendLine("e.preventDefault();");
            window.AppendLine($"var window = $('#{modalId}');");
            window.AppendLine("if (!window.data('kendoWindow')) {");
            window.AppendLine("window.kendoWindow({");
            window.AppendLine("modal: true,");
            window.AppendLine(
                $"title: '{EngineContext.Current.Resolve<ILocalizationService>().GetResource("Admin.Common.AreYouSure")}',");
            window.AppendLine("actions: ['Close']");
            window.AppendLine("});");
            window.AppendLine("}");
            window.AppendLine("window.data('kendoWindow').center().open();");
            window.AppendLine("});");
            window.AppendLine("});");
            window.AppendLine("</script>");

            return new HtmlString(window.ToString());
        }

        public static string FieldIdFor<T, TResult>(this IHtmlHelper<T> html,
            Expression<Func<T, TResult>> expression)
        {
            //TO DO remove this method and use in cshtml files
            return html.IdFor(expression);
        }

        public static string RenderHtmlContent(this IHtmlContent htmlContent)
        {
            using (var writer = new StringWriter())
            {
                htmlContent.WriteTo(writer, HtmlEncoder.Default);
                var htmlOutput = writer.ToString();
                return htmlOutput;
            }
        }
        #endregion

        #region Common extensions

        public static string ToHtmlString(this IHtmlContent tag)
        {
            using (var writer = new StringWriter())
            {
                tag.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }

        /// <summary>
        /// Creates a days, months, years drop down list using an HTML select control. 
        /// The parameters represent the value of the "name" attribute on the select control.
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="dayName">"Name" attribute of the day drop down list.</param>
        /// <param name="monthName">"Name" attribute of the month drop down list.</param>
        /// <param name="yearName">"Name" attribute of the year drop down list.</param>
        /// <param name="beginYear">Begin year</param>
        /// <param name="endYear">End year</param>
        /// <param name="selectedDay">Selected day</param>
        /// <param name="selectedMonth">Selected month</param>
        /// <param name="selectedYear">Selected year</param>
        /// <param name="localizeLabels">Localize labels</param>
        /// <param name="htmlAttributes">HTML attributes</param>
        /// <returns></returns>
        public static IHtmlContent DatePickerDropDowns(this IHtmlHelper html,
            string dayName, string monthName, string yearName,
            int? beginYear = null, int? endYear = null,
            int? selectedDay = null, int? selectedMonth = null, int? selectedYear = null,
            bool localizeLabels = true, object htmlAttributes = null, bool wrapTags = false)
        {
            var daysList = new TagBuilder("select");
            var monthsList = new TagBuilder("select");
            var yearsList = new TagBuilder("select");

            daysList.Attributes.Add("name", dayName);
            monthsList.Attributes.Add("name", monthName);
            yearsList.Attributes.Add("name", yearName);

            var htmlAttributesDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            daysList.MergeAttributes(htmlAttributesDictionary, true);
            monthsList.MergeAttributes(htmlAttributesDictionary, true);
            yearsList.MergeAttributes(htmlAttributesDictionary, true);

            var days = new StringBuilder();
            var months = new StringBuilder();
            var years = new StringBuilder();

            string dayLocale, monthLocale, yearLocale;
            if (localizeLabels)
            {
                var locService = EngineContext.Current.Resolve<ILocalizationService>();
                dayLocale = locService.GetResource("Common.Day");
                monthLocale = locService.GetResource("Common.Month");
                yearLocale = locService.GetResource("Common.Year");
            }
            else
            {
                dayLocale = "Day";
                monthLocale = "Month";
                yearLocale = "Year";
            }

            days.AppendFormat("<option value='{0}'>{1}</option>", "0", dayLocale);
            for (var i = 1; i <= 31; i++)
                days.AppendFormat("<option value='{0}'{1}>{0}</option>", i,
                    (selectedDay.HasValue && selectedDay.Value == i) ? " selected=\"selected\"" : null);


            months.AppendFormat("<option value='{0}'>{1}</option>", "0", monthLocale);
            for (var i = 1; i <= 12; i++)
            {
                months.AppendFormat("<option value='{0}'{1}>{2}</option>",
                                    i,
                                    (selectedMonth.HasValue && selectedMonth.Value == i) ? " selected=\"selected\"" : null,
                                    CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(i));
            }


            years.AppendFormat("<option value='{0}'>{1}</option>", "0", yearLocale);

            if (beginYear == null)
                beginYear = DateTime.UtcNow.Year - 100;
            if (endYear == null)
                endYear = DateTime.UtcNow.Year;

            if (endYear > beginYear)
            {
                for (var i = beginYear.Value; i <= endYear.Value; i++)
                    years.AppendFormat("<option value='{0}'{1}>{0}</option>", i,
                        (selectedYear.HasValue && selectedYear.Value == i) ? " selected=\"selected\"" : null);
            }
            else
            {
                for (var i = beginYear.Value; i >= endYear.Value; i--)
                    years.AppendFormat("<option value='{0}'{1}>{0}</option>", i,
                        (selectedYear.HasValue && selectedYear.Value == i) ? " selected=\"selected\"" : null);
            }

            daysList.InnerHtml.AppendHtml(days.ToString());
            monthsList.InnerHtml.AppendHtml(months.ToString());
            yearsList.InnerHtml.AppendHtml(years.ToString());

            if (wrapTags)
            {
                var wrapDaysList = "<span class=\"days-list select-wrapper\">" + daysList.RenderHtmlContent() + "</span>";
                var wrapMonthsList = "<span class=\"months-list select-wrapper\">" + monthsList.RenderHtmlContent() + "</span>";
                var wrapYearsList = "<span class=\"years-list select-wrapper\">" + yearsList.RenderHtmlContent() + "</span>";

                return new HtmlString(string.Concat(wrapDaysList, wrapMonthsList, wrapYearsList));
            }
            else
            {
                return new HtmlString(string.Concat(daysList.RenderHtmlContent(), monthsList.RenderHtmlContent(), yearsList.RenderHtmlContent()));
            }

        }
        #endregion

        /// <summary>
        /// BBCode editor
        /// </summary>
        /// <typeparam name="TModel">Model</typeparam>
        /// <param name="html">HTML Helper</param>
        /// <param name="name">Name</param>
        /// <returns>Editor</returns>
        public static IHtmlContent BbCodeEditor<TModel>(this IHtmlHelper<TModel> html, string name)
        {
            var sb = new StringBuilder();

            var storeLocation = EngineContext.Current.Resolve<IWebHelper>().GetLocation();
            var bbEditorWebRoot = $"{storeLocation}content/";

            sb.AppendFormat("<script src=\"{0}content/bbeditor/ed.js\" ></script>", storeLocation);
            sb.Append(Environment.NewLine);
            sb.Append("<script language=\"javascript\" type=\"text/javascript\">");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("edToolbar('{0}','{1}');", name, bbEditorWebRoot);
            sb.Append(Environment.NewLine);
            sb.Append("</script>");
            sb.Append(Environment.NewLine);

            return new HtmlString(sb.ToString());
        }

        //we have two pagers:
        //The first one can have custom routes
        //The second one just adds query string parameter
        public static IHtmlContent Pager<TModel>(this IHtmlHelper<TModel> html, PagerModel model)
        {
            if (model.TotalRecords == 0)
                return null;

            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();

            var links = new StringBuilder();
            if (model.ShowTotalSummary && (model.TotalPages > 0))
            {
                links.Append("<li class=\"total-summary page-item\">");
                links.Append(string.Format(model.CurrentPageText, model.PageIndex + 1, model.TotalPages, model.TotalRecords));
                links.Append("</li>");
            }
            if (model.ShowPagerItems && (model.TotalPages > 1))
            {
                if (model.ShowFirst)
                {
                    //first page
                    if ((model.PageIndex >= 3) && (model.TotalPages > model.IndividualPagesDisplayedCount))
                    {
                        model.RouteValues.PageNumber = 1;

                        links.Append("<li class=\"first-page page-item\">");
                        links.Append(model.UseRouteLinks
                            ? html.RouteLink(model.FirstButtonText, model.RouteActionName, model.RouteValues,
                                new
                                {
                                    title = localizationService.GetResource("Pager.FirstPageTitle"),
                                    @class = "page-link"
                                }).ToHtmlString()
                            : html.ActionLink(model.FirstButtonText, model.RouteActionName, model.RouteValues,
                                new
                                {
                                    title = localizationService.GetResource("Pager.FirstPageTitle"),
                                    @class = "page-link"
                                }).ToHtmlString());
                        links.Append("</li>");
                    }
                }
                if (model.ShowPrevious)
                {
                    //previous page
                    if (model.PageIndex > 0)
                    {
                        model.RouteValues.PageNumber = (model.PageIndex);

                        links.Append("<li class=\"previous-page page-item\">");
                        links.Append(model.UseRouteLinks
                            ? html.RouteLink(model.PreviousButtonText, model.RouteActionName, model.RouteValues,
                                new
                                {
                                    title = localizationService.GetResource("Pager.PreviousPageTitle"),
                                    @class = "page-link"
                                }).ToHtmlString()
                            : html.ActionLink(model.PreviousButtonText, model.RouteActionName, model.RouteValues,
                                new
                                {
                                    title = localizationService.GetResource("Pager.PreviousPageTitle"),
                                    @class = "page-link"
                                }).ToHtmlString());
                        links.Append("</li>");
                    }
                }
                if (model.ShowIndividualPages)
                {
                    //individual pages
                    int firstIndividualPageIndex = model.GetFirstIndividualPageIndex();
                    int lastIndividualPageIndex = model.GetLastIndividualPageIndex();
                    for (var i = firstIndividualPageIndex; i <= lastIndividualPageIndex; i++)
                    {
                        if (model.PageIndex == i)
                        {
                            links.AppendFormat("<li class=\"current-page page-item\"><a class=\"page-link\">{0}</a></li>", (i + 1));
                        }
                        else
                        {
                            model.RouteValues.PageNumber = (i + 1);

                            links.Append("<li class=\"individual-page page-item\">");
                            links.Append(model.UseRouteLinks
                                ? html.RouteLink((i + 1).ToString(), model.RouteActionName, model.RouteValues,
                                    new
                                    {
                                        title = string.Format(localizationService.GetResource("Pager.PageLinkTitle"),
                                            (i + 1)),
                                        @class = "page-link"
                                    }).ToHtmlString()
                                : html.ActionLink((i + 1).ToString(), model.RouteActionName, model.RouteValues,
                                    new
                                    {
                                        title = string.Format(localizationService.GetResource("Pager.PageLinkTitle"),
                                            (i + 1)),
                                        @class = "page-link"
                                    }).ToHtmlString());
                            links.Append("</li>");
                        }
                    }
                }
                if (model.ShowNext)
                {
                    //next page
                    if ((model.PageIndex + 1) < model.TotalPages)
                    {
                        model.RouteValues.PageNumber = (model.PageIndex + 2);

                        links.Append("<li class=\"next-page page-item\">");
                        links.Append(model.UseRouteLinks
                            ? html.RouteLink(model.NextButtonText, model.RouteActionName, model.RouteValues,
                                new
                                {
                                    title = localizationService.GetResource("Pager.NextPageTitle"),
                                    @class = "page-link"
                                }).ToHtmlString()
                            : html.ActionLink(model.NextButtonText, model.RouteActionName, model.RouteValues,
                                new
                                {
                                    title = localizationService.GetResource("Pager.NextPageTitle"),
                                    @class = "page-link"
                                }).ToHtmlString());
                        links.Append("</li>");
                    }
                }
                if (model.ShowLast)
                {
                    //last page
                    if (((model.PageIndex + 3) < model.TotalPages) && (model.TotalPages > model.IndividualPagesDisplayedCount))
                    {
                        model.RouteValues.PageNumber = model.TotalPages;

                        links.Append("<li class=\"last-page page-item\">");
                        links.Append(model.UseRouteLinks
                            ? html.RouteLink(model.LastButtonText, model.RouteActionName, model.RouteValues,
                                new
                                {
                                    title = localizationService.GetResource("Pager.LastPageTitle"),
                                    @class = "page-link"
                                }).ToHtmlString()
                            : html.ActionLink(model.LastButtonText, model.RouteActionName, model.RouteValues,
                                new
                                {
                                    title = localizationService.GetResource("Pager.LastPageTitle"),
                                    @class = "page-link"
                                }).ToHtmlString());
                        links.Append("</li>");
                    }
                }
            }
            var result = links.ToString();
            if (!string.IsNullOrEmpty(result))
            {
                result = "<ul class=\"pagination\">" + result + "</ul>";
            }
            return new HtmlString(result);
        }

        public static Pager Pager(this IHtmlHelper helper, IPageableModel pagination)
        {
            return new Pager(pagination, helper.ViewContext);
        }

        /// <summary>
        /// Get topic system name
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="html">HTML helper</param>
        /// <param name="systemName">System name</param>
        /// <returns>Topic SEO Name</returns>
        public static string GetTopicSeName<T>(this IHtmlHelper<T> html, string systemName)
        {
            var workContext = EngineContext.Current.Resolve<IWorkContext>();

            //static cache manager
            var cacheManager = EngineContext.Current.Resolve<ICacheManager>();
            var cacheKey = string.Format(ModelCacheEventConsumer.TopicSeNameBySystemName, systemName, workContext.WorkingLanguage.Id);
            var cachedSeName = cacheManager.Get(cacheKey, () =>
            {
                var topicService = EngineContext.Current.Resolve<ITopicService>();
                var topic = topicService.GetTopicBySystemName(systemName);
                return topic == null ? "" : topic.GetSeName();
            });
            return cachedSeName;
        }
    }
}
