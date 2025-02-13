using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] private string menuSceneName;
    [SerializeField] private string gameSceneName;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private float fakeProgressDuration = 2f;
    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float fadeOutDuration = 0.5f;
    private string levelName;

    public void LoadScene(string sceneName)
    {
        this.levelName = sceneName;
        StartCoroutine(LoadLevelAsync());
    }

    public void LoadMenuScene()
    {
        this.levelName = menuSceneName;
        StartCoroutine(LoadLevelAsync());
    }

    public void LoadGameScene()
    {
        this.levelName = gameSceneName;
        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        loadingScreen.SetActive(true);
        
        // Fade in
        loadingScreen.GetComponent<Image>()
            .DOFade(1, fadeInDuration);

        float timer = 0f;
        while (timer < fakeProgressDuration)
        {
            timer += Time.deltaTime;
            // set progress bar value here
            yield return null;
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 1f);
            // set progress bar value here using the progress variable
            yield return null;
        }

        // Scene has finished loading
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelName)); // Make the new scene active
        
        // Fade out
        loadingScreen
            .GetComponent<Image>()
            .DOFade(0, fadeOutDuration)
            .OnComplete(()=> loadingScreen.SetActive(false));
    }
}
