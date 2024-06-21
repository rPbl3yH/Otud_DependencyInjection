using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Zenject
{
    public sealed class MoveController : ITickable
    {
        private ICharacter _character;
        private IMoveInput _moveInput;

        [Inject]
        public void Construct(IMoveInput moveInput, ICharacter character)
        {
            _moveInput = moveInput;
            _character = character;
        }

        void ITickable.Tick()
        {
            _character.Move(_moveInput.GetDirection(), Time.deltaTime);
        }
    }
}