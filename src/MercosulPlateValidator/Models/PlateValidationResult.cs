namespace MercosulPlateValidator.Models
{
    public class PlateValidationResult
    {
        public bool IsValid { get; set; }
        public string Country { get; set; }
        public string PlateType { get; set; } // "Old" or "New"
        public string ErrorMessage { get; set; }
    }
}