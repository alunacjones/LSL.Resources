# Custom resource name

The default behaviour of choosing a resource name based on the full type may not be desirable in all circumstances. The following example shows us fetching a JSON resource whose name ends with `other.json` instead.

```csharp { data-fiddle="vMaVe6" }
var theObject = ResourceHelper
    .ReadJsonResource<JsonTestClass>(c => c
        .MatchingResourceEndsWith("other.json")
    );

// theObject will have a Name of "Als2" and Age of 13
```