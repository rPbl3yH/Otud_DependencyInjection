using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Zenject
{
    public class TestHotKeysAdder : ITickable
    {
        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Hotkey triggered!");
            }
        }
    }
}