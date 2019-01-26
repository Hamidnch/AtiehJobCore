using System.Collections.Generic;
using AtiehJobCore.Domain.Entities.Identity;
using DNTCommon.Web.Core;

namespace AtiehJobCore.ViewModel.Models.Identity.Common
{
    public class DynamicRoleClaimsManagerViewModel
    {
        public string[] ActionIds { set; get; }

        public int RoleId { set; get; }

        public Role RoleIncludeRoleClaims { set; get; }

        public ICollection<MvcControllerViewModel> SecuredControllerActions { set; get; }
    }
}