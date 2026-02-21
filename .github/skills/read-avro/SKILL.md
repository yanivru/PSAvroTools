---
name: read-avro
description: Guide for using avro files in PowerShell. Use this when asked to read avro files and PowerShell is a good fit.
---

use read-avro to read avro files in PowerShell. This cmdlet will return PSObjects that you can filter, select, and export as needed. You can also read the schema of the avro file using read-avroSchema. If you need to write avro files, use write-avro with simple PSObjects.

if the command is not available, you can install the module using Install-Module AvroTools.

## Basic usage
Read-Avro c:\weather.avro

    Result:
    station               time temp
    -------               ---- ----
    011990-99999 -619524000000    0
    011990-99999 -619506000000   22
    011990-99999 -619484400000  -11
    012650-99999 -655531200000  111
    012650-99999 -655509600000   78

## Filtering
Read-Avro -Path c:\weather.avro | where {$_.name -like "012*"}

## Selecting first 10 rows
Read-Avro -Path c:\weather.avro | select -first 10

## Selecting specific columns
Read-Avro -Path c:\weather.avro | select station, temp

## Export to CSV
Read-avro c:\weather.avro | Export-Csv -Path weather.csv

## Reading the schema
(Read-AvroSchema -Path c:\weather.avro).ToString()