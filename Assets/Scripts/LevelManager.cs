using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject loaderCanvas;
    [SerializeField] private Image progressBar;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static async void LoadSceneAsync(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        if (Instance == null || Instance.loaderCanvas == null || Instance.progressBar == null)
            return;

        Instance.loaderCanvas.SetActive(true);
        Instance.progressBar.fillAmount = 0f;

        float duration = 5f;
        float timer = 0f;

        while (timer < duration)
        {
            await Task.Delay(100);
            timer += 0.1f;

            float progress = Mathf.Clamp01(timer / duration);

            if (Instance != null && Instance.progressBar != null)
                Instance.progressBar.fillAmount = progress;
        }

        scene.allowSceneActivation = true;

        await Task.Delay(500);
        if (Instance != null && Instance.loaderCanvas != null)
            Instance.loaderCanvas.SetActive(false);
    }
}