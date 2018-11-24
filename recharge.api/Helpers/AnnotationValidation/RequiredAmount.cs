using System;
using System.ComponentModel.DataAnnotations;

namespace recharge.api.Helpers.AnnotationValidation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class RequiredAmount : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // if(value == null)
            // {
            //     try
            //     {
            //         validationContext
            //         .ObjectType
            //         .GetProperty(validationContext.MemberName)
            //         .SetValue(validationContext.ObjectInstance, 0, null);
            //         return null;
            //     }
            //     catch (System.Exception)
            //     {
            //         return new ValidationResult("");                       
            //     }
            // }
            // if()
            // Decimal parsedDecimal = (Decimal) value;

            // if (parsedDecimal == 0)
            //     return null;

            // if(parsedDecimal>=100 && parsedDecimal<=50000)
            //     return null;

            

            Decimal parsedDecimal;

            try
            {
                Decimal.TryParse(value.ToString(), out parsedDecimal);

                if(parsedDecimal == 0) {
                    generatezero(validationContext);
                    return null;
                }

                if(parsedDecimal>=100 && parsedDecimal<=50000)
                    return null;
                
                return new ValidationResult("");
            }
            catch (System.Exception)
            {
                generatezero(validationContext);
                return null;
            }


            
            

            // return base.IsValid(value, validationContext);
        }

        private void generatezero(ValidationContext validationContext){
            var prop = validationContext.ObjectType.GetProperty(validationContext.MemberName);
            prop.SetValue(validationContext.ObjectInstance, 0.00m);
        }
    }
}