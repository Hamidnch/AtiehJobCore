using AtiehJobCore.Core;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Events;
using MongoDB.Driver.Linq;
using System.Linq;

namespace AtiehJobCore.Services.Users
{
    public partial class UserApiService : IUserApiService
    {
        #region Fields

        private readonly IRepository<UserApi> _userRepository;
        private readonly IEventPublisher _eventPublisher;

        #endregion
        public UserApiService(IRepository<UserApi> userRepository, IEventPublisher eventPublisher)
        {
            this._userRepository = userRepository;
            this._eventPublisher = eventPublisher;
        }

        /// <inheritdoc />
        /// <summary>
        /// Get user api by id
        /// </summary>
        /// <param name="id">id</param>
        public virtual UserApi GetUserById(string id)
        {
            return _userRepository.GetById(id);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get user api by email
        /// </summary>
        /// <param name="email">id</param>
        public virtual UserApi GetUserByEmail(string email)
        {
            return _userRepository.Table.FirstOrDefault(x => x.Email == email.ToLowerInvariant());
        }

        /// <inheritdoc />
        /// <summary>
        /// Insert user api
        /// </summary>
        /// <param name="userApi">User api</param>
        public virtual void InsertUserApi(UserApi userApi)
        {
            _userRepository.Insert(userApi);

            //event notification
            _eventPublisher.EntityInserted(userApi);
        }

        /// <inheritdoc />
        /// <summary>
        /// Update user api
        /// </summary>
        /// <param name="userApi">User api</param>
        public virtual void UpdateUserApi(UserApi userApi)
        {
            _userRepository.Update(userApi);

            //event notification
            _eventPublisher.EntityUpdated(userApi);
        }

        /// <inheritdoc />
        /// <summary>
        /// Delete user api
        /// </summary>
        /// <param name="userApi">User api</param>
        public virtual void DeleteUserApi(UserApi userApi)
        {
            _userRepository.Delete(userApi);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get users api
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>

        public virtual IPagedList<UserApi> GetUsers(string email = "", int pageIndex = 0, int pageSize = 2147483647)
        {
            var query = _userRepository.Table;
            if (!string.IsNullOrEmpty(email))
                query = query.Where(x => x.Email.Contains(email.ToLowerInvariant()));

            var users = new PagedList<UserApi>(query, pageIndex, pageSize);
            return users;
        }
    }
}
