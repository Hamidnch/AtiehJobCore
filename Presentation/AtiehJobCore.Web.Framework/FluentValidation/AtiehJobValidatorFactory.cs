using AtiehJobCore.Core.Infrastructure;
using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Reflection;

namespace AtiehJobCore.Web.Framework.FluentValidation
{
    /// <summary>
    /// Represents custom validator factory that looks for the attribute instance on the specified type in order to provide the validator instance.
    /// </summary>
    public class AtiehJobValidatorFactory : AttributedValidatorFactory
    {
        /// <summary>
        /// Gets a validator for the appropriate type
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>Created IValidator instance; null if a validator cannot be created</returns>
        public override IValidator GetValidator(Type type)
        {
            if (type == null)
                return null;

            var validatorAttribute = (ValidatorAttribute)type.GetTypeInfo().GetCustomAttribute(typeof(ValidatorAttribute));
            if (validatorAttribute == null || validatorAttribute.ValidatorType == null)
                return null;

            //try to create instance of the validator
            var instance = EngineContext.Current.Resolve(validatorAttribute.ValidatorType);

            return instance as IValidator;
        }
    }
}
