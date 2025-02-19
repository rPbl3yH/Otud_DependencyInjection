using System;
using UnityEngine;

namespace Lessons.Lesson_DI
{
    public interface ICharacter
    {
        void Move(Vector3 direction, float deltaTime);
        Vector3 GetPosition();
    }

    public sealed class Character : MonoBehaviour, ICharacter
    {
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
    }
}