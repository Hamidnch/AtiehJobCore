using System;
using System.Collections.Generic;
using AtiehJobCore.Common.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

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
