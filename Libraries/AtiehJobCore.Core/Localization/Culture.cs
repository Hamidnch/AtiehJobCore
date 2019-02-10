using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AtiehJobCore.Core.Localization.Models;

namespace AtiehJobCore.Core.Localization
{
    public class Culture : EntityBaseWithTypedId<string>
    {
        public Culture(string id)
        {
            Id = id;
        }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(450)]
        public string Name { get; set; }

        public IList<Resource> Resources { get; set; }
    }
}
