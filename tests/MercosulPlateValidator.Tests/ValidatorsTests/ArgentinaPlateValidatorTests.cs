using MercosulPlateValidator.Validators;

namespace MercosulPlateValidator.Tests.ValidatorsTests
{
    public class ArgentinaPlateValidatorTests
    {
        private readonly ArgentinaPlateValidator _validator = new ArgentinaPlateValidator();

        [Theory]
        // Formatos antigos (AAA 999)
        [InlineData("ABC 123", true, "Old")]
        [InlineData("ABC123", true, "Old")]
        [InlineData("AB1 234", false)] // Letra no meio dos números - inválido
        [InlineData("123 ABC", false)] // Ordem inversa - inválido

        // Formatos novos (AA 999 AA)
        [InlineData("AB 123 CD", true, "New")]
        [InlineData("AB123CD", true, "New")]
        [InlineData("AB 12 CD", false)] // Poucos dígitos
        [InlineData("A1 234 BB", false)] // Formato misturado

        // Casos extremos
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("ABC-1234", false)] // Formato brasileiro
        public void ValidateArgentinaPlate_ShouldReturnCorrectResult(
            string plate, bool expectedValid, string plateType = null)
        {
            var result = _validator.Validate(plate);

            Assert.Equal(expectedValid, result.IsValid);

            if (expectedValid)
            {
                Assert.Equal("Argentina", result.Country);
                Assert.Equal(plateType, result.PlateType);
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
            Assert.Equal("New", result.PlateType);
        }

        [Fact]
        public void Validate_ShouldIdentifyOldFormatCorrectly()
        {
            var oldFormatPlate = "ABC 123";
            var result = _validator.Validate(oldFormatPlate);

            Assert.True(result.IsValid);
            Assert.Equal("Old", result.PlateType);
        }
    }
}