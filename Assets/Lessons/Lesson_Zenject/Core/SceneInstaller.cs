using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Zenject
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Character _character;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Character>().FromInstance(_character).AsSingle();
        }
    }
}