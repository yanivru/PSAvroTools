BeforeAll {
    Import-Module ./PSAvroTools/bin/Release/netstandard2.0/publish/AvroTools/AvroTools.psd1 -Force -PassThru -Verbose
}

Describe 'Read-Avro' {
    It 'Given a valid avro file, it lists all rows' {
        $allPlanets = Read-Avro ./PSAvroTools/bin/Release/netstandard2.0/publish/AvroToolsweather.avro
        $allPlanets.Count | Should -Be 5
    }
}