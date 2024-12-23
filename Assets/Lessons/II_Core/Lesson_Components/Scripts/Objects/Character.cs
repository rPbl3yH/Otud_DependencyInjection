using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    //Mediator, Facade
    public class Character : MonoBehaviour, ShootComponent.ICondition, IRightHandComponent, ILeftHandComponent
    {
        [SerializeField] private LifeComponent _lifeComponent;
        [SerializeField] private RotateComponent _rotateComponent;
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private ShootComponent _rightHandShootComponent;
        [SerializeField] private ShootComponent _leftHandShootComponent;

        private RightHandCondition _rightHandCondition;
        
        private void Awake()
        {
            
            // _rightHandCondition = new RightHandCondition(this);
            // _rightHandShootComponent.Construct(_rightHandCondition);
            // _leftHandShootComponent.Construct(this);

            _rightHandShootComponent.AddCondition(_lifeComponent.IsAlive);
            _leftHandShootComponent.AddCondition(_lifeComponent.IsAlive);
            
            _rotateComponent.AddCondition(_lifeComponent.IsAlive);    
            _moveComponent.AddCondition(_lifeComponent.IsAlive);
        }

        bool ShootComponent.ICondition.Invoke()
        {
            return _lifeComponent.IsAlive();
        }
        
        private class RightHandCondition : ShootComponent.ICondition
        {
            private Character _character;

            public RightHandCondition(Character character)
            {
                _character = character;
            }

            public bool Invoke()
            {
                return true;
            }
        }

        void IRightHandComponent.Shoot()
        {
            _rightHandShootComponent.Shoot();
        }

        void ILeftHandComponent.Shoot()
        {
            _leftHandShootComponent.Shoot();
        }
    }
}
