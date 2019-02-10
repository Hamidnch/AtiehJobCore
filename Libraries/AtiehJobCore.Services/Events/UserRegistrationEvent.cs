using AtiehJobCore.Core.Domain.Users;

namespace AtiehJobCore.Services.Events
{
    public class UserRegistrationEvent<TC, TR>
        where TC : UserRegistrationResult where TR : UserRegistrationRequest
    {
        public UserRegistrationEvent(TC result, TR request)
        {
            Result = result;
            Request = request;
        }
        public TC Result { get; }

        public TR Request { get; }
    }
}
