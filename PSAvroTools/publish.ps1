Write-Output "Starting to publish nuget package to PowerShell gallery"
Write-Host $env:NUGETAPIKEY.Length

Publish-Module -Path .\PSAvroTools\bin\Release\netstandard2.0\publish\AvroTools -NuGetApiKey $env:NUGETAPIKEY