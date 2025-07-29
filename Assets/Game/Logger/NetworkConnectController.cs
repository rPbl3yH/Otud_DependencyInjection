using System.Threading.Tasks;
using UnityEngine;

namespace Game.Logger
{
    public class NetworkConnectController : MonoBehaviour
    {
        private async void Start()
        {
            await Connect();
        }

        public async Task Connect()
        {
            await using (NetworkConnection connection = new NetworkConnection())
            {
                Debug.Log("Использование сети");
            }
        }
    }
}