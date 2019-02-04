using AtiehJobCore.Web.Framework.Components;

namespace AtiehJobCore.Web.Framework.Events
{
    public class ViewComponentEvent<T, TU> where TU : BaseViewComponent
    {
        public ViewComponentEvent(string viewName, TU viewComponent)
        {
            ViewName = viewName;
            VComponent = viewComponent;
        }

        public ViewComponentEvent(string viewName, T model, TU viewVComponent)
        {
            ViewName = viewName;
            Model = model;
            VComponent = viewVComponent;
        }

        public ViewComponentEvent(T model, TU viewComponent)
        {
            Model = model;
            VComponent = viewComponent;
        }

        public T Model { get; }

        public TU VComponent { get; }
        public string ViewName { get; }
    }
}
