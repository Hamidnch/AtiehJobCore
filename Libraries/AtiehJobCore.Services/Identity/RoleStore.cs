using System.Security.Claims;
using AtiehJobCore.Data.DbContext;
using AtiehJobCore.Domain.Entities.Identity;
using AtiehJobCore.Services.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AtiehJobCore.Services.Identity
{
    public class RoleStore : RoleStore<Role, AtiehJobCoreDbContext, int, UserRole,
            RoleClaim>, IRoleStore
    {
        public RoleStore(
            IUnitOfWork uow,
            IdentityErrorDescriber describer)
            : base((AtiehJobCoreDbContext)uow, describer)
        {
            //var uow1 = uow;
            //uow1.CheckArgumentIsNull(nameof(uow1));

            //var describer1 = describer;
            //describer1.CheckArgumentIsNull(nameof(describer1));
        }


        #region BaseClass

        protected override RoleClaim CreateRoleClaim(Role role, Claim claim)
        {
            return new RoleClaim
            {
                RoleId = role.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            };
        }

        #endregion

        #region Methods

        #endregion
    }
}