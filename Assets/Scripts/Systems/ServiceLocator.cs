using System;
using System.Collections.Generic;

namespace SampleGame
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Services = new();

        public static void AddService<T>(object service)
        {
            Services[typeof(T)] = service;
        }

        public static T GetService<T>() where T : class
        {
            return Services[typeof(T)] as T;
        }

        public static object GetService(Type argType)
        {
            return Services[argType];
        }
    }
}