using MercosulPlateValidator.Validators;

namespace MercosulPlateValidator.Tests.ValidatorsTests
{
    public class UruguayPlateValidatorTests
    {
        private readonly UruguayPlateValidator _validator = new UruguayPlateValidator();

        [Theory]
        [InlineData("ABC 1234", true, "Old")]        // Formato antigo
        [InlineData("ABC1234", true, "Old")]         // Sem espaço
        [InlineData("AB 12345", true, "New")]        // Formato novo
        [InlineData("AB12345", true, "New")]         // Sem espaço
        [InlineData("A 123456", true, "Official")]    // Oficial
        [InlineData("ABC 123", false)]               // Inválido
        [InlineData("123 ABC", false)]               // Inválido
        public void ValidateUruguayPlate_ShouldReturnCorrectResult(string plate, bool expectedValid, string plateType = null)
        {
            var result = _validator.Validate(plate);
            Assert.Equal(expectedValid, result.IsValid);

            if (expectedValid)
                Assert.Equal(plateType, result.PlateType);
        }
    }
}