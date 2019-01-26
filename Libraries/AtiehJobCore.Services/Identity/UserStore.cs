using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AtiehJobCore.Data.DbContext;
using AtiehJobCore.Domain.Entities.Identity;
using AtiehJobCore.Services.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AtiehJobCore.Services.Identity
{
    public class UserStore : UserStore<User, Role, AtiehJobCoreDbContext, int, UserClaim, UserRole,
            UserLogin, UserToken, RoleClaim>, IUserStore
    {
        public UserStore(IUnitOfWork uow, IdentityErrorDescriber describer)
            : base((AtiehJobCoreDbContext)uow, describer)
        {
            //var uow1 = uow;
            //uow1.CheckArgumentIsNull(nameof(uow1));

            //var describer1 = describer;
            //describer1.CheckArgumentIsNull(nameof(describer1));
        }


        #region BaseClass

        protected override UserClaim CreateUserClaim(User user, Claim claim)
        {
            var userClaim = new UserClaim { UserId = user.Id };
            userClaim.InitializeFromClaim(claim);
            return userClaim;
        }

        protected override UserLogin CreateUserLogin(User user, UserLoginInfo login)
        {
            return new UserLogin
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName
            };
        }

        protected override UserRole CreateUserRole(User user, Role role)
        {
            return new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            };
        }

        protected override UserToken CreateUserToken(
            User user, string loginProvider, string name, string value)
        {
            return new UserToken
            {
                UserId = user.Id,
                LoginProvider = loginProvider,
                Name = name,
                Value = value
            };
        }

        Task IUserStore.AddUserTokenAsync(UserToken token)
        {
            return base.AddUserTokenAsync(token);
        }

        Task<Role> IUserStore.FindRoleAsync(
            string normalizedRoleName, CancellationToken cancellationToken)
        {
            return base.FindRoleAsync(normalizedRoleName, cancellationToken);
        }

        Task<UserToken> IUserStore.FindTokenAsync(
            User user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            return base.FindTokenAsync(user, loginProvider, name, cancellationToken);
        }

        Task<User> IUserStore.FindUserAsync(int userId, CancellationToken cancellationToken)
        {
            return base.FindUserAsync(userId, cancellationToken);
        }

        Task<UserLogin> IUserStore.FindUserLoginAsync(
            int userId, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return base.FindUserLoginAsync(userId, loginProvider, providerKey, cancellationToken);
        }

        Task<UserLogin> IUserStore.FindUserLoginAsync(
            string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return base.FindUserLoginAsync(loginProvider, providerKey, cancellationToken);
        }

        Task<UserRole> IUserStore.FindUserRoleAsync(
            int userId, int roleId, CancellationToken cancellationToken)
        {
            return base.FindUserRoleAsync(userId, roleId, cancellationToken);
        }

        Task IUserStore.RemoveUserTokenAsync(UserToken token)
        {
            return base.RemoveUserTokenAsync(token);
        }

        #endregion

        #region Methods

        // Add  methods here

        #endregion
    }
}
