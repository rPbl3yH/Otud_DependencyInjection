using UnityEngine;

namespace SampleGame
{
    public class ServicesInstaller : Installer
    {
        [SerializeField] private Character _character;
        [SerializeField] private MoveInput _moveInput;
        
        public override void Install(DiContainer diContainer)
        {
            diContainer.AddService<ICharacter>(_character);
            diContainer.AddService<MoveInput>(_moveInput);
        }
    }
}