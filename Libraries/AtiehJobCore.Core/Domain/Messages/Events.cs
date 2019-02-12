using AtiehJobCore.Core.MongoDb;
using DotLiquid;

namespace AtiehJobCore.Core.Domain.Messages
{
    public class EmailSubscribedEvent
    {
        public EmailSubscribedEvent(string email)
        {
            Email = email;
        }

        public string Email { get; }

        public bool Equals(EmailSubscribedEvent other)
        {
            if (ReferenceEquals(null, other))
                return false;
            return ReferenceEquals(this, other) || Equals(other.Email, Email);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof(EmailSubscribedEvent))
                return false;
            return Equals((EmailSubscribedEvent)obj);
        }

        public override int GetHashCode()
        {
            return (Email != null ? Email.GetHashCode() : 0);
        }
    }

    public class EmailUnsubscribedEvent
    {
        public EmailUnsubscribedEvent(string email)
        {
            Email = email;
        }

        public string Email { get; }

        public bool Equals(EmailUnsubscribedEvent other)
        {
            if (ReferenceEquals(null, other))
                return false;
            return ReferenceEquals(this, other) || Equals(other.Email, Email);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj.GetType() == typeof(EmailUnsubscribedEvent) && Equals((EmailUnsubscribedEvent)obj);
        }

        public override int GetHashCode()
        {
            return (Email != null ? Email.GetHashCode() : 0);
        }
    }

    /// <summary>
    /// A container for tokens that are added.
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public class EntityTokensAddedEvent<T> where T : ParentMongoEntity
    {
        public EntityTokensAddedEvent(T entity, Drop liquidDrop, LiquidObject liquidObject)
        {
            Entity = entity;
            LiquidDrop = liquidDrop;
            LiquidObject = liquidObject;
        }

        public T Entity { get; }
        public Drop LiquidDrop { get; }
        public LiquidObject LiquidObject { get; }
    }

    /// <summary>
    /// A container for tokens that are added.
    /// </summary>
    /// <typeparam name="U"></typeparam>
    public class MessageTokensAddedEvent
    {
        public MessageTokensAddedEvent(MessageTemplate message, LiquidObject liquidObject)
        {
            Message = message;
            LiquidObject = liquidObject;
        }

        public MessageTemplate Message { get; }
        public LiquidObject LiquidObject { get; }
    }
}
