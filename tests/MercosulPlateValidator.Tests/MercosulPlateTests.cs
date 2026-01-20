using Xunit;

namespace MercosulPlateValidator.Tests
{
    public class MercosulPlateTests
    {
        [Theory]
        [InlineData("ABC1234", true)]   // Brasil antigo
        [InlineData("ABC-1234", true)]  // Brasil antigo com hífen
        [InlineData("ABC1D23", true)]   // Brasil novo
        [InlineData("INVALID", false)]  // inválido
        [InlineData("", false)]         // vazio
        [InlineData(null, false)]       // null
        public void ValidateBrazilianPlate_ShouldReturnResult(string? plate, bool expectedValid)
        {
            var result = MercosulPlate.ValidateBrazilianPlate(plate);
            Assert.Equal(expectedValid, result.IsValid);
        }

        [Theory]
        [InlineData("ABC1234", "Brazil")]     // Brasil (prioritário)
        [InlineData("AB123CD", "Argentina")]  // Argentina novo
        [InlineData("1234ABC", "Paraguay")]   // Paraguai antigo
        [InlineData("AB12345", "Uruguay")]    // Uruguai novo
        public void ValidatePlate_ShouldIdentifyCountry(string plate, string expectedCountry)
        {
            var result = MercosulPlate.ValidatePlate(plate);
            Assert.True(result.IsValid);
            Assert.Equal(expectedCountry, result.Country);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("INVALID")]
        public void ValidatePlate_WhenInvalid_ShouldReturnInvalidResult(string? plate)
        {
            var result = MercosulPlate.ValidatePlate(plate);
            Assert.False(result.IsValid);
            Assert.False(string.IsNullOrWhiteSpace(result.ErrorMessage));
        }
    }
}

