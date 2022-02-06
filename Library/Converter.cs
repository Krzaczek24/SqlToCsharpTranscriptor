using System.IO;

namespace SqlToCsharpConverter
{
    public static class Converter
    {
        public static SqlScriptData LoadSqlScriptData(string sqlScriptFilePath)
        {
            string[] sqlScriptFileLines = File.ReadAllLines(sqlScriptFilePath);
            return new SqlScriptData(sqlScriptFileLines);
        }
    }
}
