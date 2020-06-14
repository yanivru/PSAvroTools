# PSAvroTools
Powershell Cmdlets to work with Apache.Avro files

Usage:
Install-Module AvroTools (run as admin)

Read-Avro -Path c:\weather.avro

    Result:
    station               time temp
    -------               ---- ----
    011990-99999 -619524000000    0
    011990-99999 -619506000000   22
    011990-99999 -619484400000  -11
    012650-99999 -655531200000  111
    012650-99999 -655509600000   78

Filtering: 
$r = Read-Avro -Path c:\weather.avro| where {$_.name -like "012*"}

Selecting first 10 rows:
$r = Read-Avro -Path c:\weather.avro | select -first 10

Selecting specific columns:
$r = Read-Avro -Path c:\weather.avro | select station, temp

Reading the schema:
(Read-AvroSchema -Path c:\weather.avro).ToString()
