using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Zenject
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private LevelProvider _levelProvider;

        public override void InstallBindings()
        {
            Container.Bind<LevelProvider>().FromInstance(_levelProvider).AsCached();
        }
    }
}