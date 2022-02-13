namespace SqlToCsharpTranscriptor.ClassDefinitions
{
    internal class FieldDefinition : IReadOnlyFieldDefinition
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsReference { get; set; }
        public bool IsCollection { get; set; }
        public string FullType
        {
            get
            {
                if (IsCollection)
                    return $"IEnumerable<{TypeWithPrefixAndSuffix}>";
                else if (IsReference)
                    return TypeWithPrefixAndSuffix;
                else
                    return Type;
            }
        }
        private string TypeWithPrefixAndSuffix => ClassesCommonProperties.ClassNamePrefix + Type + ClassesCommonProperties.ClassNameSuffix;

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            FieldDefinition field = obj as FieldDefinition;
            if (field == null)
                return false;

            return Name == field.Name
                && Type == field.Type;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode()
                 ^ Type.GetHashCode();
        }

        public override string ToString()
        {
            return $"public virtual {FullType} {Name} {{ get; set; }}";
        }
    }
}
