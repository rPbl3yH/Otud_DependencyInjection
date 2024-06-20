using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Lessons.Lesson_DI
{
    public class DiContainer : MonoBehaviour
    {
        [SerializeField] private MonoInstaller[] _monoInstallers;
        
        private readonly Dictionary<Type, object> _services = new();
        
        private void Awake()
        {
            Install();
            InjectSystem();
        }

        private void Install()
        {
            foreach (var monoInstaller in _monoInstallers)
            {
                monoInstaller.Install(this);
            }
        }

        private void InjectSystem()
        {
            var gameObjects = gameObject.scene.GetRootGameObjects();

            foreach (var gameObj in gameObjects)
            {
                RecursionInject(gameObj.transform);
            }

        }

        private void RecursionInject(Transform gameObjTransform)
        {
            var components = gameObjTransform.GetComponents<MonoBehaviour>();
            foreach (var component in components)
            {
                Inject(component);
            }

            foreach (Transform child in gameObjTransform)
            {
                RecursionInject(child);
            }
        }
        
        public T GetService<T>() where T : class
        {
            var type = typeof(T);
            return _services[type] as T;
        }

        public void AddService<T>(object service)
        {
            var type = typeof(T);
            _services[type] = service;
        }

        public object GetService(Type parameterType)
        {
            return _services[parameterType];
        }

        private void Inject(MonoBehaviour monoBehaviour)
        {
            var type = monoBehaviour.GetType();

            var methods = type.GetMethods(
                BindingFlags.Instance | 
                BindingFlags.Public | 
                BindingFlags.NonPublic | 
                BindingFlags.FlattenHierarchy
                );

            foreach (MethodInfo methodInfo in methods)
            {
                if (!methodInfo.IsDefined(typeof(InjectAttribute)))
                {
                    continue;
                }

                ParameterInfo[] parametersInfo = methodInfo.GetParameters();

                var objects = new object[parametersInfo.Length];

                for (var index = 0; index < parametersInfo.Length; index++)
                {
                    var parameterInfo = parametersInfo[index];
                    Type parameterType = parameterInfo.ParameterType;
                    var parameterObject = GetService(parameterType);
                    objects[index] = parameterObject;
                }

                methodInfo.Invoke(monoBehaviour, objects);
            }

            var fields = type.GetFields(
                BindingFlags.Instance | 
                BindingFlags.Public | 
                BindingFlags.NonPublic | 
                BindingFlags.FlattenHierarchy
                );

            foreach (var fieldInfo in fields)
            {
                if (!fieldInfo.IsDefined(typeof(InjectAttribute)))
                {
                    continue;
                }
                
                Type parameterType = fieldInfo.FieldType;
                var parameterObject = GetService(parameterType);
                fieldInfo.SetValue(monoBehaviour, parameterObject);
            }
        }
    }
}