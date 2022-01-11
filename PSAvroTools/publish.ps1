Write-Output "Starting to publish nuget package to PowerShell gallery"
dotnet publish -c Release
#copy .\bin\Release\netstandard2.0\publish\*.* $HOME\Documents\WindowsPowerShell\Modules\AvroTools\
Publish-Module -Path bin\Release\netstandard2.0\publish\ -NuGetApiKey ${env:NUGETAPIKEY}
