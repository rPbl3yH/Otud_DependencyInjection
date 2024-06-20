using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lessons.Lesson_Zenject
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private string _levelSceneName;
        [SerializeField] private string _systemSceneName;

        [Button]
        public void LoadLevel()
        {
            SceneManager.LoadScene(_levelSceneName, LoadSceneMode.Additive);
        }

        [Button]
        public void UnloadLevel()
        {
            SceneManager.UnloadSceneAsync(_levelSceneName);
        }

        [Button]
        public void LoadSystem()
        {
            SceneManager.LoadScene(_systemSceneName, LoadSceneMode.Additive);
        }

        [Button]
        public void UnloadSystem()
        {
            SceneManager.UnloadSceneAsync(_systemSceneName);
        }
    }
}