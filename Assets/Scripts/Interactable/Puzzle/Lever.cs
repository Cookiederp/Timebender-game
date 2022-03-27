using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : InteractablePuzzleCaller
{
    public bool isOn = false;
    private GameObject handleObj;
    private Coroutine lastCoroutine;
    private Vector3 openEuler;
    private Vector3 closeEuler;


    private void Start()
    {
        handleObj = gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
        openEuler = new Vector3(0, 0, 0);
        closeEuler = new Vector3(80, 0, 0);

        if (isOn)
        {
            handleObj.transform.localEulerAngles = openEuler;
        }
        else
        {
            handleObj.transform.localEulerAngles = closeEuler;
        }
    }

    public override void OnRay()
    {
        UpdateMessage();
    }

    public override void OnRayExit()
    {
        ShowMessageExit();
    }

    public override void OnPress(int num)
    {
        if (isOn)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    private void UpdateMessage()
    {
        if (isOn)
        {
            ShowMessage(0, "Turn Off");
        }
        else
        {
            ShowMessage(0, "Turn On");
        }
    }

    private void TurnOn()
    {
        isOn = true;
        UpdateMessage();
        if (lastCoroutine != null)
        {
            StopCoroutine(lastCoroutine);
        }
        lastCoroutine = StartCoroutine(OpenAnim());
        base.OnPress(1);
    }
    
    private void TurnOff()
    {
        isOn = false;
        UpdateMessage();

        if(lastCoroutine != null)
        {
            StopCoroutine(lastCoroutine);
        }
        lastCoroutine = StartCoroutine(CloseAnim());
        base.OnPress(0);
    }

    //turn on
    IEnumerator OpenAnim()
    {
        Vector3 velocity = Vector3.zero;
        //lower the faster
        float speed = 0.2f;
        int i = 100;
        while (i > 0)
        {
            i--;
            handleObj.transform.localEulerAngles = Vector3.SmoothDamp(handleObj.transform.localEulerAngles, openEuler, ref velocity, speed);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

    //turn off
    IEnumerator CloseAnim()
    {
        Vector3 velocity = Vector3.zero;
        //lower the faster
        float speed = 0.2f;
        int i = 100;
        while (i > 0)
        {
            i--;
            handleObj.transform.localEulerAngles = Vector3.SmoothDamp(handleObj.transform.localEulerAngles, closeEuler, ref velocity, speed);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

}
