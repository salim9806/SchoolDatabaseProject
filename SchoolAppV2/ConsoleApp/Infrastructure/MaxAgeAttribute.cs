using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,AllowMultiple = false)]
    internal class MaxAgeAttribute:ValidationAttribute
    {
        const string DefaultErrorMessage = "\"{0}\" field be over {1} years old";
        byte minimumAge;
        int actualAge = 2;
        public MaxAgeAttribute(byte minimumAge):base(DefaultErrorMessage)
        {
            this.minimumAge = minimumAge;
        }

        public override bool IsValid(object? value)
        {
            if(value != null && value is DateTime dateTimeValue)
            {
                if (dateTimeValue < DateTime.UtcNow.AddYears(-minimumAge))
                {
                    actualAge = DateTime.UtcNow.Year - dateTimeValue.Year;
                    return true;
                }
                else
                    actualAge = DateTime.UtcNow.Year - dateTimeValue.Year;
                    return false;
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(ErrorMessageString, name, minimumAge, this.actualAge);
        }
    }
}
