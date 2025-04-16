using MercosulPlateValidator.Models;

namespace MercosulPlateValidator.Validators
{
    public class ArgentinaPlateValidator : BasePlateValidator
    {
        public override PlateValidationResult Validate(string plate)
        {
            var result = new PlateValidationResult { Country = "Argentina" };

            // Formato antigo: AAA 999
            if (ValidateFormat(plate, @"^[A-Za-z]{3}\s?\d{3}$"))
            {
                result.IsValid = true;
                result.PlateType = "Old";
                return result;
            }

            // Formato novo: AA 999 AA
            if (ValidateFormat(plate, @"^[A-Za-z]{2}\s?\d{3}\s?[A-Za-z]{2}$"))
            {
                result.IsValid = true;
                result.PlateType = "New";
                return result;
            }

            result.IsValid = false;
            result.ErrorMessage = "Formato de placa argentina inválido";
            return result;
        }
    }
}