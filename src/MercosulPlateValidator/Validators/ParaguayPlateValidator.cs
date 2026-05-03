using System.Text.RegularExpressions;
using MercosulPlateValidator.Models;
using MercosulPlateValidator.Resources;

namespace MercosulPlateValidator.Validators
{
    public class ParaguayPlateValidator : BasePlateValidator
    {
        private static readonly Regex OldFormatRegex = new Regex(@"^\d{4}[A-Z]{3}$", RegexOptions.Compiled);
        private static readonly Regex NewFormatRegex = new Regex(@"^[A-Z]{3}\d{4}$", RegexOptions.Compiled);
        private static readonly Regex MotorcycleFormatRegex = new Regex(@"^\d{3}[A-Z]{3}$", RegexOptions.Compiled);

        public override PlateValidationResult Validate(string plate)
        {
            var result = new PlateValidationResult { Country = Country.Paraguay };
            var sanitizedPlate = Sanitize(plate);

            // Formato antiguo: 1234ABC
            if (OldFormatRegex.IsMatch(sanitizedPlate))
            {
                result.IsValid = true;
                result.PlateType = PlateType.Old;
                return result;
            }

            // Formato nuevo: ABC1234
            if (NewFormatRegex.IsMatch(sanitizedPlate))
            {
                result.IsValid = true;
                result.PlateType = PlateType.New;
                return result;
            }

            // Formato motos: 123ABC
            if (MotorcycleFormatRegex.IsMatch(sanitizedPlate))
            {
                result.IsValid = true;
                result.PlateType = PlateType.Motorcycle;
                return result;
            }

            result.IsValid = false;
            result.ErrorMessage = Messages.InvalidParaguayPlate;
            return result;
        }
    }
}