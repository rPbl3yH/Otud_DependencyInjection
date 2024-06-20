using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Zenject
{
    public sealed class CameraFollower : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _offset;

        [SerializeField]
        private Camera _targetCamera;

        [Inject]
        private Character _character;

        private void LateUpdate()
        {
            var cameraPosition = _character.GetPosition() + _offset;
            _targetCamera.transform.position = cameraPosition;
        }
    }
}