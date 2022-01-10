dotnet publish -c Release
rem copy .\bin\Release\netstandard2.0\publish\*.* $HOME\Documents\WindowsPowerShell\Modules\AvroTools\
Publish-Module -Name AvroTools -NuGetApiKey $key -RequiredVersion $version
