using System.Globalization;
using System.Threading;
using MercosulPlateValidator.Models;
using Xunit;

namespace MercosulPlateValidator.Tests
{
    public class LocalizationTests
    {
        [Fact]
        public void ShouldReturnPortugueseErrorMessageByDefault()
        {
            // Arrange
            var culture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // Act
            var result = MercosulPlate.ValidatePlate("INVALID");

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Placa não corresponde a nenhum país do Mercosul", result.ErrorMessage);
        }

        [Fact]
        public void ShouldReturnSpanishErrorMessageWhenCultureIsSpanish()
        {
            // Arrange
            var culture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // Act
            var result = MercosulPlate.ValidatePlate("INVALID");

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("La placa no coincide con ningún país del Mercosur", result.ErrorMessage);
        }

        [Fact]
        public void ShouldReturnEnglishErrorMessageWhenCultureIsEnglish()
        {
            // Arrange
            var culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // Act
            var result = MercosulPlate.ValidatePlate("INVALID");

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Plate does not match any Mercosur country", result.ErrorMessage);
        }

        [Fact]
        public void ShouldReturnSpecificCountrySpanishErrorMessage()
        {
            // Arrange
            var culture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // Act
            var result = MercosulPlate.ValidateBrazilianPlate("INVALID");

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal("Formato de placa brasileña inválido", result.ErrorMessage);
        }
    }
}
