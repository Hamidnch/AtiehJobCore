﻿using System.Security.Claims;

namespace AtiehJobCore.Services.Security
{
    public interface ISecurityTrimmingService
    {
        bool CanCurrentUserAccess(string area, string controller, string action);
        bool CanUserAccess(ClaimsPrincipal user, string area, string controller, string action);
    }
}