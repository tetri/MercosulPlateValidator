// tests/PlacaMercosulValidator.Tests/ValidatorsTests/BrazilPlateValidatorTests.cs

// tests/PlacaMercosulValidator.Tests/ValidatorsTests/BrazilPlateValidatorTests.cs
using MercosulPlateValidator.Validators;

namespace MercosulPlateValidator.Tests.ValidatorsTests
{
    public class BrazilPlateValidatorTests
    {
        [Theory]
        [InlineData("ABC1234", true)] // Formato antigo sem hífen
        [InlineData("ABC-1234", true)] // Formato antigo com hífen
        [InlineData("ABC1D23", true)] // Formato novo
        [InlineData("AB1D234", false)] // Formato inválido
        public void ValidateBrazilianPlate_ShouldReturnCorrectResult(string plate, bool expected)
        {
            var validator = new BrazilPlateValidator();
            var result = validator.Validate(plate);
            Assert.Equal(expected, result.IsValid);
        }
    }
}