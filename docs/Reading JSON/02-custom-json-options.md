# Custom JSON options

Sometimes we may have a class that needs custom serialization settings e.g. the use of a `JsonStringEnumConverter`. The following example needs to use that to get a string value for an enum value to be deserialized.

!!! note
    The resource used in this example can be found [here](https://github.com/alunacjones/LSL.Resources.DotNetFiddle/blob/master/src/LSL.Resources.DotNetFiddle/Resources/LSL.Resources.DotNetFiddle.JsonTestClassWithEnum.json){ target="_blank" }

```csharp  { data-fiddle="5nKf2v" }
var theObject = ResourceHelper
    .ReadJsonResource<JsonTestClassWithEnum>(c => c
        .ConfigureJsonDeserializerOptions(
            c => c.Converters.Add(new JsonStringEnumConverter())
        )
    );

// theObject will have a Name of "OtherAls", Answer of "No" and Age of 10
```