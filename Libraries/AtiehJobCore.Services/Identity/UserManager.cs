using AtiehJobCore.Common.Extensions;
using AtiehJobCore.Data.DbContext;
using AtiehJobCore.Domain.Entities.Identity;
using AtiehJobCore.Services.Identity.Interfaces;
using AtiehJobCore.ViewModel.Models.Identity.Account;
using AtiehJobCore.ViewModel.Models.Identity.Common;
using DNTCommon.Web.Core;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AtiehJobCore.Services.Identity
{
    public class UserManager : UserManager<User>, IUserManager
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUsedPasswordsService _usedPasswordsService;
        private readonly DbSet<User> _users;
        private readonly DbSet<Role> _roles;
        private User _currentUserInScope;

        public UserManager(IUserStore store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager> logger,
            IHttpContextAccessor contextAccessor, IUnitOfWork uow, IUsedPasswordsService usedPasswordsService)
            : base((UserStore<User, Role, AtiehJobCoreDbContext, int, UserClaim, UserRole,
                    UserLogin, UserToken, RoleClaim>)store, optionsAccessor, passwordHasher,
                userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            var userStore = store;
            userStore.CheckArgumentIsNull(nameof(userStore));

            _contextAccessor = contextAccessor;
            _contextAccessor.CheckArgumentIsNull(nameof(_contextAccessor));

            _usedPasswordsService = usedPasswordsService;
            _usedPasswordsService.CheckArgumentIsNull(nameof(_usedPasswordsService));

            _users = uow.Set<User>();
            _roles = uow.Set<Role>();

            //var optionsAccessor1 = optionsAccessor;
            //optionsAccessor1.CheckArgumentIsNull(nameof(optionsAccessor1));

            //var passwordHasher1 = passwordHasher;
            //passwordHasher1.CheckArgumentIsNull(nameof(passwordHasher1));

            //var userValidators1 = userValidators;
            //userValidators1.CheckArgumentIsNull(nameof(userValidators1));

            //var passwordValidators1 = passwordValidators;
            //passwordValidators1.CheckArgumentIsNull(nameof(passwordValidators1));

            //var keyNormalizer1 = keyNormalizer;
            //keyNormalizer1.CheckArgumentIsNull(nameof(keyNormalizer1));

            //var errors1 = errors;
            //errors1.CheckArgumentIsNull(nameof(errors1));

            //var services1 = services;
            //services1.CheckArgumentIsNull(nameof(services1));

            //var logger1 = logger;
            //logger1.CheckArgumentIsNull(nameof(logger1));

            //var uow1 = uow;
            //uow1.CheckArgumentIsNull(nameof(uow1));

        }

        #region BaseClass

        string IUserManager.CreateTwoFactorRecoveryCode()
        {
            return base.CreateTwoFactorRecoveryCode();
        }

        Task<PasswordVerificationResult> IUserManager.VerifyPasswordAsync(
            IUserPasswordStore<User> store, User user, string password)
        {
            return base.VerifyPasswordAsync(store, user, password);
        }

        public override async Task<IdentityResult> CreateAsync(User user)
        {
            var result = await base.CreateAsync(user);
            if (result.Succeeded)
                await _usedPasswordsService.AddToUsedPasswordsListAsync(user);

            return result;
        }

        public override async Task<IdentityResult> CreateAsync(User user, string password)
        {
            var result = await base.CreateAsync(user, password);
            if (result.Succeeded)
                await _usedPasswordsService.AddToUsedPasswordsListAsync(user);

            return result;
        }

        public override async Task<IdentityResult> ChangePasswordAsync(
            User user, string currentPassword, string newPassword)
        {
            var result = await base.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result.Succeeded)
            {
                await _usedPasswordsService.AddToUsedPasswordsListAsync(user);
            }
            return result;
        }

        public override async Task<IdentityResult> ResetPasswordAsync(
            User user, string token, string newPassword)
        {
            var result = await base.ResetPasswordAsync(user, token, newPassword);
            if (result.Succeeded)
            {
                await _usedPasswordsService.AddToUsedPasswordsListAsync(user);
            }
            return result;
        }

        #endregion

        #region Methods

        public User FindById(int userId)
        {
            return _users.Find(userId);
        }

        public Task<User> FindByIdIncludeUserRolesAsync(int userId)
        {
            return _users.Include(x => x.UserRoles).FirstOrDefaultAsync(x => x.Id == userId);
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            return Users.ToListAsync();
        }

        public User GetCurrentUser()
        {
            if (_currentUserInScope != null)
            {
                return _currentUserInScope;
            }

            var currentUserId = GetCurrentUserId();
            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                return null;
            }

            var userId = int.Parse(currentUserId);
            return _currentUserInScope = FindById(userId);
        }

        public async Task<User> GetCurrentUserAsync()
        {
            return _currentUserInScope ??
                (_currentUserInScope = await GetUserAsync(_contextAccessor.HttpContext.User));
        }

        public string GetCurrentUserId()
        {
            return _contextAccessor.HttpContext.User.Identity.GetUserId();
        }

        public int? CurrentUserId
        {
            get
            {
                var userId = _contextAccessor.HttpContext.User.Identity.GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    return null;
                }

                return !int.TryParse(userId, out var result) ? (int?)null : result;
            }
        }

        IPasswordHasher<User> IUserManager.PasswordHasher
        { get => base.PasswordHasher; set => base.PasswordHasher = value; }

        IList<IUserValidator<User>> IUserManager.UserValidators => base.UserValidators;

        IList<IPasswordValidator<User>> IUserManager.PasswordValidators =>
            base.PasswordValidators;

        IQueryable<User> IUserManager.Users => base.Users;

        public string GetCurrentUserName()
        {
            return _contextAccessor.HttpContext.User.Identity.GetUserName();
        }

        public async Task<bool> HasPasswordAsync(int userId)
        {
            var user = await FindByIdAsync(userId.ToString());
            return user?.PasswordHash != null;
        }

        public async Task<bool> HasPhoneNumberAsync(int userId)
        {
            var user = await FindByIdAsync(userId.ToString());
            return user?.PhoneNumber != null;
        }

        public async Task<byte[]> GetEmailImageAsync(int? userId)
        {
            if (userId == null)
                return "?".TextToImage(new TextToImageOptions());

            var user = await FindByIdAsync(userId.Value.ToString());
            if (user == null)
                return "?".TextToImage(new TextToImageOptions());

            return !user.IsEmailPublic ? "?".TextToImage(new TextToImageOptions())
                : user.Email.TextToImage(new TextToImageOptions());
        }

        public async Task<PagedUsersListViewModel> GetPagedUsersListAsync(
            SearchUsersViewModel model, int pageNumber)
        {
            var skipRecords = pageNumber * model.MaxNumberOfRows;
            var query = _users.Include(x => x.UserRoles).AsNoTracking();

            if (!model.ShowAllUsers)
            {
                query = query.Where(x => x.IsActive == model.UserIsActive);
            }

            if (!string.IsNullOrWhiteSpace(model.TextToFind))
            {
                model.TextToFind = model.TextToFind.ApplyCorrectYeKe();

                if (model.IsPartOfEmail)
                {
                    query = query.Where(x => x.Email.Contains(model.TextToFind));
                }

                if (model.IsUserId)
                {
                    if (int.TryParse(model.TextToFind, out var userId))
                    {
                        query = query.Where(x => x.Id == userId);
                    }
                }

                if (model.IsPartOfName)
                {
                    query = query.Where(x => x.FirstName.Contains(model.TextToFind));
                }

                if (model.IsPartOfLastName)
                {
                    query = query.Where(x => x.LastName.Contains(model.TextToFind));
                }

                if (model.IsPartOfUserName)
                {
                    query = query.Where(x => x.UserName.Contains(model.TextToFind));
                }

                if (model.IsPartOfLocation)
                {
                    query = query.Where(x => x.PlaceOfBirth.Contains(model.TextToFind));
                }
            }

            if (model.HasEmailConfirmed)
            {
                query = query.Where(x => x.EmailConfirmed);
            }

            if (model.UserIsLockedOut)
            {
                query = query.Where(x => x.LockoutEnd != null);
            }

            if (model.HasTwoFactorEnabled)
            {
                query = query.Where(x => x.TwoFactorEnabled);
            }

            query = query.OrderBy(x => x.Id);
            return new PagedUsersListViewModel
            {
                Paging =
                {
                    TotalItems = await query.CountAsync()
                },
                Users = await query.Skip(skipRecords).Take(model.MaxNumberOfRows).ToListAsync(),
                Roles = await _roles.ToListAsync()
            };
        }

        public async Task<PagedUsersListViewModel> GetPagedUsersListAsync(
            int pageNumber, int recordsPerPage,
            string sortByField, SortOrder sortOrder,
            bool showAllUsers)
        {
            var skipRecords = pageNumber * recordsPerPage;
            var query = _users.Include(x => x.UserRoles).AsNoTracking();

            if (!showAllUsers)
            {
                query = query.Where(x => x.IsActive);
            }

            switch (sortByField)
            {
                case nameof(User.Id):
                    query = sortOrder == SortOrder.Descending ?
                        query.OrderByDescending(x => x.Id) : query.OrderBy(x => x.Id);
                    break;
                default:
                    query = sortOrder == SortOrder.Descending ?
                        query.OrderByDescending(x => x.Id) : query.OrderBy(x => x.Id);
                    break;
            }

            return new PagedUsersListViewModel
            {
                Paging =
                {
                    TotalItems = await query.CountAsync()
                },
                Users = await query.Skip(skipRecords).Take(recordsPerPage).ToListAsync(),
                Roles = await _roles.ToListAsync()
            };
        }

        public async Task<IdentityResult> UpdateUserAndSecurityStampAsync(
            int userId, Action<User> action)
        {
            var user = await FindByIdIncludeUserRolesAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "کاربر مورد نظر یافت نشد."
                });
            }

            action(user);

            var result = await UpdateAsync(user);
            if (!result.Succeeded)
            {
                return result;
            }
            return await UpdateSecurityStampAsync(user);
        }

        public async Task<IdentityResult> AddOrUpdateUserRolesAsync(
            int userId, IList<int> selectedRoleIds, Action<User> action = null)
        {
            var user = await FindByIdIncludeUserRolesAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "کاربر مورد نظر یافت نشد."
                });
            }

            var currentUserRoleIds = user.UserRoles.Select(x => x.RoleId).ToList();

            if (selectedRoleIds == null)
            {
                selectedRoleIds = new List<int>();
            }
            var newRolesToAdd = selectedRoleIds.Except(currentUserRoleIds).ToList();
            foreach (var roleId in newRolesToAdd)
            {
                user.UserRoles.Add(new UserRole { RoleId = roleId, UserId = user.Id });
            }

            var removedRoles = currentUserRoleIds.Except(selectedRoleIds).ToList();
            foreach (var roleId in removedRoles)
            {
                var userRole = user.UserRoles.SingleOrDefault(ur => ur.RoleId == roleId);
                if (userRole != null)
                {
                    user.UserRoles.Remove(userRole);
                }
            }

            action?.Invoke(user);

            var result = await UpdateAsync(user);
            if (!result.Succeeded)
            {
                return result;
            }
            return await UpdateSecurityStampAsync(user);
        }

        #endregion
    }
}
