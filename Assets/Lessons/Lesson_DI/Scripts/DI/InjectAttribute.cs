using System;

namespace Lessons.Lesson_DI
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field)]
    public class InjectAttribute : Attribute
    {
        
    }
}