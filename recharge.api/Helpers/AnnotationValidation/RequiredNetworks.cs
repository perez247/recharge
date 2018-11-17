using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace recharge.api.Helpers.AnnotationValidation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class RequiredNetworks : ValidationAttribute
    {
        readonly ICollection<string> _networks  = new List<string>() 
                                                {
                                                    "mtn", 
                                                    "glo", 
                                                    "9mobile", 
                                                    "airtel"
                                                };

    public override bool IsValid(object value)
    {
        var network = (String)value;
        bool result = false;
        if (_networks.Contains(network))
        {
            result = true;
        }
        return result;
    }
    }
}