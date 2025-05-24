Write-Output "Starting to publish nuget package to PowerShell gallery"
Write-Host $env:NUGETAPIKEY.Length
dotnet publish -c Release

# Copy to AvroTools folder (Publish-Module requires that the folder will be the package name)
New-Item -Path '.\PSAvroTools\bin\Release\netstandard2.0\publish\AvroTools\' -ItemType Directory -Force
copy .\PSAvroTools\bin\Release\netstandard2.0\publish\*.* .\PSAvroTools\bin\Release\netstandard2.0\publish\AvroTools\

Publish-Module -Path .\PSAvroTools\bin\Release\netstandard2.0\publish\AvroTools -NuGetApiKey aaa
