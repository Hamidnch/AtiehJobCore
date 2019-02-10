using AtiehJobCore.Core.Domain.Users;

namespace AtiehJobCore.Services.Events
{
    public static class WebEventsExtensions
    {

        public static void UserRegistrationEvent<TC, TR>
            (this IEventPublisher eventPublisher, TC result, TR request)
            where TC : UserRegistrationResult where TR : UserRegistrationRequest
        {
            eventPublisher.Publish(new UserRegistrationEvent<TC, TR>(result, request));
        }
    }
}
