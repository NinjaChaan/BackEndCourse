using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BackEnd2_6.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd2_6
{
	public class AllowedItemTypesAttribute : ValidationAttribute
	{
		private ItemType[] _validItemTypes;

		public AllowedItemTypesAttribute(params ItemType[] validItemTypes) {
			_validItemTypes = validItemTypes;
		}

		protected override ValidationResult IsValid(object value, ValidationContext context) {
			ItemType type = (ItemType)value;

			if (!_validItemTypes.Contains(type)) {
				return new ValidationResult(GetErrorMessage());
			}

			return ValidationResult.Success;
		}

		private string GetErrorMessage() {
			return $"Item type should be one of: %s";
		}
	}
}