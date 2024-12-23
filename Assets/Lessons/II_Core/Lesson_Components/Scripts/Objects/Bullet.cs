using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage = 2;
        
        private void OnTriggerEnter(Collider other)
        {
            //Конвенция
            // var component = other.GetComponentInParent<IDamageable>();
            // if (component != null)
            // {
            //     component.TakeDamage(_damage);
            // }
            
            //Работа через прокси
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
            }   
            
            //Подписка на события
            // if (other.TryGetComponent(out TakeDamageAction damageAction))
            // {
            //     damageAction.Invoke(_damage);
            // }
        }
    }

    public class TakeDamageAction : MonoBehaviour
    {
        public event Action<int> OnDamaged;
        
        public void Invoke(int damage)
        {
            OnDamaged?.Invoke(damage);
        }
    }
    
    //Реализовать handler для самого ивента
    //OnTriggerEnter();

    //Отдельный компонент
    public class HitBox : MonoBehaviour
    {
        public GameObject Root => _root;
        
        [SerializeField] private GameObject _root;
    }
}