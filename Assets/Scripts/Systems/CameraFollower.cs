using System;
using UnityEngine;

namespace SampleGame
{
    public sealed class CameraFollower : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _offset;

        [SerializeField]
        private Camera _targetCamera;

        [SerializeField] 
        private Character _character;

        private void LateUpdate()
        {
            var cameraPosition = _character.GetPosition() + _offset;
            _targetCamera.transform.position = cameraPosition;
        }
    }
}