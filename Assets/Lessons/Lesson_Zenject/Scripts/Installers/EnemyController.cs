using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Zenject
{
    public class EnemyController : MonoBehaviour
    {
        [Inject] 
        [ShowInInspector]
        private List<Enemy> _enemies;
    }
}