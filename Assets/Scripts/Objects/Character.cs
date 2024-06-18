using System;
using UnityEngine;

namespace SampleGame
{
    public sealed class Character : MonoBehaviour
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