using UnityEngine;

namespace SampleGame
{
    public abstract class Installer : MonoBehaviour
    {
        public abstract void Install(DiContainer diContainer);
    }
}