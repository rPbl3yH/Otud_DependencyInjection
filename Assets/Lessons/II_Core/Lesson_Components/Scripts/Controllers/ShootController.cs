using System;
using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class ShootController : MonoBehaviour
    {
        [SerializeField] private GameObject _character;
        
        private IRightHandComponent _rightHandComponent;
        private ILeftHandComponent _leftHandComponent;

        private void Awake()
        {
            _rightHandComponent = _character.GetComponent<IRightHandComponent>();
            _leftHandComponent = _character.GetComponent<ILeftHandComponent>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rightHandComponent.Shoot();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                _leftHandComponent.Shoot();
            }
        }
    }
}