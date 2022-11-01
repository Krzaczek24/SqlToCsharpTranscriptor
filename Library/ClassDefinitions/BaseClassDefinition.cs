namespace SqlToCsharpTranscriptor.ClassDefinitions
{
    internal class BaseClassDefinition : ClassDefinition
    {
        public override string Name => ClassesCommonProperties.BaseClassName;
        public override string Namespace => ClassesCommonProperties.BaseClassNamespace;
        public override string BaseClassName => string.Empty;
        internal override string ClassNamePrefix => string.Empty;
        internal override string ClassNameSuffix => string.Empty;
        internal override string ClassKeyword => $"abstract {base.ClassKeyword}";
    }
}
