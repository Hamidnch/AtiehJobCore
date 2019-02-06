using AtiehJobCore.Common.Infrastructure;
using AtiehJobCore.Services.MongoDb.Events;
using FluentValidation;

namespace AtiehJobCore.Web.Framework.Validators
{
    public abstract class BaseMongoValidator<T> : AbstractValidator<T> where T : class
    {
        protected BaseMongoValidator()
        {
            PostInitialize();
        }

        /// <summary>
        /// Developers can override this method in custom partial classes
        /// in order to add some custom initialization code to constructors
        /// </summary>
        protected virtual void PostInitialize()
        {
            EngineContext.Current.Resolve<IEventPublisher>().Publish(this);
        }
    }
}
