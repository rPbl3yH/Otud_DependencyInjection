using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Zenject
{
    public sealed class MoveController : MonoBehaviour
    {
        [Inject]
        private ICharacter _character;
        
        [SerializeField]
        private MoveInput _moveInput;

        private void Update()
        {
            _character.Move(_moveInput.GetDirection(), Time.deltaTime);
        }
    }
}