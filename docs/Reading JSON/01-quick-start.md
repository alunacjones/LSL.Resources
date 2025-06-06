# Quick start

With no configuration the following will deserialize the `JSON` content of the resource into an instance of `JsonTestClass`

!!! note
    The resource used in this example can be found [here](https://github.com/alunacjones/LSL.Resources.DotNetFiddle/blob/master/src/LSL.Resources.DotNetFiddle/Resources/LSL.Resources.DotNetFiddle.JsonTestClass.json){ target="_blank" }
    
```csharp { data-fiddle="ygBBBD" }
var theObject = ResourceHelper
    .ReadJsonResource<JsonTestClass>();

// theObject will have a Name of "Als" and Age of 12
```