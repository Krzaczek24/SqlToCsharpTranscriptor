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

        public CsharpClassesData SetBaseClassDefaultNamespace() { return SetBaseClassNamespace(null); }
        public CsharpClassesData SetBaseClassNamespace(string @namespace)
        {
            ClassesCommonProperties.BaseClassNamespace = @namespace;
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
            if (baseClass != null)
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

            if (baseClass == null)
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

        public CsharpClassesData RemoveChildrenCollectionPropertyName()
        {
            if (!string.IsNullOrWhiteSpace(ClassesCommonProperties.ChildrenPropertyName))
            {
                var classesWithSelfReferenceChildren = classes.Where(c => c.FieldsList.Any(f => f.IsCollection)).ToList();

                foreach (var @class in classesWithSelfReferenceChildren)
                {
                    var children = @class.FieldsList.Single(f => f.IsCollection);

                    @class.FieldsList.Remove(children);
                }

                ClassesCommonProperties.ChildrenPropertyName = string.Empty;
            }

            return this;
        }

        public CsharpClassesData SetChildrenCollectionPropertyName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return RemoveChildrenCollectionPropertyName();
            }

            var classesWithSelfReference = classes.Where(c => c.FieldsList.Any(f => f.FullType == c.Name && f.IsReference)).ToList();

            if (string.IsNullOrWhiteSpace(ClassesCommonProperties.ChildrenPropertyName))
            {
                foreach (var @class in classesWithSelfReference)
                {
                    @class.FieldsList.Add(new FieldDefinition()
                    {
                        Name = name,
                        Type = @class.RawName,
                        IsCollection = true
                    });
                }
            }
            else if (ClassesCommonProperties.ChildrenPropertyName != name)
            {
                classesWithSelfReference.SelectMany(c => c.FieldsList).Where(f => f.IsCollection).ToList().ForEach(f => f.Name = name);
            }

            ClassesCommonProperties.ChildrenPropertyName = name;

            return this;
        }

        public void SaveClassesToFiles(string outputDirectoryPath)
        {
            var classesDefinitions = GetClassesDefinitions().ToList();

            if (baseClass != null)
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
