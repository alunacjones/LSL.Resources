# Reading a string resource

The following example shows us reading the text content of a resource whose name ends with `text-file.txt`:

```csharp { data-fiddle="J90MSL" }
var content = typeof(JsonTestClass)
    .Assembly
    .ReadStringResource("text-file.txt");

// content will be "Text file"
```