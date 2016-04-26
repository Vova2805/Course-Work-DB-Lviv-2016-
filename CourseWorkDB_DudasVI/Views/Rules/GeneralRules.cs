using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using CourseWorkDB_DudasVI.Views.UserControls;

namespace CourseWorkDB_DudasVI.Views.Rules
{
    public class SelectedItemRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return new ValidationResult(value != null, "Оберіть один із варіантів");
        }
    }

    public class ToDateRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var date = (DateTime) value;

            return new ValidationResult(date > EditOrderOrProduct.from,
                "Кінець терміну не повинен бути меншим його початку!");
        }
    }

    public class FromDateRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var date = (DateTime) value;

            return new ValidationResult(date < EditOrderOrProduct.to,
                "Початок терміну не повинен бути більшим його кінця");
        }
    }

    public class EmptyRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var content = (string)value;

            return new ValidationResult(!content.Equals(""),
                "Введіть дані будь ласка");
        }
    }

    public class EmailRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var content = (string)value;
            string s = "[a-z0-9A-z]@[a-zA-z0-9 ]*[.]";
            Match aa = Regex.Match(content??"", s);
            return new ValidationResult(aa.Success,
                "Електронна пошта має не правильний формат");
        }
    }
    public class PasswordLoginRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var content = (string)value;
            return new ValidationResult(content.Length>=5,
                "Мінімальний обсяг символів - 5");
        }
    }
}