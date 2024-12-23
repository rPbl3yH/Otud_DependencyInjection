using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class RotateComponent : MonoBehaviour
    {
        [SerializeField] private Transform _rotationRoot;
        [SerializeField] private float _rotateRate;
        [SerializeField] private bool _canRotate;

        private Vector3 _rotateDirection;

        private CompositeCondition _compositeCondition = new();
        
        public void SetDirection(Vector3 direction)
        {
            _rotateDirection = direction;
        }

        public void SetDirection(Transform target)
        {
            if (target == null)
            {
                return;
            }
            
            var direction = target.transform.position - transform.position;
            direction.y = 0f;
            SetDirection(direction);
        }

        private void Update()
        {
            Rotate();
        }

        private void Rotate()
        {
            if(!_canRotate || !_compositeCondition.IsTrue())
            {
                return;
            }

            if (_rotateDirection == Vector3.zero)
            {
                return;
            }

            var targetRotation = Quaternion.LookRotation(_rotateDirection, Vector3.up);
            _rotationRoot.rotation = Quaternion.Lerp(_rotationRoot.rotation, targetRotation, _rotateRate);
        }

        public void AddCondition(Func<bool> condition)
        {
            _compositeCondition.AddCondition(condition);
        }
    }
}