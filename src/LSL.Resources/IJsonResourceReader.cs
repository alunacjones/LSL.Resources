using System;

namespace LSL.Resources;

/// <summary>
/// IJsonResourceReader
/// </summary>
public interface IJsonResourceReader
{
    /// <summary>
    /// Read a JSON resource into an instance of <paramref name="type"/>
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    object ReadJsonResource(Type type);
}
