using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTravelReceiver : Interactable
{
    private GameManager gameManager;
    public bool isObjInPresent = true;
    public bool isObjInFuture = true;

    private int selectTimeForObj;

    //bool recentTouch = false;

    private bool timeIsPresent;
    private bool isStatic = false;
    private TransformNew presentTransform;

    private Rigidbody rb;
    private Vector3 presentVelocity;
    private Vector3 presentAngularVelocity;

    public void Awake()
    {
        presentTransform = new TransformNew(gameObject.transform);
        if(gameObject.layer == 9)
        {
            isStatic = true;
        }

        if (!isStatic)
        {
            rb = gameObject.GetComponent<Rigidbody>();
        }
        gameManager = GameManager.instance;

        if (isObjInPresent && isObjInFuture) {selectTimeForObj = 1;}
        else if (isObjInPresent) {selectTimeForObj = 0;}
        else if (isObjInFuture) {selectTimeForObj = 2;}
        else{/*obj is not active by time change*/}


        //this last, needs most things above to work
        UpdateTimeShownForObj(false, true);
    }

    //called by TimeTravelManager, called when moving from future to present
    public void OnPresent()
    {
        timeIsPresent = true;
        if (isObjInPresent)
        {
            gameObject.SetActive(true);
            if (isObjInFuture)
            {
                //p+f, leaving future entering present
                if(rb != null)
                {
                    //uses present transforms again, because the present time was frozen in future, now unfrozen in present.
                    rb.velocity = presentVelocity;
                    rb.angularVelocity = presentAngularVelocity;
                    gameObject.transform.position = presentTransform.position;
                    gameObject.transform.eulerAngles = presentTransform.eulerRotation;
                    gameObject.transform.localScale = presentTransform.localScale;
                }
            }

        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    //called by TimeTravelManager, called when moving from present to future
    public void OnFuture()
    {
        timeIsPresent = false;
        if (isObjInFuture)
        {
            gameObject.SetActive(true);
            if (isObjInPresent)
            {
                //p+f, leaving present entering future, save transform in present, because present time is frozen while in future.. '
                if (!isStatic)
                {
                    presentVelocity = rb.velocity;
                    presentAngularVelocity = rb.angularVelocity;
                    presentTransform = new TransformNew(gameObject.transform);
                }

                /*
                if (recentTouch)
                {
                    StartCoroutine(s());
                }
                */
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    //not used right now
    public void TakeToOther()
    {
        isObjInPresent = true;
        isObjInFuture = true;
        presentTransform = new TransformNew(gameObject.transform);
    }

    //canvas
    public void UpdateTimeShownForObj(bool uptext, bool upProp)
    {
        if (uptext)
        {
            if (selectTimeForObj == 0)
            {
                //p
                //if statement prevents flicker in text when object is going to another time
                if (!timeIsPresent)
                {
                    gameManager.uiInteractManager.UpdateTimeSelectionText(-1);
                }
                else
                {
                    gameManager.uiInteractManager.UpdateTimeSelectionText(0);
                }
                isObjInPresent = true;
                isObjInFuture = false;
            }
            else if (selectTimeForObj == 1)
            {
                //p+f
                gameManager.uiInteractManager.UpdateTimeSelectionText(1);
                isObjInPresent = true;
                isObjInFuture = true;
            }
            else
            {
                //f
                //if statement prevents flicker in text when object is going to another time
                if (timeIsPresent)
                {
                    gameManager.uiInteractManager.UpdateTimeSelectionText(-1);
                }
                else
                {
                    gameManager.uiInteractManager.UpdateTimeSelectionText(2);
                }
                isObjInPresent = false;
                isObjInFuture = true;
            }
        }

        if (upProp)
        {
            if (timeIsPresent)
            {
                presentTransform = new TransformNew(gameObject.transform);
                OnPresent();
            }
            else
            {
                OnFuture();
            }
        }
    }

    public override void OnRay()
    {
        //recentTouch = true;
        gameManager.uiInteractManager.UpdateTimeSelectionText(-1);
        UpdateTimeShownForObj(true, false);
    }

    public override void OnRayExit()
    {
        gameManager.uiInteractManager.UpdateTimeSelectionText(-1);
    }

    public override void OnPress(int num)
    {
        if(num == 1)
        {
            //e
            if (selectTimeForObj < 2)
            {
                selectTimeForObj++;
            }
            UpdateTimeShownForObj(true, true);
        }
        else
        {
            //q
            if (selectTimeForObj > 0)
            {
                selectTimeForObj--;
            }
            UpdateTimeShownForObj(true, true);
        }

    }

    /*
    IEnumerator s()
    {
        Debug.Log("called");
        yield return new WaitForFixedUpdate();
        recentTouch = false;
        Physics.autoSimulation = false;
        for (int i = 0; i <160; i++)
        {

            Physics.Simulate(Time.deltaTime);
            if (rb.IsSleeping())
            {
                Debug.Log("ye " + i);
                break;
            }
        }
        Physics.autoSimulation = true;
        yield return null;
    }
    */


}