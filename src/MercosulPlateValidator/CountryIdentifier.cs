using System.Collections.Generic;
using System.Linq;
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
            var results = new List<PlateValidationResult>();

            foreach (var validator in Validators)
            {
                var result = validator.Validate(plate);
                if (result.IsValid)
                {
                    results.Add(result);
                }
            }

            if (results.Any())
            {
                // Pega o primeiro resultado como o principal, mas preenche PossibleCountries
                var firstMatch = results.First();
                firstMatch.PossibleCountries = results.Select(r => r.Country).ToList();
                return firstMatch;
            }

            return new PlateValidationResult
            {
                IsValid = false,
                Country = Country.Unknown,
                PlateType = PlateType.Unknown,
                ErrorMessage = "Placa não corresponde a nenhum país do Mercosul"
            };
        }
    }
}