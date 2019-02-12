using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using AtiehJobCore.Core.Contracts;
using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Services.Localization;
using AtiehJobCore.Web.Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

namespace AtiehJobCore.Web.Framework.UI.Paging
{
    /// <inheritdoc />
    /// <summary>
    /// Renders a pager component from an IPageableModel data source.
    /// </summary>
    public partial class Pager : IHtmlContent
    {
        protected readonly IPageableModel Model;
        protected readonly ViewContext VContext;
        protected string PgQueryName = "page";
        protected bool ShTotalSummary;
        protected bool ShPagerItems = true;
        protected bool ShFirst = true;
        protected bool ShPrevious = true;
        protected bool ShNext = true;
        protected bool ShLast = true;
        protected bool ShIndividualPages = true;
        protected bool RenEmptyParameters = true;
        protected int IndivPagesDisplayedCount = 5;
        protected IList<string> BoolParameterNames;

        public Pager(IPageableModel model, ViewContext context)
        {
            Model = model;
            VContext = context;
            BoolParameterNames = new List<string>();
        }

        protected ViewContext ViewContext => VContext;

        public Pager QueryParam(string value)
        {
            PgQueryName = value;
            return this;
        }
        public Pager ShowTotalSummary(bool value)
        {
            ShTotalSummary = value;
            return this;
        }
        public Pager ShowPagerItems(bool value)
        {
            ShPagerItems = value;
            return this;
        }
        public Pager ShowFirst(bool value)
        {
            ShFirst = value;
            return this;
        }
        public Pager ShowPrevious(bool value)
        {
            ShPrevious = value;
            return this;
        }
        public Pager ShowNext(bool value)
        {
            ShNext = value;
            return this;
        }
        public Pager ShowLast(bool value)
        {
            ShLast = value;
            return this;
        }
        public Pager ShowIndividualPages(bool value)
        {
            ShIndividualPages = value;
            return this;
        }
        public Pager RenderEmptyParameters(bool value)
        {
            RenEmptyParameters = value;
            return this;
        }
        public Pager IndividualPagesDisplayedCount(int value)
        {
            IndivPagesDisplayedCount = value;
            return this;
        }
        //little hack here due to ugly MVC implementation
        //find more info here: http://www.mindstorminteractive.com/topics/jquery-fix-asp-net-mvc-checkbox-truefalse-value/
        public Pager BooleanParameterName(string paramName)
        {
            BoolParameterNames.Add(paramName);
            return this;
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            var htmlString = GenerateHtmlString();
            writer.Write(htmlString);
        }
        public override string ToString()
        {
            return GenerateHtmlString();
        }
        public virtual string GenerateHtmlString()
        {
            if (Model.TotalItems == 0)
                return null;
            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();

            var links = new StringBuilder();
            if (ShTotalSummary && (Model.TotalPages > 0))
            {
                links.Append("<li class=\"total-summary\">");
                links.Append(string.Format(localizationService.GetResource("Pager.CurrentPage"), Model.PageIndex + 1, Model.TotalPages, Model.TotalItems));
                links.Append("</li>");
            }
            if (ShPagerItems && (Model.TotalPages > 1))
            {
                if (ShFirst)
                {
                    //first page
                    if ((Model.PageIndex >= 3) && (Model.TotalPages > IndivPagesDisplayedCount))
                    {
                        links.Append(CreatePageLink(1, localizationService.GetResource("Pager.First"), "first-page"));
                    }
                }
                if (ShPrevious)
                {
                    //previous page
                    if (Model.PageIndex > 0)
                    {
                        links.Append(CreatePageLink(Model.PageIndex, localizationService.GetResource("Pager.Previous"), "previous-page page-item"));
                    }
                }
                if (ShIndividualPages)
                {
                    //individual pages
                    var firstIndividualPageIndex = GetFirstIndividualPageIndex();
                    var lastIndividualPageIndex = GetLastIndividualPageIndex();
                    for (var i = firstIndividualPageIndex; i <= lastIndividualPageIndex; i++)
                    {
                        if (Model.PageIndex == i)
                        {
                            links.AppendFormat("<li class=\"current-page page-item\"><a class=\"page-link\">{0}</a></li>", (i + 1));
                        }
                        else
                        {
                            links.Append(CreatePageLink(i + 1, (i + 1).ToString(), "individual-page page-item"));
                        }
                    }
                }
                if (ShNext)
                {
                    //next page
                    if ((Model.PageIndex + 1) < Model.TotalPages)
                    {
                        links.Append(CreatePageLink(Model.PageIndex + 2, localizationService.GetResource("Pager.Next"), "next-page page-item"));
                    }
                }
                if (ShLast)
                {
                    //last page
                    if (((Model.PageIndex + 3) < Model.TotalPages) && (Model.TotalPages > IndivPagesDisplayedCount))
                    {
                        links.Append(CreatePageLink(Model.TotalPages, localizationService.GetResource("Pager.Last"), "last-page page-item"));
                    }
                }
            }

            var result = links.ToString();
            if (!string.IsNullOrEmpty(result))
            {
                result = "<ul class=\"pagination\">" + result + "</ul>";
            }
            return result;
        }
        public virtual bool IsEmpty()
        {
            var html = GenerateHtmlString();
            return string.IsNullOrEmpty(html);
        }

