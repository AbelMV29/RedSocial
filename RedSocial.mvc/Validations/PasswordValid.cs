using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RedSocial.mvc.Validations
{
    public class PasswordValid : ValidationAttribute
    {
        string regularExpression = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$";
        private readonly Regex regex;
        public PasswordValid()
        { 
            regex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$");
        }

        public override bool IsValid(object? value)
        {
            string email = (string)value;

            return regex.IsMatch(email);
        }
    }
}
