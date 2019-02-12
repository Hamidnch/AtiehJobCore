using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtiehJobCore.Web.Framework.Services;
using AtiehJobCore.Web.Framework.ViewComponents;
using Microsoft.AspNetCore.Mvc;

namespace AtiehJobCore.Web.Components
{
    public class TopicBlockViewComponent : BaseViewComponent
    {
        #region Fields
        private readonly ITopicViewModelService _topicViewModelService;
        #endregion

        #region Constructors

        public TopicBlockViewComponent(
            ITopicViewModelService topicViewModelService
        )
        {
            this._topicViewModelService = topicViewModelService;
        }

        #endregion

        #region Invoker

        public async Task<IViewComponentResult> InvokeAsync(string systemName)
        {
            var model = await Task.Run(() => _topicViewModelService.TopicBlock(systemName));
            if (model == null)
                return Content("");

            return View(model);
        }

        #endregion

    }
}
