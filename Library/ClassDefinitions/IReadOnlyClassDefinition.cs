using System.Collections.Generic;

namespace SqlToCsharpTranscriptor.ClassDefinitions
{
    public interface IReadOnlyClassDefinition
    {
        public string Namespace { get; }
        public string Name { get; }
        public string BaseClassName { get; }
        public string BaseClassNamespace { get; }
        public IReadOnlyCollection<IReadOnlyFieldDefinition> Fields { get; }
    }
}
