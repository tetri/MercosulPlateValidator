// CountryIdentifier.cs
using System.Collections.Generic;

using MercosulPlateValidator.Models;
using MercosulPlateValidator.Validators;

namespace MercosulPlateValidator
{
    public static class CountryIdentifier
    {
        private static readonly List<BasePlateValidator> Validators = new List<BasePlateValidator>
        {
            new BrazilPlateValidator(),
            new ArgentinaPlateValidator(),
            new ParaguayPlateValidator(),
            new UruguayPlateValidator()
        };

        public static PlateValidationResult IdentifyCountry(string plate)
        {
            foreach (var validator in Validators)
            {
                var result = validator.Validate(plate);
                if (result.IsValid)
                {
                    return result;
                }
            }

            return new PlateValidationResult
            {
                IsValid = false,
                ErrorMessage = "Placa não corresponde a nenhum país do Mercosul"
            };
        }
    }
}