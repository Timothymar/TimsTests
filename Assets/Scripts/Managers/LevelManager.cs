using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Scenes
{
    MainMenu,
    CharacterCreation
    //GameWorld
}
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject loaderCanvas;
    [SerializeField] private Image progressBar;
    private void Awake()
    {
        Instance = this;
    }

    public void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadNewGame()
    {
        // Load the Character Creation scene
        SceneManager.LoadSceneAsync(Scenes.CharacterCreation.ToString());
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadMainMenu()
    {
        // Load the Main Menu scene
        SceneManager.LoadSceneAsync(Scenes.MainMenu.ToString());
    }

    //public void LoadGameWorld()
    //{
    //    // Load the Game World scene
    //    SceneManager.LoadScene(Scenes.GameWorld.ToString());
    //}

    public async void LoadSceneAsync(string sceneName)
    {
        if (loaderCanvas == null || progressBar == null)
            return;

        loaderCanvas.SetActive(true);
        progressBar.fillAmount = 0f;

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        while (scene.progress < 0.9f)
        {
            await Task.Delay(100);
            progressBar.fillAmount = scene.progress;
        }

        float timer = 0f;
        float duration = 1.5f;
        while (timer < duration)
        {
            await Task.Delay(50);
            timer += 0.05f;
            progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer / duration);
        }

        scene.allowSceneActivation = true;

        await Task.Delay(300);
        loaderCanvas.SetActive(false);
    }
}