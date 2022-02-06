using SqlToCsharpTranscriptor.ClassDefinitions;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SqlToCsharpTranscriptor
{
    public class CsharpClassesData
    {
        private ICollection<ClassDefinition> classes;
        private BaseClassDefinition baseClass;
        public bool IsBaseClassSet => baseClass != null;

        private CsharpClassesData() { }
        internal CsharpClassesData(ICollection<ClassDefinition> classes)
        {
            this.classes = classes;
        }

        public IReadOnlyCollection<IReadOnlyClassDefinition> GetClassesDefinitions()
        {
            return classes.ToList().AsReadOnly();
        }

        public CsharpClassesData SetDefaultNamespace() { return SetNamespace(null); }
        public CsharpClassesData SetNamespace(string @namespace)
        {
            ClassesCommonProperties.Namespace = @namespace;
            return this;
        }

        public CsharpClassesData ClearNamePrefix() { return SetNamePrefix(null); }
        public CsharpClassesData SetNamePrefix(string prefix)
        {
            ClassesCommonProperties.ClassNamePrefix = prefix;
            return this;
        }

        public CsharpClassesData ClearNameSuffix() { return SetNameSuffix(null); }
        public CsharpClassesData SetNameSuffix(string suffix)
        {
            ClassesCommonProperties.ClassNameSuffix = suffix;
            return this;
        }

        public CsharpClassesData RemoveBaseClass()
        {
            if (IsBaseClassSet)
            {
                foreach (var @class in classes)
                {
                    @class.FieldsList = baseClass.FieldsList.Concat(@class.FieldsList).ToList();
                }

                baseClass = null;
            }

            return this;
        }

        public CsharpClassesData SetBaseClass(string baseClassName)
        {
            if (string.IsNullOrWhiteSpace(baseClassName))
            {
                return RemoveBaseClass();
            }

            if (!IsBaseClassSet)
            {
                var commonFields = ClassDefinition.GetCommonFields(classes);

                foreach (var @class in classes)
                {
                    @class.FieldsList = @class.FieldsList.Except(commonFields).ToList();
                }

                baseClass = new BaseClassDefinition()
                {
                    FieldsList = commonFields.ToList()
                };
            }

            ClassesCommonProperties.BaseClassName = baseClassName;

            return this;
        }

        public void SaveClassesToFiles(string outputDirectoryPath)
        {
            var classesDefinitions = GetClassesDefinitions().ToList();

            if (IsBaseClassSet)
            {
                classesDefinitions = classesDefinitions.Prepend(baseClass).ToList();
            }

            SaveClassesToFiles(classesDefinitions, outputDirectoryPath);
        }

        public static void SaveClassesToFiles(ICollection<IReadOnlyClassDefinition> classes, string outputDirectoryPath)
        {
            new DirectoryInfo(outputDirectoryPath).Create();

            foreach (var @class in classes)
            {
                File.WriteAllText(outputDirectoryPath + @class.Name + ".cs", @class.ToString());
            }
        }
    }
}
