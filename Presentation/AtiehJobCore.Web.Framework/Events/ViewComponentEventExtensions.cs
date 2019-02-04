using AtiehJobCore.Services.MongoDb.Events;
using AtiehJobCore.Web.Framework.Components;

namespace AtiehJobCore.Web.Framework.Events
{
    public static class ViewComponentEventExtensions
    {
        public static void ViewComponentEvent<T, TU>(this IEventPublisher eventPublisher,
            string viewName, TU component) where TU : BaseViewComponent
        {
            eventPublisher.Publish(new ViewComponentEvent<T, TU>(viewName, component));
        }

        public static void ViewComponentEvent<T, TU>(this IEventPublisher eventPublisher,
            T entity, TU component) where TU : BaseViewComponent
        {
            eventPublisher.Publish(new ViewComponentEvent<T, TU>(entity, component));
        }

        public static void ViewComponentEvent<T, TU>(this IEventPublisher eventPublisher,
            string viewName, T entity, TU component) where TU : BaseViewComponent
        {
            eventPublisher.Publish(new ViewComponentEvent<T, TU>(viewName, entity, component));
        }
    }
}
