using UnityEngine;

namespace SampleGame
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Character _character;
        // [SerializeField] private MoveInput _moveInput;
        
        public override void Install(DiContainer diContainer)
        {
            var moveInput = FindObjectOfType<MoveInput>();
            diContainer.AddService<ICharacter>(_character);
            diContainer.AddService<MoveInput>(moveInput);
        }
    }
}