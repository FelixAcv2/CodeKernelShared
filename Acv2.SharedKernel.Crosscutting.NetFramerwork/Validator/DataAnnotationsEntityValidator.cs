using System;
using System.Collections.Generic;
using System.Text;

namespace Acv2.SharedKernel.Crosscutting.NetFramerwork.Validator
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    using Acv2.SharedKernel.Crosscutting.Validator;
    using System.Linq;

    /// <summary>
    /// Validator based on Data Annotations. 
    /// This validator use IValidatableObject interface and
    /// ValidationAttribute ( hierachy of this) for
    /// perform validation
    /// </summary>
    public class DataAnnotationsEntityValidator
        : IEntityValidator
    {
        #region Private Methods

        /// <summary>
        /// Get erros if object implement IValidatableObject
        /// </summary>
        /// <typeparam name="TEntity">The typeof entity</typeparam>
        /// <param name="item">The item to validate</param>
        /// <param name="errors">A collection of current errors</param>
        void SetValidatableObjectErrors<TEntity>(TEntity item, List<string> errors) where TEntity : class
        {
            if (typeof(IValidatableObject).IsAssignableFrom(typeof(TEntity)))
            {
                var validationContext = new ValidationContext(item, null, null);

                var validationResults = ((IValidatableObject)item).Validate(validationContext);

                errors.AddRange(validationResults.Select(vr => vr.ErrorMessage));
            }
        }

        /// <summary>
        /// Get errors on ValidationAttribute
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="item">The entity to validate</param>
        /// <param name="errors">A collection of current errors</param>
        void SetValidationAttributeErrors<TEntity>(TEntity item, List<string> errors) where TEntity : class
        {
            var result = from property in TypeDescriptor.GetProperties(item).Cast<PropertyDescriptor>()
                         from attribute in property.Attributes.OfType<ValidationAttribute>()
                         where !attribute.IsValid(property.GetValue(item))
                         select attribute.FormatErrorMessage(string.Empty);

            if (result != null
                &&
                result.Any())
            {
                errors.AddRange(result);
            }
        }

        #endregion

        #region IEntityValidator Members


        /// <summary>
        /// <see cref="Acv2.SharedKernel.Crosscutting.Validator.IEntityValidator"/>
        /// </summary>
        /// <typeparam name="TEntity"><see cref="Acv2.SharedKernel.Crosscutting.Validator.IEntityValidator"/></typeparam>
        /// <param name="item"><see cref="Acv2.SharedKernel.Crosscutting.Validator.IEntityValidator"/></param>
        /// <returns><see cref="Acv2.SharedKernel.Crosscutting.Validator.IEntityValidator"/></returns>
        public bool IsValid<TEntity>(TEntity item) where TEntity : class
        {

            if (item == null)
                return false;

            List<string> validationErrors = new List<string>();

            SetValidatableObjectErrors(item, validationErrors);
            SetValidationAttributeErrors(item, validationErrors);

            return !validationErrors.Any();
        }
        /// <summary>
        /// <see cref="Acv2.SharedKernel.Crosscutting.Validator.IEntityValidator"/>
        /// </summary>
        /// <typeparam name="TEntity"><see cref="Acv2.SharedKernel.Crosscutting.Validator.IEntityValidator"/></typeparam>
        /// <param name="item"><see cref="Acv2.SharedKernel.Crosscutting.Validator.IEntityValidator"/></param>
        /// <returns><see cref="Acv2.SharedKernel.Crosscutting.Validator.IEntityValidator"/></returns>
        public IEnumerable<string> GetInvalidMessages<TEntity>(TEntity item) where TEntity : class
        {
            if (item == null)
                return null;

            List<string> validationErrors = new List<string>();

            SetValidatableObjectErrors(item, validationErrors);
            SetValidationAttributeErrors(item, validationErrors);


            return validationErrors;
        }

        #endregion
    }
}
