using MercosulPlateValidator.Validators;

namespace MercosulPlateValidator.Tests.ValidatorsTests
{
    public class ParaguayPlateValidatorTests
    {
        private readonly ParaguayPlateValidator _validator = new ParaguayPlateValidator();

        [Theory]
        [InlineData("1234 ABC", true, "Old")]       // Formato antigo
        [InlineData("1234ABC", true, "Old")]       // Sem espaço
        [InlineData("ABC 1234", true, "New")]      // Formato novo
        [InlineData("ABC1234", true, "New")]       // Sem espaço
        [InlineData("123 ABC", true, "Motorcycle")]// Motos
        [InlineData("12 3456", false)]             // Inválido
        [InlineData("ABCD 123", false)]            // Inválido
        public void ValidateParaguayPlate_ShouldReturnCorrectResult(string plate, bool expectedValid, string plateType = null)
        {
            var result = _validator.Validate(plate);
            Assert.Equal(expectedValid, result.IsValid);

            if (expectedValid)
                Assert.Equal(plateType, result.PlateType);
        }
    }
}