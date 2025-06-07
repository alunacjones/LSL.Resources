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
}
