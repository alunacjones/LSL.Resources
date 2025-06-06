using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using LSL.Resources.Infrastructure;

namespace LSL.Resources;

/// <summary>
/// Helpers to get resources by name or type name
/// </summary>
public static class ResourceHelper
{
    /// <summary>
    /// Gets a stream for the embedded resource in the source
    /// <see cref="Assembly"/>.
    /// It matches the first resource whose name
    /// ends with <paramref name="resourceNameEndsWith"/>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="resourceNameEndsWith"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static Stream GetResourceStream(this Assembly source, string resourceNameEndsWith)
    {
        var resourceName = source.AssertNotNull(nameof(source)).GetManifestResourceNames()
            .Where(name => name.EndsWith(resourceNameEndsWith))
            .FirstOrDefault()
            ?? throw new FileNotFoundException(
                    $"Could not locate a resource whose name ends with '{resourceNameEndsWith}' from assembly '{source.FullName}'.",
                    resourceNameEndsWith
                );

        return source.GetManifestResourceStream(resourceName);
    }

    /// <summary>
    /// Reads a resource as a string from the source
    /// <see cref="Assembly"/>.
    /// It matches the first resource whose name
    /// ends with <paramref name="resourceNameEndsWith"/>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="resourceNameEndsWith"></param>
    /// <returns></returns>
    public static string ReadStringResource(this Assembly source, string resourceNameEndsWith)
    {
        using var stream = source.GetResourceStream(resourceNameEndsWith);
        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }

    /// <summary>
    /// Reads a resource as JSON from the assembly
    /// that contains <typeparamref name="T"/>
    /// It matches the first resource whose name
    /// ends with the <see cref="Type.FullName"/> of <typeparamref name="T"/>
    /// </summary>
    /// <remarks>
    /// The assembly to search can be overridden
    /// via the configurator
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public static T ReadJsonResource<T>(Action<ReadJsonResourceSettings> configurator = null) =>
        (T)ReadJsonResource(typeof(T), configurator);

    /// <summary>
    /// Reads a resource as JSON from the assembly
    /// that contains <paramref name="type"/>.
    /// It matches the first resource whose name
    /// ends with the <see cref="Type.FullName"/> of <paramref name="type"/>s
    /// </summary>
    /// <remarks>
    /// The assembly to search can be overridden
    /// via the configurator
    /// </remarks>
    /// <param name="type"></param>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public static object ReadJsonResource(Type type, Action<ReadJsonResourceSettings> configurator = null)
    {
        var settings = new ReadJsonResourceSettings();
        settings.FromAssembly(type.Assembly);

        configurator?.Invoke(settings);

        var options = new JsonSerializerOptions();
        settings.OptionsConfigurator.Invoke(options);

        return JsonSerializer.Deserialize(GetResourceStream(settings.Assembly, $"{type.FullName}.json"), type, options);
    }
}