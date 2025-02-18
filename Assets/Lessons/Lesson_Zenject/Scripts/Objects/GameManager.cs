using UnityEngine;

namespace Lessons.Lesson_Zenject
{
    public class GameManager
    {
        private readonly InputHandler _inputHandler;

        public GameManager(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            Debug.Log($"Input handler = {_inputHandler}");
        }

        public void Lose()
        {
            Debug.Log("Lose");
            Time.timeScale = 0f;
        }
    }
}