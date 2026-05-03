using System.Text.RegularExpressions;
using MercosulPlateValidator.Models;
using MercosulPlateValidator.Resources;

namespace MercosulPlateValidator.Validators
{
    public class ArgentinaPlateValidator : BasePlateValidator
    {
        private static readonly Regex OldFormatRegex = new Regex(@"^[A-Z]{3}\d{3}$", RegexOptions.Compiled);
        private static readonly Regex NewFormatRegex = new Regex(@"^[A-Z]{2}\d{3}[A-Z]{2}$", RegexOptions.Compiled);

        public override PlateValidationResult Validate(string plate)
        {
            var result = new PlateValidationResult { Country = Country.Argentina };
            var sanitizedPlate = Sanitize(plate);

            // Formato antiguo: AAA999
            if (OldFormatRegex.IsMatch(sanitizedPlate))
            {
                result.IsValid = true;
                result.PlateType = PlateType.Old;
                return result;
            }

            // Formato nuevo: AA999AA
            if (NewFormatRegex.IsMatch(sanitizedPlate))
            {
                result.IsValid = true;
                result.PlateType = PlateType.New;
                return result;
            }

            result.IsValid = false;
            result.ErrorMessage = Messages.InvalidArgentinaPlate;
            return result;
        }
    }
}