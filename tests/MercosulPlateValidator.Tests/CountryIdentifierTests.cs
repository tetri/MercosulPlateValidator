namespace MercosulPlateValidator.Tests
{
    public class CountryIdentifierTests
    {
        [Theory]
        // Brasil - Formatos
        [InlineData("ABC1234", "Brazil", "Old")] // Antigo sem hífen
        [InlineData("ABC-1234", "Brazil", "Old")] // Antigo com hífen
        [InlineData("ABC1D23", "Brazil", "New")] // Novo formato
        [InlineData("ABC 1D23", "Brazil", "New")] // Novo com espaço

        // Argentina - Formatos
        [InlineData("ABC123", "Argentina", "Old")] // Antigo sem espaço
        [InlineData("ABC 123", "Argentina", "Old")] // Antigo com espaço
        [InlineData("AB123CD", "Argentina", "New")] // Novo sem espaço
        [InlineData("AB 123 CD", "Argentina", "New")] // Novo com espaço

        // Paraguai - Formatos
        [InlineData("1234ABC", "Paraguay", "Old")] // Antigo sem espaço
        [InlineData("1234 ABC", "Paraguay", "Old")] // Antigo com espaço
        //[InlineData("ABC1234", "Paraguay", "New")] // Novo sem espaço
        [InlineData("ABC 1234", "Paraguay", "New")] // Novo com espaço
        [InlineData("123ABC", "Paraguay", "Motorcycle")] // Moto

        // Uruguai - Formatos
        //[InlineData("ABC1234", "Uruguay", "Old")] // Antigo sem espaço
        //[InlineData("ABC 1234", "Uruguay", "Old")] // Antigo com espaço
        [InlineData("AB12345", "Uruguay", "New")] // Novo sem espaço
        [InlineData("AB 12345", "Uruguay", "New")] // Novo com espaço
        [InlineData("A123456", "Uruguay", "Official")] // Oficial sem espaço
        [InlineData("A 123456", "Uruguay", "Official")] // Oficial com espaço

        // Casos inválidos
        [InlineData("", null, null)] // String vazia
        [InlineData(null, null, null)] // Null
        [InlineData("INVALID", null, null)] // Placa inválida
        [InlineData("1234567", null, null)] // Números apenas
        [InlineData("ABCDEFG", null, null)] // Letras apenas
        public void IdentifyCountry_ShouldCorrectlyIdentify(
            string plate, string expectedCountry, string expectedPlateType)
        {
            // Act
            var result = CountryIdentifier.IdentifyCountry(plate);

            // Assert
            if (expectedCountry == null)
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
            Assert.Equal("Brazil", result.Country); // Deve priorizar Brasil
            Assert.Equal("Old", result.PlateType);
        }

        [Fact]
        public void IdentifyCountry_ShouldHandleAllValidatorsInOrder()
        {
            // Verifica a ordem dos validadores (Brasil -> Argentina -> Paraguai -> Uruguai)
            var brazilPlate = "ABC1D23";
            var argentinaPlate = "AB123CD";
            //var paraguayPlate = "ABC1234";
            var uruguayPlate = "AB12345";

            Assert.Equal("Brazil", CountryIdentifier.IdentifyCountry(brazilPlate).Country);
            Assert.Equal("Argentina", CountryIdentifier.IdentifyCountry(argentinaPlate).Country);
            //Assert.Equal("Paraguay", CountryIdentifier.IdentifyCountry(paraguayPlate).Country);
            Assert.Equal("Uruguay", CountryIdentifier.IdentifyCountry(uruguayPlate).Country);
        }
    }
}