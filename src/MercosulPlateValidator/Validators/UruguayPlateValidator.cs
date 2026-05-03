using System.Text.RegularExpressions;
using MercosulPlateValidator.Models;
using MercosulPlateValidator.Resources;

namespace MercosulPlateValidator.Validators
{
    public class UruguayPlateValidator : BasePlateValidator
    {
        private static readonly Regex OldFormatRegex = new Regex(@"^[A-Z]{3}\d{4}$", RegexOptions.Compiled);
        private static readonly Regex NewFormatRegex = new Regex(@"^[A-Z]{2}\d{5}$", RegexOptions.Compiled);
        private static readonly Regex OfficialFormatRegex = new Regex(@"^[A-Z]{1}\d{6}$", RegexOptions.Compiled);

        public override PlateValidationResult Validate(string plate)
        {
            var result = new PlateValidationResult { Country = Country.Uruguay };
            var sanitizedPlate = Sanitize(plate);

            // Formato antiguo: ABC1234
            if (OldFormatRegex.IsMatch(sanitizedPlate))
            {
                result.IsValid = true;
                result.PlateType = PlateType.Old;
                return result;
            }

            // Formato nuevo: AB12345
            if (NewFormatRegex.IsMatch(sanitizedPlate))
            {
                result.IsValid = true;
                result.PlateType = PlateType.New;
                return result;
            }

            // Formato especial (oficial): A123456
            if (OfficialFormatRegex.IsMatch(sanitizedPlate))
            {
                result.IsValid = true;
                result.PlateType = PlateType.Official;
                return result;
            }

            result.IsValid = false;
            result.ErrorMessage = Messages.InvalidUruguayPlate;
            return result;
        }
    }
}