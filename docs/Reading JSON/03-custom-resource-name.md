# Custom resource name

The default behaviour of choosing a resource name based on the full type may not be desirable in all circumstances. The following example shows us fetching a JSON resource whose name ends with `other.json` instead.

!!! note
    The resource used in this example can be found [here](https://github.com/alunacjones/LSL.Resources.DotNetFiddle/blob/master/src/LSL.Resources.DotNetFiddle/Resources/other.json){ target="_blank" }

```csharp { data-fiddle="Xo9LOp" }
var theObject = ResourceHelper
    .ReadJsonResource<JsonTestClass>(c => c
        .MatchingResourceEndsWith("other.json")
    );

// theObject will have a Name of "Als2" and Age of 13
```