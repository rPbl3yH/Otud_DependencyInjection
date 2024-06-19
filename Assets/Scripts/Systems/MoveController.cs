using System;
using UnityEngine;

namespace SampleGame
{
    public sealed class MoveController : MonoBehaviour
    {
        private ICharacter _character;
        
        [Inject]
        private MoveInput _moveInput;

        [Inject]
        public void Construct(ICharacter character)
        {
            _character = character;
            // _moveInput = moveInput;
        }

        private void Update()
        {
            _character.Move(_moveInput.GetDirection(), Time.deltaTime);
        }
    }
}