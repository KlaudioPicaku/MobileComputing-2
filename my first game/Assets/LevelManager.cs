using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider loadingBar;
    private void Awake()
    {
        loadingScreen.SetActive(false);
    }
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronous(sceneIndex));
    }
    public void LoadLevel(int sceneIndex,string loadMode)
    {
        LoadAsynchronous(sceneIndex, loadMode);
    }
    public void LoadLevel(string sceneName)
    {
        LoadAsynchronous(sceneName);
    }
    public void LoadLevel(string sceneName,string loadMode)
    {
        LoadAsynchronous(sceneName,loadMode);
    }
    IEnumerator LoadAsynchronous(string sceneName, string loadMode)
    {
        AsyncOperation operation;
        if (loadMode.Equals("additive"))
        {
            operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        else
        {
            operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingBar.value = progress;

            yield return null;
        }
    }
    IEnumerator LoadAsynchronous(int sceneIndex,string loadMode)
    {
        AsyncOperation operation;
        if (loadMode.Equals("additive")) {
             operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        }
        else
        {
             operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        }
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingBar.value = progress;

            yield return null;
        } 
    }
    IEnumerator LoadAsynchronous(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingBar.value = progress;

            yield return null;
        }
    }
    IEnumerator LoadAsynchronous(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingBar.value = progress;

            yield return null;
        }
    }
}