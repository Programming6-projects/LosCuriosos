# Code Conventions

This document outlines the coding, naming, and other conventions for this
project. Please adhere to these guidelines when contributing.

## Coding Conventions

1. **SOLID Principles**: All code should follow SOLID principles. If not,
   justification for deviations must be provided.
2. **Design Patterns**: Apply appropriate design patterns when necessary.
3. **Library Usage**: Any use of external libraries must be approved by the
   trainer.
4. **Library Introduction**: If proposing a new library, you must give a
   presentation explaining its purpose, functionality (with examples), and why
   it should be used in the project.
5. **Code Language**: All code must be written in English.
6. **Avoid Switch**: Do not use the `switch` statement.

### Code Style

1. **String Interpolation**: Use string interpolation for concatenating short
   strings.
2. **Implicit Typing**: Use implicit typing (`var`) when the type of the
   variable is obvious.
3. **Statements and Declarations**:
   - Write one statement per line.
   - Write one declaration per line.
4. **Method and Property Spacing**: Add at least one blank line between method
   and property definitions.
5. **Expression Clarity**: Use parentheses to clarify complex expressions.
6. **Interface Naming**: Interface names should start with an uppercase `I`
   (e.g., `IUserRepository`).
7. **Enum Naming**: Use singular names for non-flag enums and plural names for
   flag enums.
8. **Naming**: Choose meaningful and descriptive names for variables, methods,
   and classes.
9. **Clarity Over Brevity**: Prioritize clarity in naming and code structure
   over brevity.
10. **PascalCase**:
    - Use `PascalCase` for class names and method names.
    - Use `PascalCase` for constant names (both fields and local constants).
11. **camelCase**:
    - Use `camelCase` for method parameters and local variables.
12. **Private Fields**: Private instance fields should start with an underscore
    (`_`) and use `camelCase`.

## Naming Conventions

1. **Classes and Methods**: Use `PascalCase` (e.g., `OrderProcessor`,
   `CalculateTotal`).
2. **Method Parameters and Local Variables**: Use `camelCase` (e.g.,
   `orderAmount`, `totalPrice`).
3. **Constants**: Use `PascalCase` for constant names (e.g., `MaxRetries`,
   `DefaultTimeout`).
4. **Private Fields**: Start with an underscore and use `camelCase` (e.g.,
   `_orderRepository`).

## File and Directory Conventions

1. **File Encoding**: Use `UTF-8` encoding for all files.
2. **New Lines**: Ensure files end with a newline.
3. **Whitespace**: Trim trailing whitespace in all files.
4. **Indentation**: Use spaces for indentation (4 spaces per indentation level).
5. **End of Line**: Use LF (`\n`) as the line ending.
6. **Max Line Length**: Limit lines to 120 characters.

## Configuration

```ini
root = true

[*]
charset = utf-8
insert_final_newline = true
trim_trailing_whitespace = true
indent_style = space
indent_size = 4
end_of_line = lf
max_line_length = 120

[**/Program.cs]
dotnet_diagnostic.CA1052.severity = none

[**/*.cs]
dotnet_sort_system_directives_first = true
csharp_using_directive_placement = inside_namespace
csharp_style_namespace_declarations = file_scoped
dotnet_diagnostic.IDE0130.severity = warning
dotnet_diagnostic.IDE0058.severity = warning
dotnet_diagnostic.IDE1006.severity = warning
dotnet_diagnostic.IDE0090.severity = warning
dotnet_diagnostic.IDE0008.severity = warning
dotnet_diagnostic.IDE0161.severity = warning
dotnet_diagnostic.IDE0065.severity = warning
dotnet_diagnostic.IDE0059.severity = warning
dotnet_diagnostic.IDE0005.severity = warning
dotnet_diagnostic.IDE0290.severity = warning
dotnet_diagnostic.IDE0052.severity = warning
dotnet_diagnostic.IDE0046.severity = none
dotnet_diagnostic.IDE0055.severity = none
dotnet_diagnostic.CS1591.severity = none
dotnet_diagnostic.CA1303.severity = none

[**/GlobalUsings.cs]
dotnet_diagnostic.IDE0005.severity = none
dotnet_diagnostic.CS8019.severity = none

[test/**/*.cs]
dotnet_diagnostic.CA1707.severity = none
```

### Project-Specific Configuration

```xml
<Project>
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <LangVersion>preview</LangVersion>
    <AnalysisMode>AllEnabledByDefault</AnalysisMode>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <CollectCoverage>true</CollectCoverage>
    <CoverletOutputFormat>cobertura</CoverletOutputFormat>
    <Threshold>80</Threshold>
    <ThresholdType>line,branch,method</ThresholdType>
    <ThresholdStat>total</ThresholdStat>
    <Exclude>[ProjectNamespace.Api]*Program,[ProjectNamespace.Api]*Startup,[ProjectNamespace.Api]*Configurations*</Exclude>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.CodeAnalysis.NetAnalyzers" PrivateAssets="all">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
    </PackageReference>
  </ItemGroup>
</Project>
```

Remember, consistency with these conventions is key. When in doubt, stick to the
conventions outlined in this document.
