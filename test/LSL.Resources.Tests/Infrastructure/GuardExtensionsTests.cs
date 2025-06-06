using System;
using FluentAssertions;
using LSL.Resources.Infrastructure;

namespace LSL.Resources.Tests.Infrastructure;

public class GuardExtensionsTests
{
    [Test]
    public void AssertIsNull_GivenANonNullValue_ItShouldReturnTheOriginalValue()
    {
        "asd".AssertNotNull("test").Should().Be("asd");
    }

    [Test]
    public void AssertIsNull_GivenANullValue_ItShouldReturnTheOriginalValue()
    {
        new Action(() => ((string)null!).AssertNotNull("test"))
            .Should()
            .ThrowExactly<ArgumentNullException>()
            .And
            .ParamName.Should().Be("test");
    }    
}