# Quick start

With no configuration the following will deserialize the `JSON` content of the resource into an instance of `JsonTestClass`

```csharp { data-fiddle="ygBBBD" }
var theObject = ResourceHelper
    .ReadJsonResource<JsonTestClass>();

// theObject will have a Name of "Als" and Age of 12
```