[![Build status](https://img.shields.io/appveyor/ci/alunacjones/lsl-resources.svg)](https://ci.appveyor.com/project/alunacjones/lsl-resources)
[![Coveralls branch](https://img.shields.io/coverallsCoverage/github/alunacjones/LSL.Resources)](https://coveralls.io/github/alunacjones/LSL.Resources)
[![NuGet](https://img.shields.io/nuget/v/LSL.Resources.svg)](https://www.nuget.org/packages/LSL.Resources/)

# LSL.Resources

A library to ease the pain of reading resources from an assembly. All helper methods use `string.EndsWith` to ease the pain of searching for resources.

>**NOTE**: When searching for a resource they are ordered in full name order and the first matching resource is selected.

The main helper methods cover:

* Obtaining a `Stream` for a matched resource.
* Reading string content for a matched resource.
* Reading a matched resource as `JSON`.
    * These methods use the full type name to match a resource name (with the extension `.json`)
    * [System.Text.Json][1] is used for deserialization

<!-- HIDE -->
## Further Documentation

More in-depth documentation can be found [here](https://alunacjones.github.io/LSL.Resources/)

## Pre-amble on the following examples

THe following examples assume that the assembly that contains `JsonTestClass` contains all the named
resources used. The assembly name is assumed to be `MyResources`

## Reading Text Content

The following example shows us reading a string resource file:

```csharp
var content = typeof(JsonTestClass)
    .Assembly
    .ReadStringResource("text-file.txt");

// content will contain the contents of the embedded
// resource whose name ends with "text-file.txt"
```

## Reading JSON Content

The following example shows us reading a `JSON` string resource file into an object:

```csharp
var theObject = ResourceHelper
    .ReadJsonResource<T>();

// theObject will contain the deserialized JSON content
// of the resource whose name ends with `MyResources.JsonTestClass.json`
```
<!-- END:HIDE -->
[1]: https://www.nuget.org/packages/system.text.json/