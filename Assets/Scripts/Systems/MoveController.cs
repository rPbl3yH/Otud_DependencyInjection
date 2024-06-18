using UnityEngine;

namespace SampleGame
{
    public sealed class MoveController : MonoBehaviour
    {
        private ICharacter _character;
        private MoveInput _moveInput;
        
        [Inject]
        public void Construct(MoveInput moveInput, ICharacter character)
        {
            _character = character;
            _moveInput = moveInput;
            Debug.Log("Move input construct");
        }

        private void Update()
        {
            _character.Move(_moveInput.GetDirection(), Time.deltaTime);
        }
    }
}