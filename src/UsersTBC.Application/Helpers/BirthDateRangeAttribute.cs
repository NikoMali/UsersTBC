using System;
using System.ComponentModel.DataAnnotations;


namespace UsersTBC.Application.Helpers
{
    public class BirthDateRangeAttribute : ValidationAttribute
{
    // private string _date;

    // public BirthDateRangeAttribute(string date)
    // {
    //     _date = date;
    // }
    public readonly string _getProperty;
    public readonly int _minAge;
   

    public BirthDateRangeAttribute(string getProperty, int minAge)
    {
        _getProperty = getProperty;
        _minAge = minAge;
    }
    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
    {
            try {
                var birthDate = validationContext.ObjectType.GetProperty(_getProperty);
                var birthDateValue = birthDate.GetValue(validationContext.ObjectInstance, null);
                var currentYear = DateTime.Now.Year;
               
                
                if (currentYear - DateTime.Parse(birthDateValue.ToString()).Year < _minAge)
                {
                    return new ValidationResult(GetErrorMessage());
                }

                return ValidationResult.Success;
            }
            catch
                {
                    return new ValidationResult(GetErrorMessageForText());
                }
    }


    public string GetErrorMessage()
    {
        return $"18 year or more";
    }
     public string GetErrorMessageForText()
    {
        return $"Pls fill correct, example: 10/04/1995";
    }
}
}