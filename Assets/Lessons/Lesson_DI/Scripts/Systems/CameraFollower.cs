using UnityEngine;

namespace Lessons.Lesson_DI
{
    public sealed class CameraFollower : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _offset;

        [SerializeField]
        private Camera _targetCamera;

        [Inject]
        private ICharacter _character;

        private void LateUpdate()
        {
            var cameraPosition = _character.GetPosition() + _offset;
            _targetCamera.transform.position = cameraPosition;
        }
    }
}