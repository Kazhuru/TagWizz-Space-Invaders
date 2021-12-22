using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenuScene()
    {
        StartCoroutine(delayAfterSceneChange("MenuScene", 0f));
    }

    public void LoadMainMenuScene(float delaySceneChange)
    {
        StartCoroutine(delayAfterSceneChange("MenuScene", delaySceneChange));
    }

    public void LoadCreditsScene()
    {
        StartCoroutine(delayAfterSceneChange("CreditsScene", 0f));
    }

    public void LoadGameScene()
    {
        FindObjectOfType<GameSession>().RestartSessionVariables();
        StartCoroutine(delayAfterSceneChange("GameScene", 0f));
    }

    public void LoadGameoverScene()
    {
        StartCoroutine(delayAfterSceneChange("GameoverScene", 0f));
    }

    public void LoadGameoverScene(float delaySceneChange)
    {
        StartCoroutine(delayAfterSceneChange("GameoverScene", delaySceneChange));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator delayAfterSceneChange(string nameScene, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nameScene);
    }
}
