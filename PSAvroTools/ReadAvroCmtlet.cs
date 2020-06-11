using Avro.Generic;
using System;
using System.Linq;
using System.Management.Automation;

namespace PSAvroTools
{
    [Cmdlet(VerbsCommunications.Read, "Avro")]
    [OutputType(typeof(PSObject))]
    public class ReadAvroCmdlet : Cmdlet
    {
        // Declare the parameters for the cmdlet.
        [Parameter(Mandatory = true)]
        public string FileName { get; set; }

        // Override the ProcessRecord method to process
        // the supplied user name and write out a
        // greeting to the user by calling the WriteObject
        // method.
        protected override void ProcessRecord()
        {
            using (var fileReader = Avro.File.DataFileReader<GenericRecord>.OpenReader(FileName))
            {
                WriteObject(fileReader.NextEntries.Select(x => CreatePSObject(x)).ToArray());
            }
        }

        private PSObject CreatePSObject(GenericRecord x)
        {
            var result = new PSObject();
            foreach (var field in x.Schema.Fields)
            {
                var pSProperty = new PSNoteProperty(field.Name, x[field.Name]);
                result.Properties.Add(pSProperty);
            }

            return result;
        }
    }
}
