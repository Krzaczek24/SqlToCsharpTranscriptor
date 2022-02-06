using System.Collections.Generic;

namespace SqlToCsharpConverter.ClassDefinitions
{
    public interface IReadOnlyClassDefinition
    {
        public string Namespace { get; }
        public string Name { get; }
        public string BaseClassName { get; }
        public IReadOnlyCollection<IReadOnlyFieldDefinition> Fields { get; }
    }
}
