using System;
using LSL.Resources.Infrastructure;

namespace LSL.Resources;

/// <summary>
/// Extensions for <see cref="IJsonResourceReader"/>
/// </summary>
public static class JsonResourceReaderExtensions
{
    /// <summary>
    /// Read a JSON resource into an instance of <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonResourceReader"></param>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public static T ReadJsonResource<T>(this IJsonResourceReader jsonResourceReader, Action<JsonResourceReaderSettings> configurator = null) =>
        (T)jsonResourceReader.AssertNotNull(nameof(jsonResourceReader))
            .ReadJsonResource(typeof(T), configurator);

}