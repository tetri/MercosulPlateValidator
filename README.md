# MercosulPlateValidator

Biblioteca .NET para validação de placas de veículos do Mercosul (Brasil, Argentina, Paraguai, Uruguai) e identificação do país de origem.

## Funcionalidades

- Validação de placas de todos os países do Mercosul
- Suporte para formatos antigos e novos
- Identificação do país de origem da placa
- Fácil integração em projetos .NET

## Como usar

```csharp
// Instale o pacote
// NuGet: Install-Package tetri.net.MercosulPlateValidator

using MercosulPlateValidator;

// Validar uma placa brasileira
var result = MercosulPlate.ValidateBrazilianPlate("ABC1D23");
if (result.IsValid)
{
    Console.WriteLine($"Placa válida do {result.Country}, formato {result.PlateType}");
}

// Identificar o país de qualquer placa do Mercosul
var identification = MercosulPlate.ValidatePlate("AB 123 CD");
if (identification.IsValid)
{
    Console.WriteLine($"Placa do {identification.Country}");
}

// Validar placa paraguaia
var paraguayResult = MercosulPlate.ValidatePlate("1234 ABC");
if (paraguayResult.IsValid)
{
    Console.WriteLine($"Placa {paraguayResult.PlateType} do {paraguayResult.Country}");
}

// Validar placa uruguaia
var uruguayResult = MercosulPlate.ValidatePlate("AB 12345");
if (uruguayResult.IsValid)
{
    Console.WriteLine($"Placa {uruguayResult.PlateType} do {uruguayResult.Country}");
}
````