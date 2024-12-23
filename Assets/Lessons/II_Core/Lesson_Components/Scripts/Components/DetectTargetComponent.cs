using UnityEngine;

namespace Lessons.Lesson_Components
{
    public class DetectTargetComponent : MonoBehaviour
    {
        [SerializeField] private float _detectDistance = 3f;
        [SerializeField] private GameObject _character;
        
        public Transform GetTarget()
        {
            var direction = _character.transform.position - transform.position;

            if (direction.sqrMagnitude <= _detectDistance * _detectDistance)
            {
                return _character.transform;
            }

            return null;
        }

        public bool HasTarget()
        {
            return GetTarget() != null;
        }
    }
}