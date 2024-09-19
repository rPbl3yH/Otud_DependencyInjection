using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Zenject
{
    public interface IMoveInput
    {
        Vector3 GetDirection();
    }

    public sealed class MoveInput : IInitializable, IMoveInput
    {
        private MoveInputConfig _moveInputConfig;

        public MoveInput(MoveInputConfig moveInputConfig)
        {
            _moveInputConfig = moveInputConfig;
        }

        void IInitializable.Initialize()
        {
            Debug.Log("Initialize");
        }

        public Vector3 GetDirection()
        {
            Vector3 direction = Vector3.zero;
            
            if (Input.GetKey(_moveInputConfig.Up))
            {
                direction.z = 1;
            }
            else if (Input.GetKey(_moveInputConfig.Down))
            {
                direction.z = -1;
            }

            if (Input.GetKey(_moveInputConfig.Left))
            {
                direction.x = -1;
            }
            else if (Input.GetKey(_moveInputConfig.Right))
            {
                direction.x = 1;
            }

            return direction;
        }
    }
}