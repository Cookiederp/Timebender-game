using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public int sceneIndexToLoad;
    public GameObject loadingScreen;
    public RawImage background;

    private GameManager gameManager;

    private float fadingTime = 2f;

    private void Start()
    {
        gameManager = GameManager.instance;
        //needed to make crossFadeAlpha work
        Color temp = background.color;
        temp.a = 1;
        background.color = temp;
        //
        loadingScreen.SetActive(false);
    }

    public void Load()
    {
        if(gameManager != null)
        {
            gameManager.OnLoad();
        }
        Time.timeScale = 0;
        loadingScreen.SetActive(true);
        //needed to make crossFadeAlpha work
        background.CrossFadeAlpha(0f, 0f, true);
        //
        StartCoroutine(LoadYourAsyncScene());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Load();
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        background.CrossFadeAlpha(1f, fadingTime, true);
        yield return new WaitForSecondsRealtime(fadingTime+0.5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndexToLoad);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
