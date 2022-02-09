namespace SqlToCsharpTranscriptor.ClassDefinitions
{
    internal class BaseClassDefinition : ClassDefinition
    {
        public override string Name { get => ClassesCommonProperties.BaseClassName; }
        public override string Namespace { get => ClassesCommonProperties.BaseClassNamespace; }
        public override string BaseClassName { get => string.Empty; }
        internal override string ClassNamePrefix { get => string.Empty; }
        internal override string ClassNameSuffix { get => string.Empty; }

        protected override string GetClassAccessModifier()
        {
            return "public abstract class";
        }
    }
}
