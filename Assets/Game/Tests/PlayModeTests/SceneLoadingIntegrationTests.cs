using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class SceneLoadingIntegrationTests
{
    [UnityTest]
    public IEnumerator WhenLoadMainMenuScene_AndSceneHasUI_ThenStartButtonIsPresentAndInteractable()
    {
        var operation = SceneManager.LoadSceneAsync("MainMenu");
        operation.allowSceneActivation = true;

        // Ждём, пока сцена загрузится и станет активной
        while (!operation.isDone)
            yield return null;

        var startButton = GameObject.Find("StartButton")?.GetComponent<Button>();
        Assert.IsNotNull(startButton, "StartButton не найден");
        Assert.IsTrue(startButton.interactable, "StartButton не interactable");
    }
    
    [UnityTest]
    public IEnumerator WhenClickStart_AndButtonIsConnected_ThenGameSceneIsLoaded()
    {
        // Загружаем MainMenu
        SceneManager.LoadScene("MainMenu");
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "MainMenu");
        yield return null; // подстраховка

        // Находим кнопку
        var startButtonObject = GameObject.Find("StartButton");
        Assert.IsNotNull(startButtonObject, "StartButton не найден");

        var startButton = startButtonObject.GetComponent<Button>();
        Assert.IsTrue(startButton.interactable, "StartButton не interactable");

        // Кликаем по кнопке
        startButton.onClick.Invoke();

        // Ждём, пока загрузится GameScene
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "GameScene");

        // Проверка, что новая сцена загружена
        Assert.AreEqual("GameScene", SceneManager.GetActiveScene().name);
    }
}