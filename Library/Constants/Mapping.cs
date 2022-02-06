using System.Collections.Generic;

namespace SqlToCsharpConverter.Constants
{
    internal static class Mapping
    {
        private static IReadOnlyDictionary<string, bool> CsharpNullableTypeDictionary { get; } = new Dictionary<string, bool>
        {
            { "string", false },
            { "int", true },
            { "System.DateTime", true },
            { "bool", true }
        };

        internal static bool IsCsharpTypeNullable(string cSharpType)
        {
            if (CsharpNullableTypeDictionary.TryGetValue(cSharpType, out bool isNullable))
            {
                return isNullable;
            }

            throw new KeyNotFoundException($"C# type [{cSharpType}] has been not found in mapping dictionary.");
        }

        private static IReadOnlyDictionary<string, string> DbTypeToCsharpTypeDictionary { get; } = new Dictionary<string, string>
        {
            { "VARCHAR", "string" },
            { "INT", "int" },
            { "TIMESTAMP", "System.DateTime" },
            { "DATETIME", "System.DateTime" },
            { "TINYINT", "bool" }
        };

        internal static string MapDbTypeToCsharpType(string dbType)
        {
            if (DbTypeToCsharpTypeDictionary.TryGetValue(dbType.ToUpper(), out string cSharpType))
            {
                return cSharpType;
            }

            throw new KeyNotFoundException($"Database type [{dbType.ToUpper()}] has been not found in mapping dictionary.");
        }

        internal static string MapDbTypeToCsharpType(string dbType, bool makeNullable)
        {
            string cSharpType = MapDbTypeToCsharpType(dbType);
            if (makeNullable && IsCsharpTypeNullable(cSharpType))
                cSharpType += '?';
            return cSharpType;
        }   
    }
}
