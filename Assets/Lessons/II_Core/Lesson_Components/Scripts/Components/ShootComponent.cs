using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public interface IRightHandComponent
    {
        void Shoot();
    }

    public interface ILeftHandComponent
    {
        void Shoot();
    }
    
    public class ShootComponent : ConditionComponent
    {
        public event Action OnFire; 
        
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _shootPoint;

        private ICondition _condition;

        public void Construct(ICondition condition)
        {
            _condition = condition;
        }
        
        public void Shoot()
        {
            if (!CompositeCondition.IsTrue())
            {
                return;
            }
            // if (!_condition.Invoke())
            // {
            //     return;
            // }
            
            Debug.Log("Fire!");
            var bullet = Instantiate(_bulletPrefab, _shootPoint.position, Quaternion.identity);

            if (bullet.TryGetComponent(out MoveComponent moveComponent))
            {
                moveComponent.SetDirection(_shootPoint.forward);
            }
            
            OnFire?.Invoke();
        }
        
        public interface ICondition
        {
            bool Invoke();
        }
    }
}