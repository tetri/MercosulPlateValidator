using MercosulPlateValidator.Models;

namespace MercosulPlateValidator.Validators
{
    public class ParaguayPlateValidator : BasePlateValidator
    {
        public override PlateValidationResult Validate(string plate)
        {
            var result = new PlateValidationResult { Country = "Paraguay" };

            // Formato antigo: 1234 ABC
            if (ValidateFormat(plate, @"^\d{4}\s?[A-Za-z]{3}$"))
            {
                result.IsValid = true;
                result.PlateType = "Old";
                return result;
            }

            // Formato novo: ABC 1234
            if (ValidateFormat(plate, @"^[A-Za-z]{3}\s?\d{4}$"))
            {
                result.IsValid = true;
                result.PlateType = "New";
                return result;
            }

            // Formato motos: 123 ABC
            if (ValidateFormat(plate, @"^\d{3}\s?[A-Za-z]{3}$"))
            {
                result.IsValid = true;
                result.PlateType = "Motorcycle";
                return result;
            }

            result.IsValid = false;
            result.ErrorMessage = "Formato de placa paraguaia inválido";
            return result;
        }
    }
}