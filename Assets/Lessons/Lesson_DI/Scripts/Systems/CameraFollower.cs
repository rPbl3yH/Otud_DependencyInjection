using UnityEngine;

namespace Lessons.Lesson_DI
{
    public sealed class CameraFollower : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _offset;

        [SerializeField]
        private Camera _targetCamera;

        // [SerializeField] 
        private ICharacter _character;

        [Inject]
        public void Construct(ICharacter character)
        {
            _character = character;
        }
        
        private void LateUpdate()
        {
            var cameraPosition = _character.GetPosition() + _offset;
            _targetCamera.transform.position = cameraPosition;
        }
    }
}