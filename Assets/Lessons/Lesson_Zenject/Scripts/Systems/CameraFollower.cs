using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Zenject
{
    public sealed class CameraFollower : ILateTickable
    {
        private Vector3 _offset = new Vector3(0f, 7f, -10f);

        [Inject]
        private Camera _targetCamera;

        [Inject]
        private ICharacter _character;

        void ILateTickable.LateTick()
        {
            var cameraPosition = _character.GetPosition() + _offset;
            _targetCamera.transform.position = cameraPosition;
        }
    }
}