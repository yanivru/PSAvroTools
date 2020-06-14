using Avro.Generic;
using System.Management.Automation;

namespace PSAvroTools
{
    [Cmdlet(VerbsCommunications.Read, "AvroSchema")]
    [OutputType(typeof(PSObject))]
    public class ReadAvroSchemaCmdlet : PSCmdlet
    {
        // Declare the parameters for the cmdlet.
        [Parameter(Mandatory = true, Position = 0)]
        public string Path { get; set; }

        // Override the ProcessRecord method to process
        // the supplied user name and write out a
        // greeting to the user by calling the WriteObject
        // method.
        protected override void ProcessRecord()
        {
            var resolvedPath = SessionState.Path.GetUnresolvedProviderPathFromPSPath(Path);

            using (var fileReader = Avro.File.DataFileReader<GenericRecord>.OpenReader(resolvedPath))
            {
                var schema = fileReader.GetSchema();

                WriteObject(schema);
            }
        }
    }
}
