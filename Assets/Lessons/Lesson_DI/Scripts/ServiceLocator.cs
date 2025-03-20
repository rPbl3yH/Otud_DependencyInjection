using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lessons.Lesson_DI
{
    public class ServiceLocator : MonoBehaviour
    {
        public static ServiceLocator Instance;

        private Dictionary<Type, object> _services = new(); 
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                //DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AddService<T>(T service)
        {
            // var type1 = service.GetType();
            var type = typeof(T);
            _services[type] = service;
        }

        public T GetService<T>() where T : class
        {
            var type = typeof(T);
            return _services[type] as T;
        }

        public object GetService(Type parameterType)
        {
            return _services[parameterType];
        }
    }
}