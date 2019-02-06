using System;
using System.Collections.Generic;
using System.Text;

namespace AtiehJobCore.Web.Framework.Models.Admin
{
    public partial class PermissionMappingModel : BaseMongoModel
    {
        public PermissionMappingModel()
        {
            AvailablePermissions = new List<PermissionRecordModel>();
            AvailableUserRoles = new List<UserRoleModel>();
            Allowed = new Dictionary<string, IDictionary<string, bool>>();
        }
        public IList<PermissionRecordModel> AvailablePermissions { get; set; }
        public IList<UserRoleModel> AvailableUserRoles { get; set; }

        //[permission system name] / [user role id] / [allowed]
        public IDictionary<string, IDictionary<string, bool>> Allowed { get; set; }
    }
}
