using System;
using System.Reflection;
using UnityEngine;

namespace Lessons.Lesson_DI
{
    public class DependencyInjector : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private MoveInput _moveInput;
        
        private void Install()
        {
            ServiceLocator.Instance.AddService<ICharacter>(_character);
            ServiceLocator.Instance.AddService<MoveInput>(_moveInput);
        }
        
        private void Start()
        {
            Install();
            InjectAll();
        }

        private void InjectAll()
        {
            var sceneObjects = gameObject.scene.GetRootGameObjects();

            foreach (var sceneObject in sceneObjects)
            {
                RecursiveInject(sceneObject.transform);
            }
        }

        public void RecursiveInject(Transform objectTransform)
        {
            var monobehaviours = objectTransform.GetComponentsInChildren<MonoBehaviour>();

            foreach (MonoBehaviour monoBehaviour in monobehaviours)
            {
                Inject(monoBehaviour);
            }

            foreach (Transform childTransform in objectTransform)
            {
                RecursiveInject(childTransform);
            }
        }

        public void Inject(object targetObject)
        {
            Type type = targetObject.GetType();
            MethodInfo[] methodsInfo = type.GetMethods(
                BindingFlags.Public | 
                BindingFlags.Instance | 
                BindingFlags.FlattenHierarchy | 
                BindingFlags.NonPublic);

            foreach (MethodInfo methodInfo in methodsInfo)
            {
                if (!methodInfo.IsDefined(typeof(InjectAttribute)))
                {
                    continue;
                }

                ParameterInfo[] parameters = methodInfo.GetParameters();
                object[] arguments = new object[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    ParameterInfo parameter = parameters[i];
                    Type parameterType = parameter.ParameterType;
                    var argument = ServiceLocator.Instance.GetService(parameterType);
                    arguments[i] = argument;
                }

                methodInfo.Invoke(targetObject, arguments);
            }
        }
    }
}