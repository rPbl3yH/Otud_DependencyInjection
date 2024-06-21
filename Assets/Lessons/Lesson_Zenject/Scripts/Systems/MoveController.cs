using UnityEngine;

namespace Lessons.Lesson_Zenject
{
    public sealed class MoveController : MonoBehaviour
    {
        [SerializeField]
        private Character _character;
        
        [SerializeField]
        private MoveInput _moveInput;

        private void Update()
        {
            _character.Move(_moveInput.GetDirection(), Time.deltaTime);
        }
    }
}