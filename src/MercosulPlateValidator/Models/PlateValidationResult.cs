using System.Collections.Generic;

namespace MercosulPlateValidator.Models
{
    public class PlateValidationResult
    {
        public bool IsValid { get; set; }
        public Country Country { get; set; } = Country.Unknown;
        public PlateType PlateType { get; set; } = PlateType.Unknown;
        public string ErrorMessage { get; set; }
        public List<Country> PossibleCountries { get; set; } = new List<Country>();
    }
}