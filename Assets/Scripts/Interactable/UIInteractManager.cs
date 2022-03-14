using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIInteractManager : MonoBehaviour
{
    private TextMeshProUGUI timeSelectionText;
    private int tsize = 3;
    private string[] tstring;

    private TextMeshProUGUI highlightInfoText;
    private int hsize = 2;
    private string[] hstring;

    private Coroutine LastCoroutine = null;

    private void Awake()
    {
        tstring = new string[tsize];
        tstring[0] = "Present -> P+F [E]";
        tstring[1] = "[Q] P <- Present+Future -> F [E]";
        tstring[2] = "[Q] P+F <- Future";

        hstring = new string[hsize];
        hstring[0] = "[F] - Take ";
        hstring[1] = "[F] - Open ";
    }

    void Start()
    {
        timeSelectionText = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        highlightInfoText = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        UpdateTimeSelectionText(-1);
        UpdateHighlightInfoText(-1);
    }

    //
    public void UpdateTimeSelectionText(int index)
    {
        if(index == -1)
        {
            timeSelectionText.text = string.Empty;
            StopAllCoroutines();
        }
        else
        {
            if (index < tsize)
            {
                StopAllCoroutines();
                timeSelectionText.text = tstring[index];
                StartCoroutine(AnimUIOvershoot(timeSelectionText, 1.02f, 0.5f));
                StartCoroutine(AnimUIFade(timeSelectionText, 0.2f, 0.8f, 1f));
            }
            else
            {
                Debug.Log("error..." + index);
            }
        }

    }

    public void UpdateHighlightInfoText(int index, string objectName)
    {
        if (index == -1)
        {
            highlightInfoText.text = string.Empty;
            StopAllCoroutines();
        }
        else
        {
            if (index < hsize)
            {
                StopAllCoroutines();
                highlightInfoText.text = hstring[index] + objectName;
                StartCoroutine(AnimUIOvershoot(highlightInfoText, 1.1f, 1f));
                StartCoroutine(AnimUIFade(highlightInfoText, 0f, 0.8f, 1.5f));
            }
            else
            {
                Debug.Log("error...");
            }
        }
    }

    public void UpdateHighlightInfoText(int index)
    {
        UpdateHighlightInfoText(index, string.Empty);
    }


    
    IEnumerator AnimUIOvershoot(TextMeshProUGUI textToAnim, float overAmount, float speed)
    {
        Vector3 velocity = Vector3.zero;

        //lower the faster
        float animSpeedShoot = 0.02f / speed;
        //
        float animSpeedBack = animSpeedShoot * 1.35f;
        float defscale = 1f;
        float overBy = defscale * overAmount;
        float confOver = overBy * 0.995f;
        float confDef = defscale * 1.005f;

        //scale to this after overShoot
        Vector3 target = new Vector3(defscale, defscale, defscale);

        //overshoot
        Vector3 overShoot = new Vector3(overBy, overBy, defscale);
        bool hasOver = false;

        //starting scale
        textToAnim.transform.localScale = new Vector3(1f, 1f, defscale);

        while (true)
        {
            if (!hasOver)
            {
                textToAnim.transform.localScale = Vector3.SmoothDamp(textToAnim.transform.localScale, overShoot, ref velocity, animSpeedShoot);
                if (textToAnim.transform.localScale.x > confOver)
                {
                    hasOver = true;
                }
            }
            else
            {
                textToAnim.transform.localScale = Vector3.SmoothDamp(textToAnim.transform.localScale, target, ref velocity, animSpeedBack);
                if (textToAnim.transform.localScale.x < confDef)
                {
                    break;
                }
            }
            yield return new WaitForSecondsRealtime(0.017f);
        }
        yield return null;
    }


    IEnumerator AnimUIFade(TextMeshProUGUI textToAnim, float startAlpha, float endAlpha, float speed)
    {
        //fade
        float defAlpha = startAlpha;
        textToAnim.alpha = defAlpha;
        float goalAlpha = endAlpha;
        float step = 0.008f * speed;


        while (textToAnim.alpha < goalAlpha)
        {
            //fade
            defAlpha += step;
            textToAnim.alpha = step + defAlpha;
            yield return new WaitForSecondsRealtime(0.017f);
        }
        yield return null;
    }
}
