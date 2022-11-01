using System.Reflection;

namespace SqlToCsharpTranscriptor.ClassDefinitions
{
    internal static class ClassesCommonProperties
    {
        private const string DEFAULT_NAMESPACE = "DatabaseModels";
        private const string DEFAULT_ACCESS_MODIFIER = "public";

        private static string @namespace = DEFAULT_NAMESPACE;
        internal static string Namespace
        {
            get => @namespace;
            set => @namespace = GetValueOrDefault(value, DEFAULT_NAMESPACE);
        }

        private static string baseClassName = string.Empty;
        internal static string BaseClassName
        {
            get => baseClassName;
            set => baseClassName = GetValueOrDefault(value);
        }

        private static string baseClassNamespace = string.Empty;
        internal static string BaseClassNamespace
        {
            get => GetValueOrDefault(baseClassNamespace, Namespace);
            set => baseClassNamespace = GetValueOrDefault(value);
        }

        private static string prefix = string.Empty;
        internal static string ClassNamePrefix
        {
            get => prefix;
            set => prefix = GetValueOrDefault(value);
        }

        private static string suffix = string.Empty;
        internal static string ClassNameSuffix
        {
            get => suffix;
            set => suffix = GetValueOrDefault(value);
        }

        private static string childrenPropertyName = string.Empty;
        internal static string ChildrenPropertyName
        {
            get => childrenPropertyName;
            set => childrenPropertyName = GetValueOrDefault(value);
        }

        private static string accessModifier = DEFAULT_ACCESS_MODIFIER;
        internal static string AccessModifier
        {
            get => accessModifier;
            set => accessModifier = GetValueOrDefault(value, DEFAULT_ACCESS_MODIFIER);
        }

        private static string GetValueOrDefault(string value, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }

        private static string GetValueOrDefault(string value)
        {
            return GetValueOrDefault(value, string.Empty);
        }

        public static void Reset()
        {
            var properties = typeof(ClassesCommonProperties).GetProperties(BindingFlags.NonPublic | BindingFlags.Static);
            foreach (var property in properties)
            {
                property.SetValue(null, null);
            }
        }
    }
}
