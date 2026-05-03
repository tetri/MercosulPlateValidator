# MercosulPlateValidator

[![NuGet Version](https://img.shields.io/nuget/v/tetri.net.MercosulPlateValidator.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/tetri.net.MercosulPlateValidator/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/tetri.net.MercosulPlateValidator.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/tetri.net.MercosulPlateValidator/)
[![License](https://img.shields.io/github/license/tetri/MercosulPlateValidator.svg?style=flat-square&logo=github)](LICENSE)
[![Github Build Status](https://img.shields.io/github/actions/workflow/status/tetri/MercosulPlateValidator/publish.yml?style=flat-square&logo=github)](https://github.com/tetri/MercosulPlateValidator/actions)
[![AppVeyor Build Status](https://img.shields.io/appveyor/build/tetri/MercosulPlateValidator?style=flat-square&logo=appveyor)](https://ci.appveyor.com/project/tetri/MercosulPlateValidator)
[![Code Coverage](https://img.shields.io/badge/coverage-100%25-brightgreen?style=flat-square)](https://github.com/tetri/MercosulPlateValidator)


Biblioteca .NET para validación de placas de vehículos del Mercosur (Brasil, Argentina, Paraguay, Uruguay) e identificación del país de origen.

## Funcionalidades

- Validación de placas de todos los países del Mercosur
- Soporte para formatos antiguos y nuevos
- Identificación do país de origen de la placa
- Fácil integración en proyectos .NET

## Cómo usar

```csharp
// Instale el paquete
// NuGet: Install-Package tetri.net.MercosulPlateValidator

using MercosulPlateValidator;

// Validar una placa brasileña
var result = MercosulPlate.ValidateBrazilianPlate("ABC1D23");
if (result.IsValid)
{
    Console.WriteLine($"Placa válida de {result.Country}, formato {result.PlateType}");
}

// Identificar el país de cualquier placa del Mercosur
var identification = MercosulPlate.ValidatePlate("AB 123 CD");
if (identification.IsValid)
{
    Console.WriteLine($"Placa de {identification.Country}");
}

// Validar placa paraguaya
var paraguayResult = MercosulPlate.ValidatePlate("1234 ABC");
if (paraguayResult.IsValid)
{
    Console.WriteLine($"Placa {paraguayResult.PlateType} de {paraguayResult.Country}");
}

// Validar placa uruguaya
var uruguayResult = MercosulPlate.ValidatePlate("AB 12345");
if (uruguayResult.IsValid)
{
    Console.WriteLine($"Placa {uruguayResult.PlateType} de {uruguayResult.Country}");
}
```

## Requisitos

- .NET Standard 2.0 o superior
- Compatible con .NET Framework 4.6.1+, .NET Core 2.0+, .NET 5.0+, .NET 6.0+, .NET 7.0+, .NET 8.0+

## Instalación

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
<PackageReference Include="tetri.net.MercosulPlateValidator" Version="0.3.0" />
```

## Formatos Soportados

### Brasil
- **Antiguo**: `ABC1234` o `ABC-1234` (3 letras + 4 dígitos)
- **Nuevo Mercosul**: `ABC1D23` o `ABC 1D23` (3 letras + 1 dígito + 1 letra + 2 dígitos)

### Argentina
- **Antiguo**: `ABC123` o `ABC 123` (3 letras + 3 dígitos)
- **Nuevo Mercosul**: `AB123CD` o `AB 123 CD` (2 letras + 3 dígitos + 2 letras)

### Paraguay
- **Antiguo**: `1234ABC` o `1234 ABC` (4 dígitos + 3 letras)
- **Nuevo Mercosul**: `ABC1234` o `ABC 1234` (3 letras + 4 dígitos)
- **Motos**: `123ABC` o `123 ABC` (3 dígitos + 3 letras)

### Uruguay
- **Nuevo Mercosul**: `AB12345` o `AB 12345` (2 letras + 5 dígitos)
- **Oficial**: `A123456` o `A 123456` (1 letra + 6 dígitos)

## Resultado de la Validación

El método devuelve un objeto `PlateValidationResult` con las siguientes propiedades:

- `IsValid` (bool): Indica si la placa es válida
- `Country` (enum): País identificado (Brazil, Argentina, Paraguay, Uruguay)
- `PlateType` (enum): Tipo de placa (Old, New, Motorcycle, Official)
- `ErrorMessage` (string): Mensaje de error en caso de que la placa sea inválida (localizado)
- `PossibleCountries` (List<Country>): Lista de posibles países que coinciden con el formato

## Ejemplos Adicionales

```csharp
using MercosulPlateValidator;

// Validar placa argentina (formato antiguo)
var argentinaOld = MercosulPlate.ValidatePlate("ABC 123");
Console.WriteLine($"Válida: {argentinaOld.IsValid}, País: {argentinaOld.Country}, Tipo: {argentinaOld.PlateType}");

// Validar placa brasileña (formato nuevo)
var brazilNew = MercosulPlate.ValidateBrazilianPlate("ABC1D23");
Console.WriteLine($"Válida: {brazilNew.IsValid}, Tipo: {brazilNew.PlateType}");

// Tratamiento de error
var invalidPlate = MercosulPlate.ValidatePlate("INVALID");
if (!invalidPlate.IsValid)
{
    Console.WriteLine($"Error: {invalidPlate.ErrorMessage}");
}
```

## Contribuyendo

¡Las contribuciones son bienvenidas! Por favor, lea el [Código de Conducta](CODE_OF_CONDUCT.md) antes de contribuir.

1. Haga un fork del proyecto
2. Cree una branch para su feature (`git checkout -b feature/MiFeature`)
3. Commit sus cambios (`git commit -m 'Añade MiFeature'`)
4. Push para la branch (`git push origin feature/MiFeature`)
5. Abra un Pull Request

## Licencia

Este proyecto está licenciado bajo la licencia MIT - vea el archivo [LICENSE](LICENSE) para detalles.

## Seguridad

Para reportar vulnerabilidades de seguridad, consulte [SECURITY.md](SECURITY.md).
