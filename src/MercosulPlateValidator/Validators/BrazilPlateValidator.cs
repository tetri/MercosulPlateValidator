using MercosulPlateValidator.Models;

namespace MercosulPlateValidator.Validators
{
    public class BrazilPlateValidator : BasePlateValidator
    {
        public override PlateValidationResult Validate(string plate)
        {
            var result = new PlateValidationResult { Country = "Brazil" };

            // Formato antigo: AAA-9999
            if (ValidateFormat(plate, @"^[A-Za-z]{3}-?\d{4}$"))
            {
                result.IsValid = true;
                result.PlateType = "Old";
                return result;
            }

            // Formato novo: AAA9A99
            if (ValidateFormat(plate, @"^[A-Za-z]{3}\d[A-Za-z]\d{2}$"))
            {
                result.IsValid = true;
                result.PlateType = "New";
                return result;
            }

            result.IsValid = false;
            result.ErrorMessage = "Formato de placa brasileira inválido";
            return result;
        }
    }
}