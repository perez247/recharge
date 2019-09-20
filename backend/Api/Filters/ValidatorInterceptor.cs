using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Api.Filters
{
    public class ValidatorInterceptor : IValidatorInterceptor
    {
        public ValidationResult AfterMvcValidation(ControllerContext controllerContext, ValidationContext validationContext, ValidationResult result)
        {
            
            if(result.Errors.Count > 0)
                throw new Application.Exceptions.CustomValidationException(result.Errors);
            
            return result;
        }

        public ValidationContext BeforeMvcValidation(ControllerContext controllerContext, ValidationContext validationContext)
        {
            return validationContext;
        }
    }
}