        protected virtual int GetFirstIndividualPageIndex()
        {
            if ((Model.TotalPages < IndivPagesDisplayedCount) ||
                ((Model.PageIndex - (IndivPagesDisplayedCount / 2)) < 0))
            {
                return 0;
            }
            if ((Model.PageIndex + (IndivPagesDisplayedCount / 2)) >= Model.TotalPages)
            {
                return (Model.TotalPages - IndivPagesDisplayedCount);
            }
            return (Model.PageIndex - (IndivPagesDisplayedCount / 2));
        }
        protected virtual int GetLastIndividualPageIndex()
        {
            var num = IndivPagesDisplayedCount / 2;
            if ((IndivPagesDisplayedCount % 2) == 0)
            {
                num--;
            }
            if ((Model.TotalPages < IndivPagesDisplayedCount) ||
                ((Model.PageIndex + num) >= Model.TotalPages))
            {
                return (Model.TotalPages - 1);
            }
            if ((Model.PageIndex - (IndivPagesDisplayedCount / 2)) < 0)
            {
                return (IndivPagesDisplayedCount - 1);
            }
            return (Model.PageIndex + num);
        }
        protected virtual string CreatePageLink(int pageNumber, string text, string cssClass)
        {
            var liBuilder = new TagBuilder("li");
            if (!string.IsNullOrWhiteSpace(cssClass))
                liBuilder.AddCssClass(cssClass);

            var aBuilder = new TagBuilder("a");
            aBuilder.InnerHtml.AppendHtml(text);
            aBuilder.AddCssClass("page-link");
            aBuilder.MergeAttribute("href", CreateDefaultUrl(pageNumber));

            liBuilder.InnerHtml.AppendHtml(aBuilder);
            return liBuilder.RenderHtmlContent();
        }
        protected virtual string CreateDefaultUrl(int pageNumber)
        {
            var routeValues = new RouteValueDictionary();

            var parametersWithEmptyValues = new List<string>();
            foreach (var key in VContext.HttpContext.Request.Query.Keys.Where(key => key != null))
            {
                //TODO test new implementation (QueryString, keys). And ensure no null exception is thrown when invoking ToString(). Is "StringValues.IsNullOrEmpty" required?
                var value = VContext.HttpContext.Request.Query[key].ToString();
                if (RenEmptyParameters && string.IsNullOrEmpty(value))
                {
                    //we store query string parameters with empty values separately
                    //we need to do it because they are not properly processed in the UrlHelper.GenerateUrl method (dropped for some reasons)
                    parametersWithEmptyValues.Add(key);
                }
                else
                {
                    if (BoolParameterNames.Contains(key, StringComparer.OrdinalIgnoreCase))
                    {
                        //little hack here due to ugly MVC implementation
                        //find more info here: http://www.mindstorminteractive.com/topics/jquery-fix-asp-net-mvc-checkbox-truefalse-value/
                        if (!string.IsNullOrEmpty(value) && value.Equals("true,false", StringComparison.OrdinalIgnoreCase))
                        {
                            value = "true";
                        }
                    }
                    routeValues[key] = value;
                }
            }

            if (pageNumber > 1)
            {
                routeValues[PgQueryName] = pageNumber;
            }
            else
            {
                //SEO. we do not render pageindex query string parameter for the first page
                if (routeValues.ContainsKey(PgQueryName))
                {
                    routeValues.Remove(PgQueryName);
                }
            }

            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            var url = webHelper.GetThisPageUrl(false);
            foreach (var routeValue in routeValues)
            {
                url = webHelper.ModifyQueryString(url, routeValue.Key + "=" + routeValue.Value, null);
            }

            if (!RenEmptyParameters || !parametersWithEmptyValues.Any())
            {
                return url;
            }

            {
                foreach (var key in parametersWithEmptyValues)
                {
                    url = webHelper.ModifyQueryString(url, key + "=", null);
                }
            }
            return url;
        }

    }
}
