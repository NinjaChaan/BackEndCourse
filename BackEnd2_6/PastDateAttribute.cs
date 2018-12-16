using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd2_6
{
    public class PastDateAttribute : ValidationAttribute
	{

		public PastDateAttribute() {
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext) {

			if (DateTime.Now < (DateTime)value) {
				return new ValidationResult($"Date must be in the past.");
			}

			return ValidationResult.Success;
		}
	}
}
