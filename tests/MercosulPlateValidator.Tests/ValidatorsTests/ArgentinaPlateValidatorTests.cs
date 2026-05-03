using Xunit;
using MercosulPlateValidator.Models;
using MercosulPlateValidator.Validators;

namespace MercosulPlateValidator.Tests.ValidatorsTests
{
    public class ArgentinaPlateValidatorTests
    {
        private readonly ArgentinaPlateValidator _validator = new ArgentinaPlateValidator();

        [Theory]
        // Formatos antigos (AAA 999)
        [InlineData("ABC 123", true, PlateType.Old)]
        [InlineData("ABC123", true, PlateType.Old)]
        [InlineData("AB1 234", false, PlateType.Unknown)] // Letra no meio dos números - inválido
        [InlineData("123 ABC", false, PlateType.Unknown)] // Ordem inversa - inválido

        // Formatos novos (AA 999 AA)
        [InlineData("AB 123 CD", true, PlateType.New)]
        [InlineData("AB123CD", true, PlateType.New)]
        [InlineData("AB 12 CD", false, PlateType.Unknown)] // Poucos dígitos
        [InlineData("A1 234 BB", false, PlateType.Unknown)] // Formato misturado

        // Casos extremos
        [InlineData("", false, PlateType.Unknown)]
        [InlineData(null, false, PlateType.Unknown)]
        [InlineData("ABC-1234", false, PlateType.Unknown)] // Formato brasileiro
        public void ValidateArgentinaPlate_ShouldReturnCorrectResult(
            string? plate, bool expectedValid, PlateType expectedPlateType)
        {
            var result = _validator.Validate(plate);

            Assert.Equal(expectedValid, result.IsValid);

            if (expectedValid)
            {
                Assert.Equal(Country.Argentina, result.Country);
                Assert.Equal(expectedPlateType, result.PlateType);
            }
            else
            {
                Assert.False(string.IsNullOrEmpty(result.ErrorMessage));
            }
        }

        [Fact]
        public void Validate_ShouldIdentifyNewFormatCorrectly()
        {
            var newFormatPlate = "AB 123 CD";
            var result = _validator.Validate(newFormatPlate);

            Assert.True(result.IsValid);
            Assert.Equal(PlateType.New, result.PlateType);
        }

        [Fact]
        public void Validate_ShouldIdentifyOldFormatCorrectly()
        {
            var oldFormatPlate = "ABC 123";
            var result = _validator.Validate(oldFormatPlate);

            Assert.True(result.IsValid);
            Assert.Equal(PlateType.Old, result.PlateType);
        }
    }
}