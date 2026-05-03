using Xunit;
using MercosulPlateValidator.Models;
using MercosulPlateValidator.Validators;

namespace MercosulPlateValidator.Tests.ValidatorsTests
{
    public class UruguayPlateValidatorTests
    {
        private readonly UruguayPlateValidator _validator = new UruguayPlateValidator();

        [Theory]
        [InlineData("ABC 1234", true, PlateType.Old)]        // Formato antigo
        [InlineData("ABC1234", true, PlateType.Old)]         // Sem espaço
        [InlineData("AB 12345", true, PlateType.New)]        // Formato novo
        [InlineData("AB12345", true, PlateType.New)]         // Sem espaço
        [InlineData("A 123456", true, PlateType.Official)]    // Oficial
        [InlineData("ABC 123", false, PlateType.Unknown)]               // Inválido
        [InlineData("123 ABC", false, PlateType.Unknown)]               // Inválido
        public void ValidateUruguayPlate_ShouldReturnCorrectResult(string? plate, bool expectedValid, PlateType expectedPlateType)
        {
            var result = _validator.Validate(plate);
            Assert.Equal(expectedValid, result.IsValid);

            if (expectedValid)
                Assert.Equal(expectedPlateType, result.PlateType);
        }
    }
}