# Especificaciones Técnicas y Guía del Agente - MercosulPlateValidator

Este documento proporciona una visión técnica detallada del proyecto `MercosulPlateValidator` para ayudar a agentes y desarrolladores en el mantenimiento y evolución del código.

## 1. Visión General del Proyecto

`MercosulPlateValidator` es una biblioteca .NET diseñada para validar formatos de placas de vehículos de los países miembros del Mercosur (Brasil, Argentina, Paraguay y Uruguay) e identificar el país de origen basándose en el patrón de la placa.

## 2. Arquitectura

El proyecto utiliza una arquitectura basada en el patrón **Strategy**, permitiendo que diferentes algoritmos de validación se apliquen de forma modular.

### 2.1. Estructura de Clases Principal

- **`MercosulPlate`**: Punto de entrada estático (Facade) para las funcionalidades de validación.
- **`CountryIdentifier`**: Responsable de orquestar la validación entre los diferentes países.
- **`BasePlateValidator`**: Clase base abstracta que define el contrato para los validadores de país y proporciona métodos utilitarios de Regex.
- **Validadores Concretos** (`BrazilPlateValidator`, `ArgentinaPlateValidator`, etc.): Implementan las reglas específicas de Regex para cada país.
- **`PlateValidationResult`**: DTO que contiene el resultado de la validación.

### 2.2. Flujo de Validación

1. La entrada es recibida por `MercosulPlate.ValidatePlate(string)`.
2. `CountryIdentifier` recorre una lista de validadores registrados.
3. El primer validador que devuelva `IsValid = true` define el resultado principal, pero se identifican todos los posibles países.

## 3. Patrones de Codificación y Requisitos

- **Target Framework**: .NET Standard 2.0 (compatibilidad máxima).
- **Pruebas**: Utiliza XUnit para pruebas unitarias.
- **Convenciones**: Sigue las convenciones estándar de C# (PascalCase para métodos y clases).

## 4. Mejoras Implementadas

### 4.1. Uso de Enums
`Country` y `PlateType` se representan como enums en `PlateValidationResult`.

### 4.2. Rendimiento de Expresiones Regulares
Los Regex se definen como campos `static readonly Regex` con `RegexOptions.Compiled` para mejorar el rendimiento.

### 4.3. Normalización de Entrada
Un método de sanitización elimina caracteres no alfanuméricos antes de la validación.

### 4.4. Tratamiento de Ambigüedades
`PlateValidationResult` devuelve una lista de posibles países (`PossibleCountries`).

### 4.5. Internacionalización (i18n)
Implementado soporte para recursos (`.resx`) con mensajes en Portugués (predeterminado), Español e Inglés.

## 5. Cómo Ejecutar Pruebas

Para garantizar la integridad del proyecto:
```bash
dotnet test
```
Las pruebas se encuentran en `tests/MercosulPlateValidator.Tests/` y cubren escenarios de placas válidas e inválidas para todos los países soportados.
