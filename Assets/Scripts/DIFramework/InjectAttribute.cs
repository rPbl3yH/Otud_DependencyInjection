using System;
using JetBrains.Annotations;

namespace SampleGame
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method)]
    public class InjectAttribute : Attribute
    {
    }
}