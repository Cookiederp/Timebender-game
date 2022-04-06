using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneEntry : MonoBehaviour
{

    public GameObject loadingScreen;
    public RawImage background;

    private float fadingTime = 2f;

    // Start is called before the first frame update
    void Start()
    {

        Application.targetFrameRate = -1;
        loadingScreen.SetActive(true);
        //needed to make crossFadeAlpha work
        Color temp = background.color;
        temp.a = 1;
        background.color = temp;
        background.CrossFadeAlpha(1f, 0f, false);
        Debug.Log("yesys");
        //
        background.CrossFadeAlpha(0f, fadingTime, false);
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(2f);
        loadingScreen.SetActive(false);
    }
}
