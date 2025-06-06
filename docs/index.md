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


[1]: https://www.nuget.org/packages/system.text.json/