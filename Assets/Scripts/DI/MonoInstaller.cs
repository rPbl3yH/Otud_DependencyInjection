using UnityEngine;

namespace SampleGame
{
    public abstract class MonoInstaller : MonoBehaviour
    {
        public abstract void Install(DiContainer diContainer);
    }
}