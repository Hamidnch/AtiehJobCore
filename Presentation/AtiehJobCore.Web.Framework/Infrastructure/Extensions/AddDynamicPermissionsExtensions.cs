﻿using AtiehJobCore.Core.Constants;
using AtiehJobCore.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace AtiehJobCore.Web.Framework.Infrastructure.Extensions
{
    public static class AddDynamicPermissionsExtensions
    {
        public static IServiceCollection AddDynamicPermissions(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, DynamicPermissionsAuthorizationHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    name: PolicyNames.DynamicPermission,
                    configurePolicy: policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.Requirements.Add(new DynamicPermissionRequirement());
                    });
            });

            return services;
        }
    }
}
