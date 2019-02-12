using System;
using System.ComponentModel.DataAnnotations;
using AtiehJobCore.Web.Framework.Mvc.ModelBinding;

namespace AtiehJobCore.Web.Framework.Models.Admin
{
    public partial class MaintenanceModel : BaseMongoModel
    {
        public MaintenanceModel()
        {
            DeleteGuests = new DeleteGuestsModel();
            DeleteExportedFiles = new DeleteExportedFilesModel();
        }

        public DeleteGuestsModel DeleteGuests { get; set; }
        public DeleteExportedFilesModel DeleteExportedFiles { get; set; }
        public bool DeleteActivityLog { get; set; }

        #region Nested classes

        public partial class DeleteGuestsModel : BaseMongoModel
        {
            [AtiehJobResourceDisplayName("Admin.System.Maintenance.DeleteGuests.StartDate")]
            [UIHint("DateNullable")]
            public DateTime? StartDate { get; set; }

            [AtiehJobResourceDisplayName("Admin.System.Maintenance.DeleteGuests.EndDate")]
            [UIHint("DateNullable")]
            public DateTime? EndDate { get; set; }

            public int? NumberOfDeletedUsers { get; set; }
        }

        public partial class DeleteExportedFilesModel : BaseMongoModel
        {
            [AtiehJobResourceDisplayName("Admin.System.Maintenance.DeleteExportedFiles.StartDate")]
            [UIHint("DateNullable")]
            public DateTime? StartDate { get; set; }

            [AtiehJobResourceDisplayName("Admin.System.Maintenance.DeleteExportedFiles.EndDate")]
            [UIHint("DateNullable")]
            public DateTime? EndDate { get; set; }

            public int? NumberOfDeletedFiles { get; set; }
        }

        #endregion
    }
}
