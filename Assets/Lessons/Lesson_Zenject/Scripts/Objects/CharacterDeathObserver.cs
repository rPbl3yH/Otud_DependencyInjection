using Zenject;

namespace Lessons.Lesson_Zenject
{
    public class CharacterDeathObserver : IInitializable
    {
        private Character _character;
        private GameManager _gameManager;

        public CharacterDeathObserver(Character character, GameManager gameManager)
        {
            _character = character;
            _gameManager = gameManager;
        }

        public void Initialize()
        {
            _character.OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            _gameManager.Lose();
        }
    }
}