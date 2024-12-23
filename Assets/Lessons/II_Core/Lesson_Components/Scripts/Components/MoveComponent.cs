using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private Vector3 _moveDirection;
        [SerializeField] private bool _canMove = true;

        private CompositeCondition _compositeCondition = new();
        
        private void Update()
        {
            Move();
        }

        public void SetDirection(Vector3 direction)
        {
            _moveDirection = direction;
        }

        private void Move()
        {
            if (!_canMove || !_compositeCondition.IsTrue())
            {
                return;
            }
            
            _root.position += _moveDirection * _speed * Time.deltaTime;
        }

        public void AddCondition(Func<bool> condition)
        {
            _compositeCondition.AddCondition(condition);
        }
    }
}