namespace SqlToCsharpTranscriptor.ClassDefinitions
{
    internal class FieldDefinition : IReadOnlyFieldDefinition
    {
        public string Name { get; set; }
        public string Type { get; set; }  
        public bool IsReference { get; set; }
        public string FullType => IsReference ? ClassesCommonProperties.ClassNamePrefix + Type + ClassesCommonProperties.ClassNameSuffix : Type;

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
