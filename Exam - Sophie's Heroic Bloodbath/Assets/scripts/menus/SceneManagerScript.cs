using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    /*
    Title: How to make a LOADING BAR in Unity
    Author: Asbjørn Thirslund / Brackeys
    Date: 14 May 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=YMj2qPq9CP8&t=258s
    */

    public static SceneManagerScript sceneManager;//in this case, static means youve made the accessable within other scripts, but wont show in the inspector

    [Header("loading screen ui")]
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;
   
    void Awake()//awake is called before start, and is most usefull in this case, for setting up references to other scripts as its called before the start method
    {
        Time.timeScale = 1f;

        /*
        if (sceneManager == null)
        {
            sceneManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);//as a new scene is loaded, the gameobject this script is attached to will follow, and if the object already exists, then this (now duplicate) is deleted
        }
        */
    }

    public void NextLevel()
    {
        StartCoroutine(LoadNextLevelAsynchronously());
    }

    IEnumerator LoadNextLevelAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        loadingScreen.SetActive(true);

        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            if (progressText != null)
            {
                progressText.text = progress * 100f + "%";//turns progress to a percentage
            }

            yield return null;
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsynchronously(sceneName));
    }

    IEnumerator LoadSceneAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            if (progressText != null)
            {
                progressText.text = progress * 100f + "%";//turns progress to a percentage
            }

            yield return null;
        }
    }

    public void restart()
    {
        StartCoroutine(LoadActiveSceneAsynchronously());
    }

    IEnumerator LoadActiveSceneAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

        loadingScreen.SetActive(true);

        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            if (progressText != null)
            {
                progressText.text = progress * 100f + "%";//turns progress to a percentage
            }

            yield return null;
        }
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quit.");
    }
}
