using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Zenject
{
    public class Bootstrap : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;
        
        [ShowInInspector, ReadOnly]
        private LevelProvider _levelProvider;

        [Button]
        public void GetProvider()
        {
            _levelProvider = _diContainer.Resolve<LevelProvider>();
        }
        
        [Button]
        public void ShowWorld()
        {
            _levelProvider.gameObject.SetActive(true);
        }
    }
}