using System;
using System.Collections.Generic;
using System.Text;
using AtiehJobCore.Common.Http;
using AtiehJobCore.Common.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace AtiehJobCore.Services.Authentication.External
{
    /// <summary>
    /// External authorizer helper
    /// </summary>
    public static partial class ExternalAuthorizerHelper
    {
        #region Constants

        /// <summary>
        /// Key for store external authentication parameters to session
        /// </summary>
        private const string ExternalAuthenticationParameters = "atiehJob.externalauth.parameters";

        /// <summary>
        /// Key for store external authentication errors to session
        /// </summary>
        private const string ExternalAuthenticationErrors = "atiehJob.externalauth.errors";

        #endregion

        #region Methods

        public static void StoreParametersForRoundTrip(ExternalAuthenticationParameters parameters)
        {
            EngineContext.Current.Resolve<IHttpContextAccessor>().HttpContext?.Session?.Set(ExternalAuthenticationParameters, parameters);
        }

        public static void AddErrorsToDisplay(string error)
        {
            var session = EngineContext.Current.Resolve<IHttpContextAccessor>().HttpContext?.Session;
            var errors = session?.Get<IList<string>>(ExternalAuthenticationErrors) ?? new List<string>();
            errors.Add(error);
            session?.Set(ExternalAuthenticationErrors, errors);
        }

        public static IList<string> RetrieveErrorsToDisplay(bool removeOnRetrieval)
        {
            var session = EngineContext.Current.Resolve<IHttpContextAccessor>().HttpContext?.Session;
            var errors = session?.Get<IList<string>>(ExternalAuthenticationErrors);

            if (errors != null && removeOnRetrieval)
                session.Remove(ExternalAuthenticationErrors);

            return errors;
        }

        #endregion
    }
}
