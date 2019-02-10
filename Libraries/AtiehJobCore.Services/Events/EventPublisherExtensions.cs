using AtiehJobCore.Core.Events;
using AtiehJobCore.Core.MongoDb;

namespace AtiehJobCore.Services.Events
{
    public static class EventPublisherExtensions
    {
        public static void EntityInserted<T>(this IEventPublisher eventPublisher, T entity) where T : ParentMongoEntity
        {
            eventPublisher.Publish(new EntityInserted<T>(entity));
        }

        public static void EntityUpdated<T>(this IEventPublisher eventPublisher, T entity) where T : ParentMongoEntity
        {
            eventPublisher.Publish(new EntityUpdated<T>(entity));
        }

        public static void EntityDeleted<T>(this IEventPublisher eventPublisher, T entity) where T : ParentMongoEntity
        {
            eventPublisher.Publish(new EntityDeleted<T>(entity));
        }

    }
}
