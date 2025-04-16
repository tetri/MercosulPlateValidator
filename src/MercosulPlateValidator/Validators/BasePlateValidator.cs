using MercosulPlateValidator.Models;

namespace MercosulPlateValidator.Validators
{
    public abstract class BasePlateValidator
    {
        public abstract PlateValidationResult Validate(string plate);

        protected bool ValidateFormat(string plate, string regexPattern)
        {
            if (string.IsNullOrWhiteSpace(plate))
                return false;

            return System.Text.RegularExpressions.Regex.IsMatch(plate, regexPattern);
        }
    }
}