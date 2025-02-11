using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
    public string levelName;
    public GameObject loadingScreen;
    public float fakeProgressDuration = 2f;
    public float fadeInDuration = 0.5f;
    public float fadeOutDuration = 0.5f;

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelAsync());
    }

    public void LoadLevel(string levelName)
    {
        this.levelName = levelName;
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
