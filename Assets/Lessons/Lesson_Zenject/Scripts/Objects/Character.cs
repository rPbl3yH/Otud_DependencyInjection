using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Lesson_Zenject
{
    public sealed class Character : MonoBehaviour, ICharacter
    {
        public event Action OnDeath;
        
        [SerializeField]
        private float _speed = 2.5f;
        
        public void Move(Vector3 direction, float deltaTime)
        {
            transform.position += direction * (deltaTime * _speed);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        [Button]
        public void Death()
        {
            OnDeath?.Invoke();
        }
    }
}