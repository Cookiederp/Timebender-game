using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTravelReceiver : Interactable
{
    private UITimeSelection UITimeSelection_;
    public bool isObjInPresent = true;
    public bool isObjInFuture = true;
    private int selectTimeForObj;

    bool recentTouch = false;

    private bool timeIsPresent;

    private TransformNew presentTransform;

    private Rigidbody rb;
    private Vector3 presentVelocity;
    private Vector3 presentAngularVelocity;

    public void Awake()
    {
        presentTransform = new TransformNew(gameObject.transform);
        rb = gameObject.GetComponent<Rigidbody>();


        //canvas
        UITimeSelection_ = UITimeSelection.instance;
        //

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
                //p+f, leaving present entering future
                presentVelocity = rb.velocity;
                presentAngularVelocity = rb.angularVelocity;
                presentTransform = new TransformNew(gameObject.transform);
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
    public void UpdateTimeShownForObj(bool uptext, bool upPF)
    {
        if (uptext)
        {
            if (selectTimeForObj == 0)
            {
                //p
                UITimeSelection_.SetText(0);
                isObjInPresent = true;
                isObjInFuture = false;
            }
            else if (selectTimeForObj == 1)
            {
                //p+f
                UITimeSelection_.SetText(1);
                isObjInPresent = true;
                isObjInFuture = true;
            }
            else
            {
                //f
                UITimeSelection_.SetText(2);
                isObjInPresent = false;
                isObjInFuture = true;
            }
        }

        if (upPF)
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
        recentTouch = true;
        UITimeSelection_.SetActive(true);
        UpdateTimeShownForObj(true, false);
    }

    public override void OnRayExit()
    {
        UITimeSelection_.SetActive(false);
        UpdateTimeShownForObj(true, false);
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