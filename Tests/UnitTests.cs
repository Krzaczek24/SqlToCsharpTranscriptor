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
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            new DirectoryInfo(TEST_OUTPUT_FILES_PATH).Create();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            var outputDirectory = new DirectoryInfo(TEST_OUTPUT_FILES_PATH);
            var outputDirectories = outputDirectory.EnumerateDirectories();
            var outputFiles = outputDirectories.SelectMany(x => x.EnumerateFiles());
            foreach (var outputFile in outputFiles)
                outputFile.Delete();
        }

        [Test]
        [Order(1)]
        public void Test_FileNames_WithoutManipulations()
        {
            string @namespace = "DatabaseModels";

            Converter
                .LoadSqlScriptData(TEST_INPUT_FILE_PATH)
                .ConvertToCsharpClasses()
                .SaveClassesToFiles(GetOutputDirectoryPath(@namespace));

            VerifyIfFilesAreTheSame(@namespace);
        }

        [Test]
        [Order(2)]
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
        [Order(3)]
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
        [Order(4)]
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

        private static ICollection<FileInfo> GetOutputFiles(string @namespace)
        {
            var directory = new DirectoryInfo(TEST_OUTPUT_FILES_PATH + @namespace);
            var files = directory.EnumerateFiles();
            return files.ToList();
        }

        private static void VerifyIfFilesAreTheSame(string @namespace)
        {
            var outputFileNames = GetOutputFiles(@namespace).Select(x => x.Name).ToList();

            string noFilesMessage = $"No file was found";
            Assert.IsNotNull(outputFileNames, noFilesMessage);
            CollectionAssert.IsNotEmpty(outputFileNames, noFilesMessage);

            foreach (var outputFileName in outputFileNames)
            {
                string outputFilePath = $@"{FILES_PATH}{@namespace}\{outputFileName}";
                string testFilePath = $@"{TEST_OUTPUT_FILES_PATH}{@namespace}\{outputFileName}";

                string[] expectedText = File.ReadAllLines(outputFilePath);
                string[] actualText = File.ReadAllLines(testFilePath);

                string emptyFileMessage = $@"File [{outputFileName}] is empty";
                Assert.IsNotNull(actualText, emptyFileMessage);
                CollectionAssert.IsNotEmpty(actualText, emptyFileMessage);
                Assert.AreEqual(expectedText.Length, actualText.Length, $"Length of [{outputFileName}] file differs from length of test file");

                for (int index = 0; index < actualText.Length; index++)
                {
                    Assert.AreEqual(expectedText[index], actualText[index], $"Content of [{outputFileName}] file differs from test file content in [{index + 1}.] line");
                }
            }
        }

        private static string GetOutputDirectoryPath(string @namespace)
        {
            return TEST_OUTPUT_FILES_PATH + @namespace + '\\';
        }
    }
}