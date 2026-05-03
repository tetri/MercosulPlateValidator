using System.Text.RegularExpressions;
using MercosulPlateValidator.Models;

namespace MercosulPlateValidator.Validators
{
    public abstract class BasePlateValidator
    {
        public abstract PlateValidationResult Validate(string plate);

        protected string Sanitize(string plate)
        {
            if (string.IsNullOrWhiteSpace(plate))
                return string.Empty;

            // Elimina caracteres no alfanuméricos y convierte a mayúsculas
            var sanitized = Regex.Replace(plate, @"[^a-zA-Z0-9]", "");
            return sanitized.ToUpperInvariant();
        }
    }
}