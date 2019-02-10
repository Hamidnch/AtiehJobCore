using AtiehJobCore.Core.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;

namespace AtiehJobCore.Web.Framework.Filters
{
    public class SiteSettingsValidationStartUpFilter : IStartupFilter
    {
        private readonly IEnumerable<IValidatable> _validatableObjects;

        public SiteSettingsValidationStartUpFilter(IEnumerable<IValidatable> validatableObjects)
        {
            _validatableObjects = validatableObjects;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            foreach (var validatableObject in _validatableObjects)
            {
                validatableObject.Validate();
            }

            //don't alter the configuration
            return next;
        }
    }
}
