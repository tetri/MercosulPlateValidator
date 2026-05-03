using System.Collections.Generic;
using System.Linq;
using MercosulPlateValidator.Models;
using MercosulPlateValidator.Resources;
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
                // Toma el primer resultado como el principal, pero completa PossibleCountries
                var firstMatch = results.First();
                firstMatch.PossibleCountries = results.Select(r => r.Country).ToList();
                return firstMatch;
            }

            return new PlateValidationResult
            {
                IsValid = false,
                Country = Country.Unknown,
                PlateType = PlateType.Unknown,
                ErrorMessage = Messages.NoMatchFound
            };
        }
    }
}