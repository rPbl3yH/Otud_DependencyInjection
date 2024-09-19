using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Zenject
{
    public class InputHandler : IInitializable
    {
        public void Initialize()
        {
            Debug.Log("Input handler initialize");
        }
    }
}