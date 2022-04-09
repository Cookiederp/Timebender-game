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
    private int hsize = 3;
    private string[] hstring;

    private TextMeshProUGUI controlInfoText;

    public GameObject noteHolder;
    public GameObject[] noteObj;
    private int currentNId;
    
    bool reading = false;

    private Coroutine[] LastCoroutine;

    private void Awake()
    {
        LastCoroutine = new Coroutine[4];

        tstring = new string[tsize];
        tstring[0] = "Present";
        tstring[1] = "Present+Future";
        tstring[2] = "Future";

        hstring = new string[hsize];
        hstring[0] = "[F] - ";
        hstring[1] = "";

        timeSelectionText = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        highlightInfoText = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        controlInfoText = gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        UpdateTimeSelectionText(-1);
        UpdateHighlightInfoText(-1);
        UpdateControlInfoText("");

        noteHolder.SetActive(false);
    }

    void Update()
    {
        if (reading)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                OnLeaveReadNote();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                OnLeaveReadNote();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                OnLeaveReadNote();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                OnLeaveReadNote();
            }

            if (Input.GetMouseButtonDown(0))
            {
                OnLeaveReadNote();
            }
        }
    }

    //
    public void UpdateTimeSelectionText(int index)
    {
        if(index == -1)
        {
            timeSelectionText.text = string.Empty;
            for (int i = 0; i < 2; i++)
            {
                if (LastCoroutine[i] != null)
                {
                    StopCoroutine(LastCoroutine[i]);
                }
            }
        }
        else
        {
            if (index < tsize)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (LastCoroutine[i] != null)
                    {
                        StopCoroutine(LastCoroutine[i]);
                    }
                }
                timeSelectionText.text = tstring[index];
                LastCoroutine[0]=StartCoroutine(AnimUIOvershoot(timeSelectionText, 1.02f, 0.5f));
                LastCoroutine[1]=StartCoroutine(AnimUIFade(timeSelectionText, 0.2f, 0.8f, 1f));
            }
            else
            {
                Debug.Log("error..." + index);
            }
        }

    }

    public void UpdateHighlightInfoText(int index, string str)
    {
        if (index == -1)
        {
            highlightInfoText.text = string.Empty;

            for(int i = 2; i<4; i++)
            {
                if (LastCoroutine[i] != null)
                {
                    StopCoroutine(LastCoroutine[i]);
                }
            }
        }
        else
        {
            if (index < hsize)
            {
                for (int i = 2; i < 4; i++)
                {
                    if (LastCoroutine[i] != null)
                    {
                        StopCoroutine(LastCoroutine[i]);
                    }
                }
                highlightInfoText.text = hstring[index] + str;
                LastCoroutine[2]=StartCoroutine(AnimUIOvershoot2(highlightInfoText, 1.1f, 1f));
                LastCoroutine[3]=StartCoroutine(AnimUIFade2(highlightInfoText, 0f, 0.8f, 1.5f));
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

    public void UpdateControlInfoText(string str)
    {
        controlInfoText.text = str;
    }


    public void OnReadNote(int noteId)
    {
        noteHolder.SetActive(true);
        noteObj[noteId].SetActive(true);
        currentNId = noteId;
        reading = true;
    }

    public void OnLeaveReadNote()
    {
       noteObj[currentNId].SetActive(false);
       noteHolder.SetActive(false);
       reading = false;
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

    //this exists because only one can run at a time for one op, and I needed 2. will maybe change this later.
    IEnumerator AnimUIOvershoot2(TextMeshProUGUI textToAnim, float overAmount, float speed)
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

    //this exists because only one can run at a time for one op, and I needed 2. will maybe change this later.
    IEnumerator AnimUIFade2(TextMeshProUGUI textToAnim, float startAlpha, float endAlpha, float speed)
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
