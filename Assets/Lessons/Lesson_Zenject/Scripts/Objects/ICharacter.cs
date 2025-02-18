using UnityEngine;

namespace Lessons.Lesson_Zenject
{
    public interface ICharacter
    {
        void Move(Vector3 direction, float deltaTime);
        Vector3 GetPosition();
    }
}