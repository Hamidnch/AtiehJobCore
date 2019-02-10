using AtiehJobCore.Core.Infrastructure;
using AtiehJobCore.Services.Events;
using AtiehJobCore.Web.Framework.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace AtiehJobCore.Web.Framework.ViewComponents
{
    public abstract class BaseViewComponent : ViewComponent
    {
        public new ViewViewComponentResult View<TModel>(string viewName, TModel model)
        {
            EngineContext.Current.Resolve<IEventPublisher>().ViewComponentEvent(viewName, model, this);
            return base.View<TModel>(viewName, model);
        }

        public new ViewViewComponentResult View<TModel>(TModel model)
        {
            EngineContext.Current.Resolve<IEventPublisher>().ViewComponentEvent(model, this);
            return base.View<TModel>(model);
        }

        public new ViewViewComponentResult View(string viewName)
        {
            EngineContext.Current.Resolve<IEventPublisher>().ViewComponentEvent(viewName, this);
            return base.View(viewName);
        }

    }
}
