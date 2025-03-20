using Lessons.Lesson_DI;
using UnityEngine;
using DiContainer = Zenject.DiContainer;
using MonoInstaller = Zenject.MonoInstaller;

namespace Lessons.Lesson_Zenject
{
    public class SceneInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Debug.Log("Install bindings");
            
            // Container.BindInterfacesAndSelfTo<Character>().FromComponentInHierarchy().AsCached();
            
            Container.Bind<Enemy>().FromComponentsInHierarchy().AsCached();
            // Container.Bind<PlayerController>().AsSingle();
            Container.Bind<GameManager>().AsSingle().NonLazy();
            // Container.BindInterfacesTo<CharacterDeathObserver>().AsSingle();
            //
            Container.Bind<FootballManager>().AsSingle().NonLazy();

            // Container.Bind<Pool>().AsTransient();

        }
    }
}
