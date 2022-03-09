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

    private float fadingTime = 2f;

    private void Start()
    {
        //needed to make crossFadeAlpha work
        Color temp = background.color;
        temp.a = 1;
        background.color = temp;
        //
        loadingScreen.SetActive(false);
    }

    public void Load()
    {
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
        background.CrossFadeAlpha(1f, fadingTime, false);
        yield return new WaitForSeconds(fadingTime+0.5f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndexToLoad);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
