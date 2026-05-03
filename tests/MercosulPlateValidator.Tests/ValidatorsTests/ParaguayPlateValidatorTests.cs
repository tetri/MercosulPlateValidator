using Xunit;
using MercosulPlateValidator.Models;
using MercosulPlateValidator.Validators;

namespace MercosulPlateValidator.Tests.ValidatorsTests
{
    public class ParaguayPlateValidatorTests
    {
        private readonly ParaguayPlateValidator _validator = new ParaguayPlateValidator();

        [Theory]
        [InlineData("1234 ABC", true, PlateType.Old)]       // Formato antigo
        [InlineData("1234ABC", true, PlateType.Old)]       // Sem espaço
        [InlineData("ABC 1234", true, PlateType.New)]      // Formato novo
        [InlineData("ABC1234", true, PlateType.New)]       // Sem espaço
        [InlineData("123 ABC", true, PlateType.Motorcycle)]// Motos
        [InlineData("12 3456", false, PlateType.Unknown)]             // Inválido
        [InlineData("ABCD 123", false, PlateType.Unknown)]            // Inválido
        public void ValidateParaguayPlate_ShouldReturnCorrectResult(string? plate, bool expectedValid, PlateType expectedPlateType)
        {
            var result = _validator.Validate(plate);
            Assert.Equal(expectedValid, result.IsValid);

            if (expectedValid)
                Assert.Equal(expectedPlateType, result.PlateType);
        }
    }
}