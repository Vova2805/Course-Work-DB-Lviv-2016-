using System;
using System.Globalization;
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
}