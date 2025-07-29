using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OnStartClicked()
    {
        SceneManager.LoadScene("GameScene");
    }
}