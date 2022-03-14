using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : Interactable
{
    public Item itemData;

    //cache of item canvas
    //private GameObject UIHighlight;
    //
    private Coroutine lastCoroutineUI;

    bool isTaken = false;

    private float StartingScaleCanvas;

    private GameManager gameManager;


    public void Start()
    {
        gameManager = GameManager.instance;
        //get item canvas
        /*
        UIHighlight = gameObject.transform.GetChild(0).gameObject;
   
        StartingScaleCanvas = UIHighlight.gameObject.transform.localScale.x;

        UIHighlight.SetActive(false);
        */
    }

    public override void OnRay()
    {
        if (!isTaken)
        {
            gameManager.uiInteractManager.UpdateHighlightInfoText(0, itemData.itemName);
            //UIHighlight.SetActive(true);
            //lastCoroutineUI = StartCoroutine(AnimUI());
        }
    }

    public override void OnRayExit()
    {
        if (!isTaken)
        {
            gameManager.uiInteractManager.UpdateHighlightInfoText(-1, itemData.itemName);
            // StopCoroutine(lastCoroutineUI);
            // UIHighlight.SetActive(false);
        }
    }

    public override void OnPress(int n)
    {
        OnRayExit();
        isTaken = true;
        StartCoroutine(AnimTake());
    }

    public bool IsItemTaken()
    {
        return isTaken;
    }
    /*
    IEnumerator AnimUI()
    {
        Vector3 velocity = Vector3.zero;

        //lower the faster
        float animSpeed = 0.02f;

        //scale to this after overShoot
        Vector3 target = new Vector3(StartingScaleCanvas, StartingScaleCanvas, StartingScaleCanvas);

        //overshoot
        Vector3 overShoot = new Vector3(StartingScaleCanvas + 0.01f, StartingScaleCanvas + 0.01f, StartingScaleCanvas);
        bool hasOver = false;

        //starting scale
        UIHighlight.transform.localScale = new Vector3(0.03f, 0.03f, UIHighlight.transform.localScale.z);

        while (true)
        {
            
            if (!hasOver)
            {
                UIHighlight.transform.localScale = Vector3.SmoothDamp(UIHighlight.transform.localScale, overShoot, ref velocity, animSpeed);
                if (UIHighlight.transform.localScale.x > 0.098f)
                {
                    hasOver = true;
                }
            }
            else
            {
                UIHighlight.transform.localScale = Vector3.SmoothDamp(UIHighlight.transform.localScale, target, ref velocity, animSpeed+0.005f);
                if (UIHighlight.transform.localScale.x < 0.0905f)
                {
                    break;
                }
            }
            yield return new WaitForSecondsRealtime(0.017f);
        }
        yield return null;
    }
    */

    /*
    IEnumerator AnimUIExit()
    {
        Vector3 velocity = Vector3.zero;

        //lower the faster
        float animSpeed = 0.03f;

        Vector3 target = new Vector3(0, 0, 0);

        bool hasOver = false;

        //starting scale
        UIHighlight.transform.localScale = new Vector3(0.09f, 0.09f, UIHighlight.transform.localScale.z);
        while (true)
        {

            UIHighlight.transform.localScale = Vector3.SmoothDamp(UIHighlight.transform.localScale, target, ref velocity, animSpeed);
            if (UIHighlight.transform.localScale.x < 0.006f)
            {
                break;
            }
            yield return new WaitForSecondsRealtime(0.017f);
        };
        UIHighlight.SetActive(false);
        yield return null;
    }
    */
    IEnumerator AnimTake()
    {
        Vector3 velocity = Vector3.zero;

        //lower the faster
        float animSpeed = 0.022f;

        //scale to this
        Vector3 target = new Vector3(0,0,0);

        while (true)
        {
            gameObject.transform.localScale = Vector3.SmoothDamp(gameObject.transform.lossyScale, target, ref velocity, animSpeed);
            if (gameObject.transform.lossyScale.x < 0.05f)
            {
                break;
            }
            yield return new WaitForSecondsRealtime(0.017f);
        }
        Destroy(gameObject, 0.01f);
        yield return null;
    }
}
