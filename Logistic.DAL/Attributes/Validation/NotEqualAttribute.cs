using System.ComponentModel.DataAnnotations;

namespace Logistic.DAL.Attributes.Validation
{
    public class NotEqualAttribute : ValidationAttribute
    {
        private string OtherProperty { get; set; }//другое св-во
        private string MainPropertyName { get; }//имя основного свойства
        private string OtherPropertyName { get; }//другое основное свойства

        public NotEqualAttribute(string otherProperty, string mainPropertyName, string otherPropertyName)//Не равный атрибут
        {
            OtherProperty = otherProperty;
            MainPropertyName = mainPropertyName;
            OtherPropertyName = otherPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)//проверка
        {
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
            var otherValue = otherPropertyInfo?.GetValue(validationContext.ObjectInstance);

            return value?.ToString()?.Equals(otherValue?.ToString()) is true or null
                ? new ValidationResult($"{MainPropertyName} не должно быть равно {OtherPropertyName}.")
                : ValidationResult.Success;
        }
    }
}