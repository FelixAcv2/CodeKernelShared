using System;
using System.Collections.Generic;
using System.Text;
using Acv2.SharedKernel.Crosscutting.Validator;

namespace Acv2.SharedKernel.Crosscutting.NetFramerwork.Validator
{

    /// <summary>
    /// Data Annotations based entity validator factory
    /// </summary>
    public class DataAnnotationsEntityValidatorFactory
        : IEntityValidatorFactory
    {
        /// <summary>
        /// Create a entity validator
        /// </summary>
        /// <returns></returns>
        public IEntityValidator Create()
        {
            return new DataAnnotationsEntityValidator();
        }
    }

}
