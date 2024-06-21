using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Zenject
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Character _characterPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private string _name;

        [SerializeField] private Camera _camera;
        
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();
            Container.BindInterfacesTo<MoveInput>().AsSingle();
            Container.BindInterfacesTo<CameraFollower>().AsSingle();
            Container.BindInterfacesTo<MoveController>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<Character>()
                .FromComponentInNewPrefab(_characterPrefab, new GameObjectCreationParameters()
                {
                    Name = _name,
                    ParentTransform = _spawnPoint,
                }).AsCached();
        }
    }
}