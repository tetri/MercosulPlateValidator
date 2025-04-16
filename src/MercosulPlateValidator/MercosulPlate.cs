using MercosulPlateValidator.Models;
using MercosulPlateValidator.Validators;

namespace MercosulPlateValidator
{
    public static class MercosulPlate
    {
        public static PlateValidationResult ValidateBrazilianPlate(string plate)
        {
            var validator = new BrazilPlateValidator();
            return validator.Validate(plate);
        }

        public static PlateValidationResult ValidatePlate(string plate)
        {
            return CountryIdentifier.IdentifyCountry(plate);
        }
    }
}