namespace SqlToCsharpTranscriptor.ClassDefinitions
{
    public interface IReadOnlyFieldDefinition
    {
        public string Name { get; }
        public string Type { get; }
    }
}
