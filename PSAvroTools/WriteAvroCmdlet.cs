using Avro;
using Avro.Generic;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Linq;
using Avro.File;

namespace PSAvroTools
{
    [Cmdlet(VerbsCommunications.Write, "Avro")]
    public class WriteAvroCmdlet : PSCmdlet
    {
        readonly Dictionary<string, Schema.Type> _dotNetTypesToAvroPrimitivesTypesMapping = CreateTypeMapping();
        private bool _firstRow = true;
        private RecordSchema _schema;
        private IFileWriter<GenericRecord> _fileWriter;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public PSObject Input { get; set; }

        /// <summary>
        /// Mandatory file name to write to.
        /// </summary>
        [Parameter(Position = 0)]
        [ValidateNotNullOrEmpty]
        public string Path { get; set; }

        [Parameter(Position = 1)]
        public string SchemaName { get; set; }

        protected override void BeginProcessing()
        {
            WriteVerbose("Started");
        }

        protected override void ProcessRecord()
        {
            if (_firstRow)
            {
                _schema = CreateSchema(Input);
                WriteVerbose(_schema.ToString());

                _fileWriter = CreateWriter();

                _firstRow = false;
            }

            GenericRecord genericRecord = ConvertToGenericRecord(Input);

            _fileWriter.Append(genericRecord);
        }

        protected override void EndProcessing()
        {
            WriteVerbose("Ended");
            _fileWriter?.Dispose();
        }

        private GenericRecord ConvertToGenericRecord(PSObject input)
        {
            GenericRecord genericRecord = new GenericRecord(_schema);
            int i = 0;
            foreach (var property in input.Properties.Where(x => x.IsGettable))
            {
                var newValue = property.Value is PSObject pso ? ConvertToGenericRecord(pso) : property.Value;
                genericRecord.Add(i, newValue);
                i++;
            }

            return genericRecord;
        }

        private IFileWriter<GenericRecord> CreateWriter()
        {
            var resolvedPath = SessionState.Path.GetUnresolvedProviderPathFromPSPath(Path);

            return DataFileWriter<GenericRecord>.OpenWriter(new GenericDatumWriter<GenericRecord>(_schema), resolvedPath);
        }

        private int _schemaId = 0;

        private RecordSchema CreateSchema(PSObject input)
        {
            var properties = input.Properties.Where(x => x.IsGettable).Select((x, i) => CreateAvroPropertySchema(x, i));
            var schema = RecordSchema.Create(SchemaName ?? "Schema" + _schemaId++, properties.ToList());
            return schema;
        }

        private Field CreateAvroPropertySchema(PSPropertyInfo property, int position)
        {
            if(property.Value is PSObject pso)
            {
                return new Field(CreateSchema(pso), property.Name, position);
            }

            if (!_dotNetTypesToAvroPrimitivesTypesMapping.ContainsKey(property.TypeNameOfValue))
            {
                throw new Exception($"Type {property.TypeNameOfValue} of property {property.Name} is not supported {property.Value.GetType()}. Fields can only contain primitive types.");
            }

            return new Field(PrimitiveSchema.Create(_dotNetTypesToAvroPrimitivesTypesMapping[property.TypeNameOfValue]), property.Name, position);
        }

        private static Dictionary<string, Schema.Type> CreateTypeMapping()
        {
            (Type, Schema.Type)[] DotNetTypesToAvroPrimitivesTypesMapping = new (Type, Schema.Type)[]
        {
            (typeof(string), Schema.Type.String),
            (typeof(int), Schema.Type.Int),
            (typeof(float), Schema.Type.Float),
            (typeof(double), Schema.Type.Double),
            (typeof(bool), Schema.Type.Boolean),
            (typeof(long), Schema.Type.Long),
        };

            return DotNetTypesToAvroPrimitivesTypesMapping.ToDictionary(x => x.Item1.ToString(), x => x.Item2);
        }
    }
}
