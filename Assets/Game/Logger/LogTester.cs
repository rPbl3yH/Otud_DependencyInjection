using System;
using UnityEngine;

namespace Game.Logger
{
    public class LogTester : MonoBehaviour
    {
        public bool IsException;
        
        public void LogMessage()
        {
            Debug.Log("Это обычный лог");
        }

        public void LogWarning()
        {
            Debug.LogWarning("Это предупреждение");
        }

        public void LogError()
        {
            Debug.LogError("Это ошибка");
        }

        public void ThrowException()
        {
            Debug.Log("До ошибки");
            
            if (IsException)
            {
                throw new Exception("Это исключение (Exception), вызванное вручную");
            }
            
            Debug.Log("После ошибки");
        }
    }
}