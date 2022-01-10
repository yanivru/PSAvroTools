param ($nugetApiKey, $version)
dotnet publish -c Release
#copy .\bin\Release\netstandard2.0\publish\*.* $HOME\Documents\WindowsPowerShell\Modules\AvroTools\
Publish-Module -Name AvroTools -NuGetApiKey $nugetApiKey -RequiredVersion $version
