using System;
using System.Reflection;
using UnityEngine;

namespace SampleGame
{
    public class DependencyInjector : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _monoBehaviours;
        
        private void Start()
        {
            foreach (var monoBehaviour in _monoBehaviours)
            {
                Inject(monoBehaviour);
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
                    var arg = ServiceLocator.GetService(argType);
                    args[i] = arg;
                }

                methodInfo.Invoke(monoBehaviour, args);
            }
        }
    }
}