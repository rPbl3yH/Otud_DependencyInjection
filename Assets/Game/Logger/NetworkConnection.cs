using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Logger
{
    public class NetworkConnection : IAsyncDisposable
    {
        public NetworkConnection()
        {
            // имитация подключения
            Debug.Log("Подключение установлено.");
        }

        public async ValueTask DisposeAsync()
        {
            // имитация асинхронного закрытия соединения
            await Task.Delay(1000);
            Debug.Log("Подключение закрыто асинхронно.");
            GC.SuppressFinalize(this);
        }

        public static int[] MoveZerosToEnd(int[] array)
        {
            int insertIndex = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != 0)
                {
                    array[insertIndex] = array[i];
                    insertIndex++;
                }
            }

            while (insertIndex < array.Length)
            {
                array[insertIndex] = 0;
                insertIndex++;
            }
            
            return array;
        }

        ~NetworkConnection()
        {
            DisposeAsync().AsTask().Wait();
        }
    }

}