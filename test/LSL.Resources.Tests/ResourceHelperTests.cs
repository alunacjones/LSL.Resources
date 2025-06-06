using System;
using System.IO;
using FluentAssertions;

namespace LSL.Resources.Tests;

public class ResourceHelperTests
{
    [Test]
    public void ReadJsonResource_GivenAValidResource_ItShouldReturnTheExpectedResult()
    {
        ResourceHelper
            .ReadJsonResource<JsonTestClass>(c => c.FromAssemblyOfType<JsonTestClass>())
            .Should()
            .BeEquivalentTo(new JsonTestClass
            {
                Name = "Als"
            });
    }

    [Test]
    public void GetResourceStream_GivenAnInvalidResourceName_ItShouldThrowFileNotFoundException()
    {
        new Action(() => GetType().Assembly.GetResourceStream("not-real"))
            .Should()
            .ThrowExactly<FileNotFoundException>()
            .WithMessage("Could not locate a resource whose name ends with 'not-real' from assembly 'LSL.Resources.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.")
            .And
            .FileName
            .Should()
            .Be("not-real");

    }

    [Test]
    public void ReadStringResource_GivenAValidResource_ItShouldReturnTheExpectedResult()
    {
        GetType()
            .Assembly
            .ReadStringResource("text-file.txt")
            .Should()
            .Be("Text file");
    }
}