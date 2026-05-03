# Especificações Técnicas e Guia do Agente - MercosulPlateValidator

Este documento fornece uma visão técnica detalhada do projeto `MercosulPlateValidator` para auxiliar agentes e desenvolvedores na manutenção e evolução do código.

## 1. Visão Geral do Projeto

A `MercosulPlateValidator` é uma biblioteca .NET projetada para validar formatos de placas de veículos dos países membros do Mercosul (Brasil, Argentina, Paraguai e Uruguai) e identificar o país de origem com base no padrão da placa.

## 2. Arquitetura

O projeto utiliza uma arquitetura baseada no padrão **Strategy**, permitindo que diferentes algoritmos de validação sejam aplicados de forma modular.

### 2.1. Estrutura de Classes Principal

- **`MercosulPlate`**: Ponto de entrada estático (Facade) para as funcionalidades de validação.
- **`CountryIdentifier`**: Responsável por orquestrar a validação entre os diferentes países.
- **`BasePlateValidator`**: Classe base abstrata que define o contrato para os validadores de país e fornece métodos utilitários de Regex.
- **Validadores Concretos** (`BrazilPlateValidator`, `ArgentinaPlateValidator`, etc.): Implementam as regras específicas de Regex para cada país.
- **`PlateValidationResult`**: DTO que carrega o resultado da validação.

### 2.2. Fluxo de Validação

1. A entrada é recebida pelo `MercosulPlate.ValidatePlate(string)`.
2. O `CountryIdentifier` percorre uma lista de validadores registrados.
3. O primeiro validador que retornar `IsValid = true` define o resultado.

## 3. Padrões de Codificação e Requisitos

- **Target Framework**: .NET Standard 2.0 (compatibilidade máxima).
- **Testes**: Utiliza XUnit para testes unitários.
- **Convenções**: Segue as convenções padrão de C# (PascalCase para métodos e classes).

## 4. Melhorias Sugeridas (Roadmap Técnico)

Durante a avaliação do projeto, foram identificadas as seguintes oportunidades de melhoria:

### 4.1. Uso de Enums
Atualmente, `Country` e `PlateType` são representados como strings em `PlateValidationResult`.
- **Sugestão**: Substituir por `enum Country` e `enum PlateType` para evitar erros de digitação e facilitar o uso em lógicas condicionais.

### 4.2. Performance de Expressões Regulares
Os Regex são instanciados ou chamados via métodos estáticos simples.
- **Sugestão**: Utilizar campos `static readonly Regex` com `RegexOptions.Compiled` (ou `GeneratedRegex` em .NET 7+) para melhorar a performance em cenários de validação em massa.

### 4.3. Normalização de Entrada
A biblioteca lida com espaços e hífens nos Regex, mas isso pode tornar os padrões complexos.
- **Sugestão**: Criar um método de sanitização que remova caracteres não alfanuméricos antes da validação, simplificando os padrões.

### 4.4. Tratamento de Ambiguidades
Alguns padrões são idênticos entre países (ex: `AAA1234` é válido no formato antigo do Brasil, novo do Paraguai e antigo do Uruguai).
- **Sugestão**: O `PlateValidationResult` poderia retornar uma lista de possíveis países (`PossibleCountries`) em vez de apenas o primeiro encontrado, ou permitir que o usuário passe um "hint" de país esperado.

### 4.5. Internacionalização (i18n)
As mensagens de erro estão fixas em português.
- **Sugestão**: Implementar suporte a recursos (`.resx`) para permitir mensagens em Espanhol e Inglês.

## 5. Como Executar Testes

Para garantir a integridade do projeto:
```bash
dotnet test
```
Os testes estão localizados em `tests/MercosulPlateValidator.Tests/` e cobrem cenários de placas válidas e inválidas para todos os países suportados.
