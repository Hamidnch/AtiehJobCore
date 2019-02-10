using AtiehJobCore.Core.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace AtiehJobCore.Services.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Event subscription service
    /// </summary>
    public class SubscriptionService : ISubscriptionService
    {
        /// <inheritdoc />
        /// <summary>
        /// Get subscriptions
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Event consumers</returns>
        public IList<IConsumer<T>> GetSubscriptions<T>()
        {
            return EngineContext.Current.ResolveAll<IConsumer<T>>().ToList();
        }
    }
}
