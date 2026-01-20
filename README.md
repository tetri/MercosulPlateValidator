# MercosulPlateValidator

[![NuGet Version](https://img.shields.io/nuget/v/tetri.net.MercosulPlateValidator.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/tetri.net.MercosulPlateValidator/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/tetri.net.MercosulPlateValidator.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/tetri.net.MercosulPlateValidator/)
[![License](https://img.shields.io/github/license/tetri/MercosulPlateValidator.svg?style=flat-square&logo=github)](LICENSE)
[![Github Build Status](https://img.shields.io/github/actions/workflow/status/tetri/MercosulPlateValidator/publish.yml?style=flat-square&logo=github)](https://github.com/tetri/MercosulPlateValidator/actions)
[![AppVeyor Build Status](https://img.shields.io/appveyor/build/tetri/MercosulPlateValidator?style=flat-square&logo=appveyor)](https://ci.appveyor.com/project/tetri/MercosulPlateValidator)
[![Code Coverage](https://img.shields.io/badge/coverage-100%25-brightgreen?style=flat-square)](https://github.com/tetri/MercosulPlateValidator)


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
```

## Requisitos

- .NET Standard 2.0 ou superior
- Compatível com .NET Framework 4.6.1+, .NET Core 2.0+, .NET 5.0+, .NET 6.0+, .NET 7.0+, .NET 8.0+

## Instalação

### Package Manager Console
```powershell
Install-Package tetri.net.MercosulPlateValidator
```

### .NET CLI
```bash
dotnet add package tetri.net.MercosulPlateValidator
```

### PackageReference
```xml
<PackageReference Include="tetri.net.MercosulPlateValidator" Version="0.2.1" />
```

## Formatos Suportados

### Brasil
- **Antigo**: `ABC1234` ou `ABC-1234` (3 letras + 4 dígitos)
- **Novo Mercosul**: `ABC1D23` ou `ABC 1D23` (3 letras + 1 dígito + 1 letra + 2 dígitos)

### Argentina
- **Antigo**: `ABC123` ou `ABC 123` (3 letras + 3 dígitos)
- **Novo Mercosul**: `AB123CD` ou `AB 123 CD` (2 letras + 3 dígitos + 2 letras)

### Paraguai
- **Antigo**: `1234ABC` ou `1234 ABC` (4 dígitos + 3 letras)
- **Novo Mercosul**: `ABC1234` ou `ABC 1234` (3 letras + 4 dígitos)
- **Motos**: `123ABC` ou `123 ABC` (3 dígitos + 3 letras)

### Uruguai
- **Novo Mercosul**: `AB12345` ou `AB 12345` (2 letras + 5 dígitos)
- **Oficial**: `A123456` ou `A 123456` (1 letra + 6 dígitos)

## Resultado da Validação

O método retorna um objeto `PlateValidationResult` com as seguintes propriedades:

- `IsValid` (bool): Indica se a placa é válida
- `Country` (string): País identificado ("Brazil", "Argentina", "Paraguay", "Uruguay")
- `PlateType` (string): Tipo de placa ("Old", "New", "Motorcycle", "Official")
- `ErrorMessage` (string): Mensagem de erro caso a placa seja inválida

## Exemplos Adicionais

```csharp
using MercosulPlateValidator;

// Validar placa argentina (formato antigo)
var argentinaOld = MercosulPlate.ValidatePlate("ABC 123");
Console.WriteLine($"Válida: {argentinaOld.IsValid}, País: {argentinaOld.Country}, Tipo: {argentinaOld.PlateType}");

// Validar placa brasileira (formato novo)
var brazilNew = MercosulPlate.ValidateBrazilianPlate("ABC1D23");
Console.WriteLine($"Válida: {brazilNew.IsValid}, Tipo: {brazilNew.PlateType}");

// Tratamento de erro
var invalidPlate = MercosulPlate.ValidatePlate("INVALID");
if (!invalidPlate.IsValid)
{
    Console.WriteLine($"Erro: {invalidPlate.ErrorMessage}");
}
```

## Contribuindo

Contribuições são bem-vindas! Por favor, leia o [Código de Conduta](CODE_OF_CONDUCT.md) antes de contribuir.

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona MinhaFeature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

## Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo [LICENSE](LICENSE) para detalhes.

## Segurança

Para reportar vulnerabilidades de segurança, consulte [SECURITY.md](SECURITY.md).