using Xunit;
using MercosulPlateValidator.Models;

namespace MercosulPlateValidator.Tests
{
    public class CountryIdentifierTests
    {
        [Theory]
        // Brasil - Formatos
        [InlineData("ABC1234", Country.Brazil, PlateType.Old)] // Antigo sem hífen
        [InlineData("ABC-1234", Country.Brazil, PlateType.Old)] // Antigo com hífen
        [InlineData("ABC1D23", Country.Brazil, PlateType.New)] // Novo formato
        [InlineData("ABC 1D23", Country.Brazil, PlateType.New)] // Novo com espaço

        // Argentina - Formatos
        [InlineData("ABC123", Country.Argentina, PlateType.Old)] // Antigo sem espaço
        [InlineData("ABC 123", Country.Argentina, PlateType.Old)] // Antigo com espaço
        [InlineData("AB123CD", Country.Argentina, PlateType.New)] // Novo sem espaço
        [InlineData("AB 123 CD", Country.Argentina, PlateType.New)] // Novo com espaço

        // Paraguai - Formatos
        [InlineData("1234ABC", Country.Paraguay, PlateType.Old)] // Antigo sem espaço
        [InlineData("1234 ABC", Country.Paraguay, PlateType.Old)] // Antigo com espaço
        // [InlineData("ABC 1234", Country.Paraguay, PlateType.New)] // Novo com espaço - COMENTADO POIS BRASIL TEM PRIORIDADE NO RETORNO PRINCIPAL
        [InlineData("123ABC", Country.Paraguay, PlateType.Motorcycle)] // Moto

        // Uruguai - Formatos
        [InlineData("AB12345", Country.Uruguay, PlateType.New)] // Novo sem espaço
        [InlineData("AB 12345", Country.Uruguay, PlateType.New)] // Novo com espaço
        [InlineData("A123456", Country.Uruguay, PlateType.Official)] // Oficial sem espaço
        [InlineData("A 123456", Country.Uruguay, PlateType.Official)] // Oficial com espaço

        // Casos inválidos
        [InlineData("", Country.Unknown, PlateType.Unknown)] // String vazia
        [InlineData(null, Country.Unknown, PlateType.Unknown)] // Null
        [InlineData("INVALID", Country.Unknown, PlateType.Unknown)] // Placa inválida
        [InlineData("1234567", Country.Unknown, PlateType.Unknown)] // Números apenas
        [InlineData("ABCDEFG", Country.Unknown, PlateType.Unknown)] // Letras apenas
        public void IdentifyCountry_ShouldCorrectlyIdentify(
            string? plate, Country expectedCountry, PlateType expectedPlateType)
        {
            // Act
            var result = CountryIdentifier.IdentifyCountry(plate);

            // Assert
            if (expectedCountry == Country.Unknown)
            {
                Assert.False(result.IsValid);
                Assert.Equal("Placa não corresponde a nenhum país do Mercosul", result.ErrorMessage);
            }
            else
            {
                Assert.True(result.IsValid);
                Assert.Equal(expectedCountry, result.Country);
                Assert.Equal(expectedPlateType, result.PlateType);
                Assert.Null(result.ErrorMessage);
            }
        }

        [Fact]
        public void IdentifyCountry_ShouldPrioritizeBrazilOverOthers()
        {
            // Placa que poderia ser confundida (formato brasileiro antigo e paraguaio novo)
            var plate = "ABC1234";
            var result = CountryIdentifier.IdentifyCountry(plate);

            Assert.True(result.IsValid);
            Assert.Equal(Country.Brazil, result.Country); // Deve priorizar Brasil
            Assert.Equal(PlateType.Old, result.PlateType);

            // Agora deve conter múltiplos países possíveis
            Assert.Contains(Country.Brazil, result.PossibleCountries);
            Assert.Contains(Country.Paraguay, result.PossibleCountries);
            Assert.Contains(Country.Uruguay, result.PossibleCountries);
        }

        [Fact]
        public void IdentifyCountry_ShouldHandleAllValidatorsInOrder()
        {
            // Verifica a ordem dos validadores (Brasil -> Argentina -> Paraguai -> Uruguai)
            var brazilPlate = "ABC1D23";
            var argentinaPlate = "AB123CD";
            var uruguayPlate = "AB12345";

            Assert.Equal(Country.Brazil, CountryIdentifier.IdentifyCountry(brazilPlate).Country);
            Assert.Equal(Country.Argentina, CountryIdentifier.IdentifyCountry(argentinaPlate).Country);
            Assert.Equal(Country.Uruguay, CountryIdentifier.IdentifyCountry(uruguayPlate).Country);
        }
    }
}
