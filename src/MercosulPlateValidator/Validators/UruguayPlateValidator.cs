using MercosulPlateValidator.Models;

namespace MercosulPlateValidator.Validators
{
    public class UruguayPlateValidator : BasePlateValidator
    {
        public override PlateValidationResult Validate(string plate)
        {
            var result = new PlateValidationResult { Country = "Uruguay" };

            // Formato antigo: ABC 1234
            if (ValidateFormat(plate, @"^[A-Za-z]{3}\s?\d{4}$"))
            {
                result.IsValid = true;
                result.PlateType = "Old";
                return result;
            }

            // Formato novo: AB 12345
            if (ValidateFormat(plate, @"^[A-Za-z]{2}\s?\d{5}$"))
            {
                result.IsValid = true;
                result.PlateType = "New";
                return result;
            }

            // Formato especial (oficial): A 123456
            if (ValidateFormat(plate, @"^[A-Za-z]{1}\s?\d{6}$"))
            {
                result.IsValid = true;
                result.PlateType = "Official";
                return result;
            }

            result.IsValid = false;
            result.ErrorMessage = "Formato de placa uruguaia inválido";
            return result;
        }
    }
}