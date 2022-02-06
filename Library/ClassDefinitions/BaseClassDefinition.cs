namespace SqlToCsharpConverter.ClassDefinitions
{
    internal class BaseClassDefinition : ClassDefinition
    {
        public override string Name { get => ClassesCommonProperties.BaseClassName; }
        public override string BaseClassName { get => string.Empty; }
        internal override string ClassNamePrefix { get => string.Empty; }
        internal override string ClassNameSuffix { get => string.Empty; }
    }
}
