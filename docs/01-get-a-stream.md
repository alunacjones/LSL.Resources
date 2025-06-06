# Getting a stream

If you wish to just use the stream of the resource you are interested in (rather than the extra helper methods) then this method will be for you.

!!! note
    This can be useful for reading binary data or interfacing with libraries that
    prefer to use streams.

    This example could have just used the [ReadStringResource](./02-read-text.md) method instead.

```csharp { data-fiddle="jQVA6w" }
using var stream = typeof(JsonTestClass)
    .Assembly
    .GetResourceStream("text-file.txt");

using var reader = new StreamReader(stream);

var content = reader.ReadToEnd();

// content will be "Text file"
```

