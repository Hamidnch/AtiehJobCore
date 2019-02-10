﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AtiehJobCore.Web.Framework.Events
{
    /// <summary>
    /// Admin tabStrip created event
    /// </summary>
    public class AdminTabStripCreated
    {
        public AdminTabStripCreated(IHtmlHelper helper, string tabStripName)
        {
            this.Helper = helper;
            this.TabStripName = tabStripName;
            this.BlocksToRender = new List<IHtmlContent>();
        }

        public IHtmlHelper Helper { get; private set; }
        public string TabStripName { get; private set; }
        public IList<IHtmlContent> BlocksToRender { get; set; }
    }
}
