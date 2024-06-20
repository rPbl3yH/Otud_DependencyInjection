using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Zenject
{
    public class PlayerSpawner : MonoBehaviour
    {
        [Inject] private LevelProvider _levelProvider;
        [Inject] private Character _prefab;

        public void Start()
        {
            Instantiate(_prefab, _levelProvider.SpawnPoint);
        }
    }
}