using UnityEngine;

namespace Lessons.Lesson_Zenject
{
    [CreateAssetMenu(
        fileName = "MoveInputConfig",
        menuName = "InputConfig/New MoveInputConfig"
    )]
    public class MoveInputConfig : ScriptableObject
    {
        public KeyCode Up;
        public KeyCode Down;
        public KeyCode Left;
        public KeyCode Right;
    }
}