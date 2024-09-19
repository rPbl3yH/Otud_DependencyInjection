using UnityEngine;

namespace Lessons.Lesson_DI
{
    public abstract class MonoInstaller : MonoBehaviour
    {
        public abstract void Install(DiContainer diContainer);
    }
}