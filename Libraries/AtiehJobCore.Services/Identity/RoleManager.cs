using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AtiehJobCore.Common.Extensions;
using AtiehJobCore.Data.DbContext;
using AtiehJobCore.Domain.Entities.Identity;
using AtiehJobCore.Services.Identity.Interfaces;
using AtiehJobCore.ViewModel.Models.Identity.Account;
using AtiehJobCore.ViewModel.Models.Identity.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AtiehJobCore.Services.Identity
{
    public class RoleManager : RoleManager<Role>, IRoleManager
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly DbSet<User> _users;

        public RoleManager(
            IRoleStore store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager> logger,
            IHttpContextAccessor contextAccessor,
            IUnitOfWork uow) : base((RoleStore<Role, AtiehJobCoreDbContext,
                int, UserRole, RoleClaim>)store,
                roleValidators, keyNormalizer, errors, logger)
        {
            //var store1 = store;
            //store1.CheckArgumentIsNull(nameof(store1));

            //var roleValidators1 = roleValidators;
            //roleValidators1.CheckArgumentIsNull(nameof(roleValidators1));

            //var keyNormalizer1 = keyNormalizer;
            //keyNormalizer1.CheckArgumentIsNull(nameof(keyNormalizer1));

            //var errors1 = errors;
            //errors1.CheckArgumentIsNull(nameof(errors1));

            //var logger1 = logger;
            //logger1.CheckArgumentIsNull(nameof(logger1));

            _contextAccessor = contextAccessor;
            _contextAccessor.CheckArgumentIsNull(nameof(_contextAccessor));

            //var uow1 = uow;
            uow.CheckArgumentIsNull(nameof(uow));

            _users = uow.Set<User>();
        }

        #region BaseClass

        #endregion

        #region Methods

        public IList<Role> FindCurrentUserRoles()
        {
            var userId = GetCurrentUserId();
            return FindUserRoles(userId);
        }

        public IList<Role> FindUserRoles(int userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.UserRoles
                                 where user.UserId == userId
                                 select role;

            return userRolesQuery.OrderBy(x => x.Name).ToList();
        }

        public Task<List<Role>> GetAllRolesAsync()
        {
            return Roles.ToListAsync();
        }

        public IList<RoleAndUsersCountViewModel> GetAllRolesAndUsersCountList()
        {
            return Roles.Select(role =>
                                    new RoleAndUsersCountViewModel
                                    {
                                        Role = role,
                                        UsersCount = role.UserRoles.Count()
                                    }).ToList();
        }


        public async Task<PagedUsersListViewModel> GetPagedUsersInRoleListAsync(
                int roleId, int pageNumber, int recordsPerPage,
                string sortByField, SortOrder sortOrder, bool showAllUsers)
        {
            var skipRecords = pageNumber * recordsPerPage;

            var roleUserIdsQuery = from role in Roles
                                   where role.Id == roleId
                                   from user in role.UserRoles
                                   select user.UserId;
            var query = _users.Include(user => user.UserRoles)
                              .Where(user => roleUserIdsQuery.Contains(user.Id))
                         .AsNoTracking();

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
                Roles = await Roles.ToListAsync()
            };
        }


        public IList<User> GetUsersInRole(string roleName)
        {
            var roleUserIdsQuery = from role in Roles
                                   where role.Name == roleName
                                   from user in role.UserRoles
                                   select user.UserId;
            return _users.Where(user => roleUserIdsQuery.Contains(user.Id))
                         .ToList();
        }

        public IList<Role> GetRolesForCurrentUser()
        {
            var userId = GetCurrentUserId();
            return GetRolesForUser(userId);
        }

        public IList<Role> GetRolesForUser(int userId)
        {
            var roles = FindUserRoles(userId);
            if (roles == null || !roles.Any())
            {
                return new List<Role>();
            }

            return roles.ToList();
        }

        public IList<UserRole> GetUserRolesInRole(string roleName)
        {
            return Roles.Where(role => role.Name == roleName)
                             .SelectMany(role => role.UserRoles)
                             .ToList();
        }

        public bool IsCurrentUserInRole(string roleName)
        {
            var userId = GetCurrentUserId();
            return IsUserInRole(userId, roleName);
        }

        public bool IsUserInRole(int userId, string roleName)
        {
            var userRolesQuery = from role in Roles
                                 where role.Name == roleName
                                 from user in role.UserRoles
                                 where user.UserId == userId
                                 select role;
            var userRole = userRolesQuery.FirstOrDefault();
            return userRole != null;
        }

        public Task<Role> FindRoleIncludeRoleClaimsAsync(int roleId)
        {
            return Roles.Include(x => x.RoleClaims).FirstOrDefaultAsync(x => x.Id == roleId);
        }

        public async Task<IdentityResult> AddOrUpdateRoleClaimsAsync(
            int roleId, string roleClaimType, IList<string> selectedRoleClaimValues)
        {
            var role = await FindRoleIncludeRoleClaimsAsync(roleId);
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "RoleNotFound",
                    Description = "نقش مورد نظر یافت نشد."
                });
            }

            var currentRoleClaimValues = role.RoleClaims.Where(
                    rc => rc.ClaimType == roleClaimType)
                                                    .Select(rc => rc.ClaimValue)
                                                    .ToList();

            if (selectedRoleClaimValues == null)
            {
                selectedRoleClaimValues = new List<string>();
            }
            var newClaimValuesToAdd = selectedRoleClaimValues.Except(currentRoleClaimValues).ToList();
            foreach (var claimValue in newClaimValuesToAdd)
            {
                role.RoleClaims.Add(new RoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = roleClaimType,
                    ClaimValue = claimValue
                });
            }

            var removedClaimValues = currentRoleClaimValues.Except(selectedRoleClaimValues).ToList();
            foreach (var claimValue in removedClaimValues)
            {
                var roleClaim = role.RoleClaims.SingleOrDefault(
                    rc => rc.ClaimValue == claimValue && rc.ClaimType == roleClaimType);
                if (roleClaim != null)
                {
                    role.RoleClaims.Remove(roleClaim);
                }
            }

            return await UpdateAsync(role);
        }

        private int GetCurrentUserId() => _contextAccessor.HttpContext.User.Identity.GetUserId<int>();

        #endregion
    }
}
