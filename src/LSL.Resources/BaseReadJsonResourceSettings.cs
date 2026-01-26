using System;
using System.IO;
using LSL.Resources.Infrastructure;

namespace LSL.Resources;

/// <summary>
/// BaseReadJsonResourceSettings
/// </summary>
/// <typeparam name="TSelf"></typeparam>
public abstract class BaseReadJsonResourceSettings<TSelf> : BaseSettings<TSelf>, IBaseReadJsonResourceSettings
    where TSelf : BaseSettings<TSelf>
{
    internal string ResourceNamePrefix { get; private set; }
    internal string ResourceNameEndsWith { get; private set; }
    internal Func<Stream, Type, object> CustomDeserialiser  { get; private set; }

    string IBaseReadJsonResourceSettings.ResourceNameEndsWith => ResourceNameEndsWith;

    string IBaseReadJsonResourceSettings.ResourceNamePrefix => ResourceNamePrefix;

    /// <summary>
    /// Allows for overriding the default
    /// behaviour of using the type name to
    /// match the resource name
    /// </summary>
    /// <param name="resourceNameEndsWith"></param>
    /// <returns></returns>
    public TSelf MatchingResourceEndsWith(string resourceNameEndsWith)
    {
        ResourceNameEndsWith = resourceNameEndsWith.AssertNotNull(nameof(resourceNameEndsWith));
        return Self;
    }

    /// <summary>
    /// Allows for prefixing of the partial resource
    /// name to allow for sub folders but still
    /// allow the full type name to be utilised in the name
    /// </summary>
    /// <param name="resourceNamePrefix"></param>
    /// <returns></returns>
    public TSelf WithResourceNamePrefixOf(string resourceNamePrefix)
    {
        ResourceNamePrefix = resourceNamePrefix.AssertNotNull(nameof(resourceNamePrefix));
        return Self;
    }

    /// <summary>
    /// Use a custom deserialiser
    /// </summary>
    /// <param name="customDeserialiser"></param>
    /// <returns></returns>
    public TSelf WithCustomDeserialiser(Func<Stream, Type, object> customDeserialiser)
    {
        CustomDeserialiser = customDeserialiser.AssertNotNull(nameof(customDeserialiser));
        return Self;
    }
}
