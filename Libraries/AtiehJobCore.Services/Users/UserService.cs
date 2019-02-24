using AtiehJobCore.Core;
using AtiehJobCore.Core.Caching;
using AtiehJobCore.Core.Domain.Users;
using AtiehJobCore.Core.Enums;
using AtiehJobCore.Core.Extensions;
using AtiehJobCore.Core.MongoDb.Data;
using AtiehJobCore.Services.Events;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtiehJobCore.Services.Users
{
    public class UserService : IUserService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// </remarks>
        private const string UserRolesAllKey = "AtiehJob.userrole.all-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : system name
        /// </remarks>
        private const string UserRolesBySystemNameKey = "AtiehJob.userrole.systemname-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string UserRolesPatternKey = "AtiehJob.userrole.";

        #endregion

        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserHistoryPassword> _userHistoryPasswordRepository;
        private readonly IRepository<UserNote> _userNoteRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IEventPublisher _eventPublisher;

        public UserService(IRepository<User> userRepository,
            IRepository<Role> roleRepository, ICacheManager cacheManager,
            IEventPublisher eventPublisher,
            IRepository<UserHistoryPassword> userHistoryPasswordRepository,
            IRepository<UserNote> userNoteRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _cacheManager = cacheManager;
            _eventPublisher = eventPublisher;
            _userHistoryPasswordRepository = userHistoryPasswordRepository;
            _userNoteRepository = userNoteRepository;
        }

        public IPagedList<User> GetAllUsers(DateTime? createdFromUtc = null,
            DateTime? createdToUtc = null, string[] userRoleIds = null,
            string email = null, string username = null, string firstName = null,
            string lastName = null, string company = null,
            string phone = null, string zipPostalCode = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _userRepository.Table;

            if (createdFromUtc.HasValue)
                query = query.Where(c => createdFromUtc.Value <= c.CreatedOnUtc);
            if (createdToUtc.HasValue)
                query = query.Where(c => createdToUtc.Value >= c.CreatedOnUtc);
            query = query.Where(c => !c.Deleted);
            if (userRoleIds != null && userRoleIds.Length > 0)
                query = query.Where(c => c.Roles.Any(x => userRoleIds.Contains(x.Id)));

            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(c => c.Email != null && c.Email.Contains(email.ToLower()));

            if (!string.IsNullOrWhiteSpace(username))
                query = query.Where(c => c.Username != null
                                      && c.Username.ToLower().Contains(username.ToLower()));

            query = query.OrderByDescending(c => c.CreatedOnUtc);

            var users = new PagedList<User>(query, pageIndex, pageSize);
            return users;
        }

        public IList<User> GetAllUsersByPasswordFormat(PasswordFormat passwordFormat)
        {
            var passwordFormatId = (int)passwordFormat;

            var query = _userRepository.Table;
            query = query.Where(c => c.PasswordFormatId == passwordFormatId);
            query = query.OrderByDescending(c => c.CreatedOnUtc);
            var users = query.ToList();
            return users;
        }

        public IPagedList<User> GetOnlineUsers(DateTime lastActivityFromUtc,
            string[] userRoleIds, int pageIndex = 0,
            int pageSize = int.MaxValue)
        {
            var query = _userRepository.Table;
            query = query.Where(c => lastActivityFromUtc <= c.LastActivityDateUtc);
            query = query.Where(c => !c.Deleted);
            if (userRoleIds != null && userRoleIds.Length > 0)
                query = query.Where(c => c.Roles.Select(cr => cr.Id).Intersect(userRoleIds).Any());

            query = query.OrderByDescending(c => c.LastActivityDateUtc);
            var users = new PagedList<User>(query, pageIndex, pageSize);
            return users;
        }

        public void DeleteUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (user.IsSystemAccount)
                throw new AtiehJobException(
                    $"System user account ({user.SystemName}) could not be deleted");

            user.Deleted = true;
            user.Email = $"DELETED@{DateTime.UtcNow.Ticks}.COM";
            user.Username = user.Email;

            //delete generic attr
            user.GenericAttributes.Clear();
            //delete user roles
            user.Roles.Clear();

            //update user
            _userRepository.Update(user);
        }

        public User GetUserById(string userId)
        {
            return _userRepository.GetById(userId);
        }

        public IList<User> GetUsersByIds(string[] userIds)
        {
            if (userIds == null || userIds.Length == 0)
                return new List<User>();

            var query = from c in _userRepository.Table
                        where userIds.Contains(c.Id)
                        select c;
            var users = query.ToList();
            //sort by passed identifiers
            var sortedUsers = new List<User>();
            foreach (var id in userIds)
            {
                var user = users.Find(x => x.Id == id);
                if (user != null)
                    sortedUsers.Add(user);
            }
            return sortedUsers;
        }

        public User GetUserByGuid(Guid userGuid)
        {
            if (userGuid == Guid.Empty)
                return null;

            var filter = Builders<User>.Filter.Eq(x => x.UserGuid, userGuid);
            return _userRepository.Collection.Find(filter).FirstOrDefault();
        }

        public User GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;
            var filter = Builders<User>.Filter.Eq(x => x.Email, email.ToLower());
            return _userRepository.Collection.Find(filter).FirstOrDefault();
        }

        public User GetUserBySystemName(string systemName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
                return null;

            var filter = Builders<User>.Filter.Eq(x => x.SystemName, systemName);
            return _userRepository.Collection.Find(filter).FirstOrDefault();
        }

        public User GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var filter = Builders<User>.Filter.Eq(x => x.Username, username.ToLower());
            return _userRepository.Collection.Find(filter).FirstOrDefault();
        }
        public User GetUserByMobileNumber(string mobileNumber)
        {
            if (string.IsNullOrWhiteSpace(mobileNumber))
                return null;

            var filter = Builders<User>.Filter.Eq(x => x.MobileNumber, mobileNumber.ToLower());
            return _userRepository.Collection.Find(filter).FirstOrDefault();
        }
        public User GetUserByNationalCode(string nationalCode)
        {
            if (string.IsNullOrWhiteSpace(nationalCode))
                return null;

            var filter = Builders<User>.Filter.Eq(x => x.NationalCode, nationalCode.ToLower());
            return _userRepository.Collection.Find(filter).FirstOrDefault();
        }
        public User InsertGuestUser(string urlReferrer = "")
        {
            var user = new User
            {
                UserGuid = Guid.NewGuid(),
                Active = true,
                CreatedOnUtc = DateTime.UtcNow,
                LastActivityDateUtc = DateTime.UtcNow,
                UrlReferrer = urlReferrer
            };

            //add to 'Guests' role
            var guestRole = GetUserRoleBySystemName(SystemUserRoleNames.Guests);
            if (guestRole == null)
                throw new AtiehJobException("'Guests' role could not be loaded");
            user.Roles.Add(guestRole);

            _userRepository.Insert(user);

            return user;
        }

        public void InsertUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (!string.IsNullOrEmpty(user.Email))
                user.Email = user.Email.ToLower();

            if (!string.IsNullOrEmpty(user.Username))
                user.Username = user.Username.ToLower();

            _userRepository.Insert(user);

            //event notification
            _eventPublisher.EntityInserted(user);
        }

        public void UpdateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var builder = Builders<User>.Filter;
            var filter = builder.Eq(x => x.Id, user.Id);
            var update = Builders<User>.Update
                .Set(x => x.Email, string.IsNullOrEmpty(user.Email) ? "" : user.Email.ToLower())
                .Set(x => x.PasswordFormatId, user.PasswordFormatId)
                .Set(x => x.PasswordSalt, user.PasswordSalt)
                .Set(x => x.Active, user.Active)
                .Set(x => x.Password, user.Password)
                .Set(x => x.PasswordChangeDateUtc, user.PasswordChangeDateUtc)
                .Set(x => x.Username, string.IsNullOrEmpty(user.Username) ? "" : user.Username.ToLower())
                .Set(x => x.Deleted, user.Deleted)
                .Set(x => x.Jobseekers, user.Jobseekers)
                .Set(x => x.MobileNumber, user.MobileNumber)
                .Set(x => x.NationalCode, user.NationalCode);

            var result = _userRepository.Collection.UpdateOneAsync(filter, update).Result;

            //event notification
            _eventPublisher.EntityUpdated(user);
        }

        public void UpdateUserLastActivityDate(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var builder = Builders<User>.Filter;
            var filter = builder.Eq(x => x.Id, user.Id);
            var update = Builders<User>.Update
               .Set(x => x.LastActivityDateUtc, user.LastActivityDateUtc);
            var result = _userRepository.Collection.UpdateOneAsync(filter, update).Result;
        }

        public void UpdateUserPassword(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var builder = Builders<User>.Filter;
            var filter = builder.Eq(x => x.Id, user.Id);
            var update = Builders<User>.Update
               .Set(x => x.Password, user.Password);
            var result = _userRepository.Collection.UpdateOneAsync(filter, update).Result;
        }

        public void UpdateActive(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            var builder = Builders<User>.Filter;
            var filter = builder.Eq(x => x.Id, user.Id);
            var update = Builders<User>.Update
               .Set(x => x.Active, user.Active);

            var result = _userRepository.Collection.UpdateOneAsync(filter, update).Result;
        }

        public void UpdateUserLastLoginDate(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var builder = Builders<User>.Filter;
            var filter = builder.Eq(x => x.Id, user.Id);
            var update = Builders<User>.Update
               .Set(x => x.LastLoginDateUtc, user.LastLoginDateUtc)
               .Set(x => x.FailedLoginAttempts, user.FailedLoginAttempts)
               .Set(x => x.CannotLoginUntilDateUtc, user.CannotLoginUntilDateUtc);

            var result = _userRepository.Collection.UpdateOneAsync(filter, update).Result;
        }

        public void UpdateUserLastIpAddress(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var builder = Builders<User>.Filter;
            var filter = builder.Eq(x => x.Id, user.Id);
            var update = Builders<User>.Update
               .Set(x => x.LastIpAddress, user.LastIpAddress);
            var result = _userRepository.Collection.UpdateOneAsync(filter, update).Result;
        }

        public void UpdateUserInAdminPanel(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var builder = Builders<User>.Filter;
            var filter = builder.Eq(x => x.Id, user.Id);
            var update = Builders<User>.Update
               .Set(x => x.Active, user.Active)
               .Set(x => x.AdminComment, user.AdminComment)
               .Set(x => x.IsSystemAccount, user.IsSystemAccount)
               .Set(x => x.Active, user.Active)
               .Set(x => x.Email, string.IsNullOrEmpty(user.Email) ? "" : user.Email.ToLower())
               .Set(x => x.Password, user.Password)
               .Set(x => x.SystemName, user.SystemName)
               .Set(x => x.Username, string.IsNullOrEmpty(user.Username) ? "" : user.Username.ToLower())
               .Set(x => x.Roles, user.Roles);

            var result = _userRepository.Collection.UpdateOneAsync(filter, update).Result;
            //event notification
            _eventPublisher.EntityUpdated(user);
        }

        public int DeleteGuestUsers(DateTime? createdFromUtc, DateTime? createdToUtc)
        {
            var guestRole = GetUserRoleBySystemName(SystemUserRoleNames.Guests);
            if (guestRole == null)
                throw new AtiehJobException("'Guests' role could not be loaded");

            var builder = Builders<User>.Filter;
            var filter = builder.ElemMatch(x => x.Roles, role => role.Id == guestRole.Id);

            if (createdFromUtc.HasValue)
                filter = filter & builder.Gte(x => x.LastActivityDateUtc, createdFromUtc.Value);
            if (createdToUtc.HasValue)
                filter = filter & builder.Lte(x => x.LastActivityDateUtc, createdToUtc.Value);

            var users = _userRepository.Collection.DeleteMany(filter);

            return (int)users.DeletedCount;
        }

        public IList<UserHistoryPassword> GetPasswords(string userId, int passwordsToReturn)
        {
            var filter = Builders<UserHistoryPassword>.Filter.Eq(x => x.UserId, userId);
            return _userHistoryPasswordRepository.Collection.Find(filter)
               .SortByDescending(password => password.CreatedOnUtc)
               .Limit(passwordsToReturn)
               .ToList();
        }

        public void InsertUserPassword(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userHistoryPassword = new UserHistoryPassword
            {
                Password = user.Password,
                PasswordFormatId = user.PasswordFormatId,
                PasswordSalt = user.PasswordSalt,
                UserId = user.Id,
                CreatedOnUtc = DateTime.UtcNow
            };

            _userHistoryPasswordRepository.Insert(userHistoryPassword);

            //event notification
            _eventPublisher.EntityInserted(userHistoryPassword);
        }

        public void InsertUserRole(Role userRole)
        {
            if (userRole == null)
                throw new ArgumentNullException(nameof(userRole));

            _roleRepository.Insert(userRole);

            _cacheManager.RemoveByPattern(UserRolesPatternKey);

            //event notification
            _eventPublisher.EntityInserted(userRole);
        }

        public void UpdateUserRole(Role userRole)
        {
            if (userRole == null)
                throw new ArgumentNullException(nameof(userRole));

            _roleRepository.Update(userRole);

            var builder = Builders<User>.Filter;
            var filter = builder.ElemMatch(x => x.Roles, y => y.Id == userRole.Id);
            var update = Builders<User>.Update
               .Set(x => x.Roles.ElementAt(-1), userRole);

            var result = _userRepository.Collection.UpdateManyAsync(filter, update).Result;

            _cacheManager.RemoveByPattern(UserRolesPatternKey);

            //event notification
            _eventPublisher.EntityUpdated(userRole);
        }

        public void DeleteUserRole(Role userRole)
        {
            if (userRole == null)
                throw new ArgumentNullException(nameof(userRole));

            if (userRole.IsSystemRole)
                throw new AtiehJobException("System role could not be deleted");

            _roleRepository.Delete(userRole);

            var builder = Builders<User>.Update;
            var updateFilter = builder.PullFilter(x => x.Roles, y => y.Id == userRole.Id);
            var result = _userRepository.Collection.UpdateManyAsync(
                new BsonDocument(), updateFilter).Result;

            _cacheManager.RemoveByPattern(UserRolesPatternKey);

            //event notification
            _eventPublisher.EntityDeleted(userRole);
        }

        public Role GetUserRoleById(string userRoleId)
        {
            return _roleRepository.GetById(userRoleId);
        }

        public Role GetUserRoleBySystemName(string systemName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
                return null;

            var key = string.Format(UserRolesBySystemNameKey, systemName);
            return _cacheManager.Get(key, () =>
            {
                var filter = Builders<Role>.Filter.Eq(x => x.SystemName, systemName);
                return _roleRepository.Collection.Find(filter).FirstOrDefault();
            });
        }

        public IList<Role> GetAllUserRoles(bool showHidden = false)
        {
            var key = string.Format(UserRolesAllKey, showHidden);
            return _cacheManager.Get(key, () =>
            {
                var query = from cr in _roleRepository.Table
                            where (showHidden || cr.Active)
                            orderby cr.Name
                            select cr;
                var userRoles = query.ToList();
                return userRoles;
            });
        }

        public void InsertUserRoleInUser(Role userRole)
        {
            if (userRole == null)
                throw new ArgumentNullException(nameof(userRole));

            var updateBuilder = Builders<User>.Update;
            var update = updateBuilder.AddToSet(p => p.Roles, userRole);
            _userRepository.Collection.UpdateOneAsync(
                new BsonDocument("_id", userRole.UserId), update);
        }

        public void DeleteUserRoleInUser(Role userRole)
        {
            if (userRole == null)
                throw new ArgumentNullException(nameof(userRole));

            var updateBuilder = Builders<User>.Update;
            var update = updateBuilder.Pull(p => p.Roles, userRole);
            _userRepository.Collection.UpdateOneAsync(
                new BsonDocument("_id", userRole.UserId), update);
        }

        public UserNote GetUserNote(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            return _userNoteRepository.Table.FirstOrDefault(x => x.Id == id);
        }

        public void DeleteUserNote(UserNote userNote)
        {
            if (userNote == null)
                throw new ArgumentNullException(nameof(userNote));

            _userNoteRepository.Delete(userNote);

            //event notification
            _eventPublisher.EntityDeleted(userNote);
        }

        public void InsertUserNote(UserNote userNote)
        {
            if (userNote == null)
                throw new ArgumentNullException(nameof(userNote));

            _userNoteRepository.Insert(userNote);

            //event notification
            _eventPublisher.EntityInserted(userNote);
        }

        public IList<UserNote> GetUserNotes(string userId, bool? displayToUser = null)
        {
            var query = from userNote in _userNoteRepository.Table
                        where userNote.UserId == userId
                        select userNote;

            if (displayToUser.HasValue)
                query = query.Where(x => x.DisplayToUser == displayToUser.Value);

            query = query.OrderByDescending(x => x.CreatedOnUtc);

            return query.ToList();
        }

        public bool IsDuplicateEmail(string email)
        {
            return _userRepository.Any(u => u.Email.ToLower().Trim() == email.ToLower().Trim());
        }

        public bool IsDuplicateMobileNumber(string mobileNumber)
        {
            return _userRepository.Any(u => u.MobileNumber.ToLower().Trim() == mobileNumber.ToLower().Trim());
        }

        public bool IsDuplicateNationalCode(string nationalCode)
        {
            return _userRepository.Any(u => u.NationalCode.ToLower().Trim() == nationalCode.ToLower().Trim());
        }
    }
}
