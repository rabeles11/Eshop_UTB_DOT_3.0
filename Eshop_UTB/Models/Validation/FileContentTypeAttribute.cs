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
    public class FileContentTypeAttribute : ValidationAttribute , IClientModelValidator
    {
        private readonly string contentType;
        public FileContentTypeAttribute(string contentType){

            this.contentType = contentType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return ValidationResult.Success;
            }
            else if (value is IFormFile iff)
            {
                if (iff.ContentType.ToLower().Contains(contentType.ToLower()))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(GetErrorMessage("File"), new List<string> { validationContext.MemberName });
                }
            }

            throw new NotImplementedException($"The attribute {nameof(FileContentTypeAttribute)} is not implemented for object {value.GetType()}. {nameof(IFormFile)} only is implemented");
        }
        protected string GetErrorMessage(string memeberName)
        {
            return $"{memeberName} must be type of {contentType}!";
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            
            ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val", "true");
            ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val-filecontent", GetErrorMessage("File"));
            ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val-filecontent-type", contentType);
        }

        



    }
}
