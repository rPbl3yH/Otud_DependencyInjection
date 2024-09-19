using UnityEngine;
using Zenject;

namespace Lessons.Lesson_Zenject
{
    public class PlayerController
    {
        private readonly ICharacter _character;
        
        public PlayerController(ICharacter character)
        {
            _character = character;
            Debug.Log($"Character = {character}");
        }
    }
}