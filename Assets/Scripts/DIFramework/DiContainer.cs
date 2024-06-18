using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace SampleGame
{
    
    public class DiContainer : MonoBehaviour
    {
        [SerializeField] private Installer[] _installers;
        
        private static readonly Dictionary<Type, object> Services = new();
        
        private void Awake()
        {
            foreach (var installer in _installers)
            {
                installer.Install(this);
            }
        }
        
        private void Start()
        {
            var rootObjects = gameObject.scene.GetRootGameObjects();

            foreach (var rootObject in rootObjects)
            {
                RecursiveInject(rootObject.transform);
            }
        }

        public void AddService<T>(object service)
        {
            Services[typeof(T)] = service;
        }

        public T GetService<T>() where T : class
        {
            return Services[typeof(T)] as T;
        }

        public object GetService(Type argType)
        {
            return Services[argType];
        }

        private void RecursiveInject(Transform rootTransform)
        {
            var components = rootTransform.GetComponents<MonoBehaviour>();
            foreach (var monoBehaviour in components)
            {
                Inject(monoBehaviour);
            }
            
            foreach (Transform child in rootTransform)
            {
                RecursiveInject(child);
            }
        }

        private void Inject(object monoBehaviour)
        {
            Type type = monoBehaviour.GetType();

            var methodsInfo = type.GetMethods(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.FlattenHierarchy
            );

            foreach (var methodInfo in methodsInfo)
            {
                if (!methodInfo.IsDefined(typeof(InjectAttribute)))
                {
                    continue;
                }

                var parametersInfo = methodInfo.GetParameters();
                var args = new object[parametersInfo.Length];
                    
                for (int i = 0; i < parametersInfo.Length; i++)
                {
                    Type argType = parametersInfo[i].ParameterType;
                    var arg = Services[argType];
                    args[i] = arg;
                }

                methodInfo.Invoke(monoBehaviour, args);
            }
        }
    }
}