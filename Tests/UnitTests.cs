using NUnit.Framework;
using SqlToCsharpTranscriptor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SqlToCsharpConverterTests
{
    public class Tests
    {
        private const string FILES_PATH = @"..\..\..\Files\";
        private const string TEST_INPUT_FILE_PATH = FILES_PATH + "script_sql";
        private const string TEST_OUTPUT_FILES_PATH = @"output\";
        private const string BASE_CLASS_NAME = "MyBaseClassName";
        private const string PREFIX = "Prefix";
        private const string SUFFIX = "Suffix";
        private static readonly ICollection<string> classNames = new[]
        {
            "FormFieldDefinition",
            "FormFieldDefinitionValueType",
            "FormPredefiniedField",
            "FormPredefiniedFieldOption",
            "FormValueType"
        };
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            new DirectoryInfo(TEST_OUTPUT_FILES_PATH).Create();
        }

        [TearDown]
        public void TearDown()
        {
            var outputDirectory = new DirectoryInfo(TEST_OUTPUT_FILES_PATH);
            var outputDirectories = outputDirectory.EnumerateDirectories();
            var outputFiles = outputDirectories.SelectMany(x => x.EnumerateFiles());
            foreach (var outputFile in outputFiles)
                outputFile.Delete();
        }

        [Test]
        public void Test_FileNames_WithoutManipulations()
        {
            string @namespace = "WithoutManipulations";

            Converter
                .LoadSqlScriptData(TEST_INPUT_FILE_PATH)
                .ConvertToCsharpClasses()
                .SaveClassesToFiles(GetOutputDirectoryPath(@namespace));

            VerifyIfFilesAreTheSame(@namespace);
        }

        [Test]
        public void Test_FileNames_WithPrefixAndSuffix()
        {
            string @namespace = "WithPrefixAndSuffix";

            Converter
                .LoadSqlScriptData(TEST_INPUT_FILE_PATH)
                .ConvertToCsharpClasses()
                .SetNamePrefix(PREFIX)
                .SetNameSuffix(SUFFIX)
                .SetNamespace(@namespace)
                .SaveClassesToFiles(GetOutputDirectoryPath(@namespace));

            VerifyIfFilesAreTheSame(@namespace);
        }

        [Test]
        public void Test_FileNames_WithBaseClass()
        {
            string @namespace = "WithBaseClass";

            Converter
                .LoadSqlScriptData(TEST_INPUT_FILE_PATH)
                .ConvertToCsharpClasses()
                .SetNamespace(@namespace)
                .SetBaseClass(BASE_CLASS_NAME)
                .SaveClassesToFiles(GetOutputDirectoryPath(@namespace));

            VerifyIfFilesAreTheSame(@namespace);
        }

        [Test]
        public void Test_FileNames_WithPrefixSuffixAndBaseClass()
        {
            string @namespace = "WithPrefixSuffixAndBaseClass";

            Converter
                .LoadSqlScriptData(TEST_INPUT_FILE_PATH)
                .ConvertToCsharpClasses()
                .SetNamePrefix(PREFIX)
                .SetBaseClass(BASE_CLASS_NAME)
                .SetNamespace(@namespace)
                .SetNameSuffix(SUFFIX)
                .SaveClassesToFiles(GetOutputDirectoryPath(@namespace));

            VerifyIfFilesAreTheSame(@namespace);
        }

        private static ICollection<FileInfo> GetOutputFiles()
        {
            var directory = new DirectoryInfo(TEST_OUTPUT_FILES_PATH);
            var files = directory.EnumerateFiles();
            return files.ToList();
        }

        private static void VerifyIfFilesAreTheSame(string @namespace)
        {
            var outputFileNames = GetOutputFiles().Select(x => x.Name).ToList();

            foreach (var outputFileName in outputFileNames)
            {
                string expectedText = File.ReadAllText($@"{FILES_PATH}\{@namespace}\{outputFileName}");
                string actualText = File.ReadAllText($@"{TEST_OUTPUT_FILES_PATH}{@namespace}\{outputFileName}");

                Assert.AreEqual(expectedText, actualText);
            }
        }

        private static string GetOutputDirectoryPath(string @namespace)
        {
            return TEST_OUTPUT_FILES_PATH + @namespace + '\\';
        }
    }
}