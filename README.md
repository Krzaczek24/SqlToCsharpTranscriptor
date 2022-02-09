# SqlToCsharpTranscriptor

## This library allows easily convert MySQL Workbench output SQL scripts into C# database models.

### Example usage:
```
Converter
  .LoadSqlScriptData("c:\directory\script.sql")
  .ConvertToCsharpClasses()
  .SetNamespace("MyNamespace")
  .SetBaseClass("MyCommonFieldsModel")
  .SetBaseClassNamespace("MyNamespace.Base")
  .SetNamePrefix("My")
  .SetNameSuffix("Model");
  .SaveClassesToFiles("c:\directory\output");
```
