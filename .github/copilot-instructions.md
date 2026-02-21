# PSAvroTools - Copilot Instructions

## Project Overview
PSAvroTools is a PowerShell module (C#-based cmdlets) for reading and writing Apache Avro files. It provides three main cmdlets:
- `Read-Avro` - Reads Avro files and outputs PSObjects
- `Read-AvroSchema` - Reads and returns the Avro schema
- `Write-Avro` - Writes PSObjects to Avro format

## Technology Stack
- **Language**: C# (.NET Standard 2.0)
- **Type**: PowerShell Binary Module
- **Target**: PowerShell 5.1+ and PowerShell 7+
- **Dependencies**: Apache.Avro NuGet package

## Project Structure
```
PSAvroTools/
├── PSAvroTools/           # Main module source
│   ├── *.cs               # Cmdlet implementations
│   ├── AvroTools.psd1     # Module manifest
│   ├── PSAvroTools.csproj # Project file
│   └── IntegrationTests/  # Pester tests
├── azure-pipelines.yml    # CI/CD pipeline
└── PSAvroTools.sln        # Solution file
```

## Coding Standards

### C# Cmdlets
- Inherit from `PSCmdlet` or `Cmdlet`
- Use `[Cmdlet(VerbsCommon.Get, "Something")]` attribute
- Use `[Parameter()]` attributes with proper validation
- Follow PowerShell approved verbs
- Handle pipeline input with `ValueFromPipeline`

### PowerShell Testing
- Use Pester 5.x syntax
- Name test files as `*.Tests.ps1`
- Use `Describe`, `Context`, `It` blocks
- Place tests in `IntegrationTests/` folder

## Common Patterns

### Cmdlet Template
```csharp
[Cmdlet(VerbsData.Import, "Something")]
[OutputType(typeof(PSObject))]
public class ImportSomethingCmdlet : PSCmdlet
{
    [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
    [ValidateNotNullOrEmpty]
    public string Path { get; set; }

    protected override void ProcessRecord()
    {
        // Implementation
        WriteObject(result);
    }
}
```

### Pester Test Template
```powershell
Describe 'Read-Avro' {
    Context 'When reading a valid Avro file' {
        It 'Should return objects' {
            $result = Read-Avro -Path $testFile
            $result | Should -Not -BeNullOrEmpty
        }
    }
}
```

## Build & Publish
- Build: `dotnet build`
- Publish: Run `publish.ps1`
- Module published to PowerShell Gallery as `AvroTools`
