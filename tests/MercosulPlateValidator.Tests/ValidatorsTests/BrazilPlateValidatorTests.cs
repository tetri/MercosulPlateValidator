using Xunit;
using MercosulPlateValidator.Models;
using MercosulPlateValidator.Validators;

namespace MercosulPlateValidator.Tests.ValidatorsTests
{
    public class BrazilPlateValidatorTests
    {
        private readonly BrazilPlateValidator _validator = new BrazilPlateValidator();

        [Theory]
        [InlineData("ABC1234", true, PlateType.Old)] // Formato antigo sem hífen
        [InlineData("ABC-1234", true, PlateType.Old)] // Formato antigo com hífen
        [InlineData("ABC1D23", true, PlateType.New)] // Formato novo
        [InlineData("AB1D234", false, PlateType.Unknown)] // Formato inválido
        public void ValidateBrazilianPlate_ShouldReturnCorrectResult(string plate, bool expectedValid, PlateType expectedPlateType)
        {
            var result = _validator.Validate(plate);
            Assert.Equal(expectedValid, result.IsValid);

            if (expectedValid)
            {
                Assert.Equal(Country.Brazil, result.Country);
                Assert.Equal(expectedPlateType, result.PlateType);
            }
        }
    }
}