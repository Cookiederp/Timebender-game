using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//works both ways (direct player interact or button, lever...), can be one or the other or both, depending on layers and tag chosen.
//might make this class a master, and have other doors reference it, idk.
public class Door : InteractablePuzzleReceiver
{
    public bool isOpen = false;
    public bool isLocked = false;
    private Vector3 closeEuler;
    private Vector3 openEuler;
    Coroutine lastCoroutine;
    public bool ignorePlayerPress;
    public bool readOnlyPressFromSwitch;
    public bool pressOnOnSignalOnly;
    //under power related
    public int onWhenOnSignalFromNumIsOn;
    public bool ignorePressWhenPowerOff;
    public bool needPowerToStayOpen;
    public bool needPowerToStayClose;

    private void Start()
    {
        closeEuler = gameObject.transform.eulerAngles;
        openEuler = new Vector3(closeEuler.x, closeEuler.y + 90, closeEuler.z);
        if (isOpen)
        {
            if (lastCoroutine != null)
            {
                StopCoroutine(lastCoroutine);
            }
            lastCoroutine = StartCoroutine(OpenAnim());
        }
        else
        {
            if (lastCoroutine != null)
            {
                StopCoroutine(lastCoroutine);
            }
            lastCoroutine = StartCoroutine(CloseAnim());
        }
    }

    //press from player
    public override void OnPress(int num)
    {

        if (ignorePlayerPress == false)
        {
            if (ignorePressWhenPowerOff)
            {
                if (onWhenOnSignalFromNumIsOn == 0)
                {
                    //true when the perfect amount of switches are on/off
                    UpdateDoorState();
                }
            }
            else
            {
                UpdateDoorState();
            }
        }
    }

    public override void OnRay()
    {
        if (ignorePlayerPress == false)
        {
            if (ignorePressWhenPowerOff)
            {
                if (onWhenOnSignalFromNumIsOn == 0)
                {
                    //on
                    UpdateMessage();
                }
                else
                {
                    //not enought switch on, off
                    ShowMessage(1, "Need Power");
                }
            }
            else
            {
                UpdateMessage();
            }
        }
        else
        {
            ShowMessage(1, "...");
        }
    }

    public override void OnRayExit()
    {
        base.ShowMessageExit();
    }

    //press from buttons.. levers.. not player.
    public override void OnPressFromSwitch(int n)
    {
        //n < 1 = off signal,
        if(n < 1)
        {
            if (ignorePressWhenPowerOff)
            {
                onWhenOnSignalFromNumIsOn++;
            }
        }
        //n > 0 = on signal
        else
        {
            if (ignorePressWhenPowerOff)
            {
                onWhenOnSignalFromNumIsOn--;
            }
        }
 
 
        if (readOnlyPressFromSwitch == false)
        {
            if (pressOnOnSignalOnly && n < 1)
            {
                //signal was off, dont do anything if pressOnOnSignalOnly was true
            }
            else
            {
                if (ignorePressWhenPowerOff)
                {
                    if(onWhenOnSignalFromNumIsOn == 0)
                    {
                        UpdateDoorState();
                    }
                    else if (needPowerToStayOpen)
                    {
                        if (isOpen)
                        {
                            UpdateDoorState();
                        }
                    }
                    else if (needPowerToStayClose)
                    {
                        if (isOpen == false)
                        {
                            UpdateDoorState();
                        }
                    }
                }
            }
        }
    }

    private void UpdateDoorState()
    {
        if (isOpen)
        {
            if (!isLocked)
            {
                CloseDoor();
            }
        }
        else
        {
            if (!isLocked)
            {
                OpenDoor();
            }
        }
    }

    public override void OnKeyUnlock()
    {
        isLocked = false;
    }

    public override void OnKeyLock()
    {
        isLocked = true;
    }

    private void OpenDoor()
    {
        if(lastCoroutine != null)
        {
            StopCoroutine(lastCoroutine);
        }
        lastCoroutine = StartCoroutine(OpenAnim());

        isOpen = true;
        UpdateMessage();
    }

    private void CloseDoor()
    {
        if (lastCoroutine != null)
        {
            StopCoroutine(lastCoroutine);
        }
        lastCoroutine = StartCoroutine(CloseAnim());

        isOpen = false;
        UpdateMessage();
    }

    private void UpdateMessage()
    {
        if (!isLocked)
        {
            if (isOpen)
            {
                ShowMessage(0, "Close Door");
            }
            else
            {
                ShowMessage(0, "Open Door");
            }
        }
        else
        {
            ShowMessage(1, "Door is Locked");
        }

    }

    IEnumerator OpenAnim()
    {
        Vector3 velocity = Vector3.zero;
        //lower the faster
        float speed = 0.5f;
        int i = 140;
        while (i > 0)
        {
            i--;
            gameObject.transform.eulerAngles = Vector3.SmoothDamp(gameObject.transform.eulerAngles, openEuler, ref velocity, speed);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

    IEnumerator CloseAnim()
    {
        Vector3 velocity = Vector3.zero;
        //lower the faster
        float speed = 0.5f;
        int i = 140;
        while (i > 0)
        {
            i--;
            gameObject.transform.eulerAngles = Vector3.SmoothDamp(gameObject.transform.eulerAngles, closeEuler, ref velocity, speed);
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
}

