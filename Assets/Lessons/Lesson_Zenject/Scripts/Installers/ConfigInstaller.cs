using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Zenject
{
    [CreateAssetMenu(
        fileName = "ConfigInstaller",
        menuName = "Config/New ConfigInstaller"
    )]
    public class ConfigInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private MoveInputConfig _moveInputConfig;

        public override void InstallBindings()
        {
            
            Container.Bind<MoveInputConfig>().FromInstance(_moveInputConfig).AsSingle();
        }
    }
}