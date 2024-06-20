using UnityEngine;

namespace Lessons.Lesson_DI
{
    public sealed class MoveController : MonoBehaviour
    {
        private ICharacter _character;
        private MoveInput _moveInput;

        [Inject]
        public void Construct(ICharacter character, MoveInput moveInput)
        {
            _character = character;
            _moveInput = moveInput;
        }

        private void Update()
        {
            _character.Move(_moveInput.GetDirection(), Time.deltaTime);
        }
    }
}