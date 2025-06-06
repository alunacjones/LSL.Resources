using System;
using System.Reflection;
using LSL.Resources.Infrastructure;

namespace LSL.Resources;

/// <summary>
/// BaseSettings
/// </summary>
public abstract class BaseSettings<TSelf>
    where TSelf : BaseSettings<TSelf>
{
    internal TSelf Self { get; set; }

    internal Assembly Assembly { get; private set; }

    /// <summary>
    /// Set the assembly to search for a resource
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public TSelf FromAssembly(Assembly assembly)
    {
        Assembly = assembly.AssertNotNull(nameof(assembly));
        return Self;
    }

    /// <summary>
    /// Sets the assembly to search for a resource
    /// to the assembly that contains <paramref name="type"/>
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public TSelf FromAssemblyOfType(Type type) => FromAssembly(type.AssertNotNull(nameof(type)).Assembly);

    /// <summary>
    /// Sets the assembly to search for a resource
    /// to the assembly that contains <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public TSelf FromAssemblyOfType<T>() => FromAssemblyOfType(typeof(T));
}
