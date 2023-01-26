using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Infrastructure
{
    public static class ModelValidator
    {
        public static bool IsValid(object model)
        {
            ArgumentNullException.ThrowIfNull(model);

            return Validator.TryValidateObject(model, new ValidationContext(model), null,validateAllProperties:true);
        }

        public static IEnumerable<ValidationResult> Validate(object model)
        {
            ArgumentNullException.ThrowIfNull(model);

            ICollection<ValidationResult> validationResults = new Collection<ValidationResult>();

            Validator.TryValidateObject(model, new ValidationContext(model), validationResults, validateAllProperties: true);
            return validationResults;
        }
    }
}
