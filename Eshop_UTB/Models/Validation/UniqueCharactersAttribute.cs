using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop_UTB.Models.Validation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter,AllowMultiple = false)]
    public class UniqueCharactersAttribute : ValidationAttribute , IClientModelValidator
    {
        private readonly int minumumChar;
        public UniqueCharactersAttribute(int contentType){

            this.minumumChar = contentType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int cnt = value.ToString().ToCharArray().Distinct().Count();

            if(cnt >= minumumChar)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(GetErrorMessage("Passsword"));
            }

        }
        protected string GetErrorMessage(string memeberName)
        {
            return $"{memeberName} must be atleast {minumumChar} unique characters";
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            
            ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val", "true");
            ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val-uniquecharacters", GetErrorMessage("Password"));
            ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val-uniquecharacters-type", minumumChar.ToString());
        }

        



    }
}
