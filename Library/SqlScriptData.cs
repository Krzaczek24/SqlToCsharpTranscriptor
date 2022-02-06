using SqlToCsharpConverter.ClassDefinitions;
using SqlToCsharpConverter.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SqlToCsharpConverter
{
    public class SqlScriptData
    {
        private IReadOnlyCollection<string> sqlScriptFileLines;

        private SqlScriptData() { }
        internal SqlScriptData(string[] sqlScriptFileLines)
        {
            this.sqlScriptFileLines = sqlScriptFileLines;
        }

        public CsharpClassesData ConvertToCsharpClasses()
        {
            var classes = new List<ClassDefinition>();
            var enumerator = sqlScriptFileLines.GetEnumerator();

            while (enumerator.MoveNext())
            {
                HandleMainLines(classes, enumerator);
            }

            return new CsharpClassesData(classes);
        }

        private void HandleMainLines(List<ClassDefinition> classes, IEnumerator<string> enumerator)
        {
            while (!enumerator.Current.StartsWith("CREATE TABLE", true, null))
            {
                if (!enumerator.MoveNext())
                {
                    return;
                }
            }

            var match = Regex.Match(enumerator.Current, @"^CREATE TABLE (IF NOT EXISTS )?(`\w+`\.)?`(\w+(\.\w+)*)`");
            if (match.Success)
            {
                var @class = new ClassDefinition()
                {
                    Name = ConvertNameToPascalCase(match.Groups[3].Value)
                };

                enumerator.MoveNext();
                HandleTableLines(@class, enumerator);
                classes.Add(@class);
            }
        }

        private void HandleTableLines(ClassDefinition @class, IEnumerator<string> enumerator)
        {
            Match match;
            while ((match = Regex.Match(enumerator.Current, @"^\s*`(\w+(\.\w+)*)`\s(\w+)(\(\d+\))?\s(NOT NULL)?")).Success)
            {
                @class.FieldsList.Add(new FieldDefinition()
                {
                    Name = ConvertNameToPascalCase(match.Groups[1].Value),
                    Type = Mapping.MapDbTypeToCsharpType(match.Groups[3].Value, !match.Groups[5].Success)
                });

                if (!enumerator.MoveNext())
                {
                    break;
                }
            }

            while (!enumerator.Current.TrimEnd().EndsWith(';'))
            {
                if (enumerator.Current.TrimStart().StartsWith("FOREIGN KEY"))
                {
                    string foreignKeyField = Regex.Match(enumerator.Current, @"^\s*FOREIGN KEY \(`(\w+(\.\w+)*)`\)").Groups[1].Value;
                    enumerator.MoveNext();
                    string foreignKeyType = Regex.Match(enumerator.Current, @"^\s*REFERENCES (`\w+`\.)?`(\w+(\.\w+)*)`").Groups[2].Value;

                    foreignKeyField = ConvertNameToPascalCase(foreignKeyField);
                    foreignKeyType = ConvertNameToPascalCase(foreignKeyType);

                    var oldFieldDefinition = @class.FieldsList.Single(f => f.Name == foreignKeyField);
                    foreignKeyField = RemoveIdKeywordFromName(foreignKeyField);
                    @class.FieldsList.Remove(oldFieldDefinition);
                    @class.FieldsList.Add(new FieldDefinition()
                    { 
                        Name = foreignKeyField,
                        Type = foreignKeyType
                    });
                }
                
                if (!enumerator.MoveNext())
                {
                    break;
                }
            }
        }

        private string ConvertNameToPascalCase(string name)
        {
            var nameParts = name.Replace('.', '_').Split('_');
            nameParts = nameParts.Select(x => ConvertSingleWordToPascalCase(x)).ToArray();

            return string.Join("", nameParts);
        }

        private string ConvertSingleWordToPascalCase(string word)
        {
            return char.ToUpper(word[0]) + word.Substring(1).ToLower();
        }

        private string RemoveIdKeywordFromName(string name)
        {
            const string ID = "id";

            if (name.EndsWith(ID, StringComparison.OrdinalIgnoreCase))
            {
                return name.Substring(0, name.Length - ID.Length);
            }

            return name;
        }
    }
}
