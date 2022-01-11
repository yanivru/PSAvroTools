Write-Output "Starting to publish nuget package to PowerShell gallery"
dotnet publish -c Release

# Copy to AvroTools folder (Publish-Module requires that the folder will be the package name)
New-Item -Path '.\bin\Release\netstandard2.0\publish\AvroTools\' -ItemType Directory -Force
copy .\bin\Release\netstandard2.0\publish\*.* .\bin\Release\netstandard2.0\publish\AvroTools\

Publish-Module -Path .\bin\Release\netstandard2.0\publish\AvroTools -NuGetApiKey ${env:NUGETAPIKEY}
