using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlToCsharpTranscriptor.ClassDefinitions
{
    internal class ClassDefinition : IReadOnlyClassDefinition
    {
        private string @namespace;
        private string baseClassNamespace;
        private string baseClassName;
        private string rawName;
        private string prefix;
        private string suffix;

        #region INTERFACE THINGS
        public virtual string Namespace
        {
            get => string.IsNullOrWhiteSpace(@namespace) ? ClassesCommonProperties.Namespace : @namespace;
            set => @namespace = value;
        }
        public virtual string Name
        {
            get => ClassNamePrefix + rawName + ClassNameSuffix;
            set => rawName = value;
        }
        public virtual string BaseClassNamespace
        {
            get => string.IsNullOrWhiteSpace(baseClassNamespace) ? ClassesCommonProperties.BaseClassNamespace : baseClassNamespace;
            set => baseClassNamespace = value;
        }
        public virtual string BaseClassName
        {
            get => string.IsNullOrWhiteSpace(baseClassName) ? ClassesCommonProperties.BaseClassName : baseClassName;
            set => baseClassName = value;
        }
        public virtual IReadOnlyCollection<IReadOnlyFieldDefinition> Fields => FieldsList.ToList().AsReadOnly();
        #endregion INTERFACE THINGS

        internal virtual string RawName => rawName;
        internal virtual string ClassNamePrefix
        {
            get => string.IsNullOrWhiteSpace(prefix) ? ClassesCommonProperties.ClassNamePrefix : prefix;
            set => prefix = value;
        }
        internal virtual string ClassNameSuffix
        {
            get => string.IsNullOrWhiteSpace(suffix) ? ClassesCommonProperties.ClassNameSuffix : suffix;
            set => suffix = value;
        }
        internal virtual ICollection<FieldDefinition> FieldsList { get; set; } = new List<FieldDefinition>();

        internal static IReadOnlyCollection<FieldDefinition> GetCommonFields(IEnumerable<ClassDefinition> classes) { return GetCommonFields(classes.ToArray()); }
        internal static IReadOnlyCollection<FieldDefinition> GetCommonFields(params ClassDefinition[] classes)
        {
            var commonFields = new List<FieldDefinition>();

            if (classes == null || classes.Count() <= 1)
                return commonFields;

            commonFields.AddRange(classes.First().FieldsList);

            foreach (var @class in classes)
            {
                if (commonFields.Count == 0)
                    break;

                commonFields = commonFields.Intersect(@class.FieldsList).ToList();
            }

            return commonFields;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Namespace != BaseClassNamespace)
            {
                sb.AppendLine($"using {BaseClassNamespace};");
                sb.AppendLine();
            }
            sb.AppendLine($"namespace {Namespace}");
            sb.AppendLine($"{{");
            sb.AppendLine($"\t{GetClassAccessModifier()} {Name}{GetBaseClassDerivation()}");
            sb.AppendLine($"\t{{");
            foreach (var field in Fields)
            {
                sb.AppendLine($"\t\t{field}");
            }
            sb.AppendLine($"\t}}");
            sb.AppendLine($"}}");

            return sb.ToString();
        }

        protected virtual string GetClassAccessModifier()
        {
            return "public class";
        }

        private string GetBaseClassDerivation()
        {
            return string.IsNullOrWhiteSpace(BaseClassName) ? string.Empty : $" : {BaseClassName}";
        }
    }
}
