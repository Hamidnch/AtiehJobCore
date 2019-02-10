using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System.Collections.Generic;
using System.Linq;

namespace AtiehJobCore.Web.Framework.KendoUi
{
    public static class ModelStateExtensions
    {
        public static object SerializeErrors(this ModelStateDictionary modelStateDictionary)
        {
            return modelStateDictionary.Where(entry => entry.Value.Errors.Any())
                .ToDictionary(entry => entry.Key, entry => SerializeModelState(entry.Value));
        }

        private static Dictionary<string, object> SerializeModelState(ModelStateEntry modelState)
        {
            var errors = new List<string>();
            foreach (var modelError in modelState.Errors)
            {
                var errorText = ValidationHelpers.GetModelErrorMessageOrDefault(modelError);

                if (!string.IsNullOrEmpty(errorText))
                {
                    errors.Add(errorText);
                }
            }

            var dictionary = new Dictionary<string, object>
            {
                ["errors"] = errors.ToArray()
            };
            return dictionary;
        }

        public static object ToDataSourceResult(this ModelStateDictionary modelState)
        {
            return !modelState.IsValid ? modelState.SerializeErrors() : null;
        }
    }
}
