using System;
using System.Globalization;
using System.Windows.Controls;
using CourseWorkDB_DudasVI.Views.UserControls;

namespace CourseWorkDB_DudasVI.Views.Rules
{
    public class ToDateRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime date = (DateTime)value;

            return new ValidationResult(date > EditOrder.from, "Кінець терміну не повинен бути меншим його початку!");
        }
    }
    public class FromDateRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime date = (DateTime)value;

            return new ValidationResult(date < EditOrder.to, "Початок терміну не повинен бути більшим його кінця");
        }
    }
}