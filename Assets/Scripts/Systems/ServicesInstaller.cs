using UnityEngine;

namespace SampleGame
{
    public class ServicesInstaller : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private MoveInput _moveInput;
        
        private void Awake()
        {
            ServiceLocator.AddService<ICharacter>(_character);
            ServiceLocator.AddService<MoveInput>(_moveInput);
        }
    }
}