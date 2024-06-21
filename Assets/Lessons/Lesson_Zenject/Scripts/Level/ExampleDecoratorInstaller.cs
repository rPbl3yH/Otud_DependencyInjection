using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Zenject
{
    public class ExampleDecoratorInstaller : MonoInstaller
    {
        [SerializeField] private Character _character;
        
        public override void InstallBindings()
        {
            Container.Bind<ITickable>().To<TestHotKeysAdder>().AsSingle();
            Container.BindInterfacesAndSelfTo<Character>().FromInstance(_character).AsSingle();
        }
    }
}