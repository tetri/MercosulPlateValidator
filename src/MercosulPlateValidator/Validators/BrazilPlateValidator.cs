using System.Text.RegularExpressions;
using MercosulPlateValidator.Models;
using MercosulPlateValidator.Resources;

namespace MercosulPlateValidator.Validators
{
    public class BrazilPlateValidator : BasePlateValidator
    {
        private static readonly Regex OldFormatRegex = new Regex(@"^[A-Z]{3}\d{4}$", RegexOptions.Compiled);
        private static readonly Regex NewFormatRegex = new Regex(@"^[A-Z]{3}\d[A-Z]\d{2}$", RegexOptions.Compiled);

        public override PlateValidationResult Validate(string plate)
        {
            var result = new PlateValidationResult { Country = Country.Brazil };
            var sanitizedPlate = Sanitize(plate);

            // Formato antiguo: AAA9999
            if (OldFormatRegex.IsMatch(sanitizedPlate))
            {
                result.IsValid = true;
                result.PlateType = PlateType.Old;
                return result;
            }

            // Formato nuevo: AAA9A99
            if (NewFormatRegex.IsMatch(sanitizedPlate))
            {
                result.IsValid = true;
                result.PlateType = PlateType.New;
                return result;
            }

            result.IsValid = false;
            result.ErrorMessage = Messages.InvalidBrazilPlate;
            return result;
        }
    }
}