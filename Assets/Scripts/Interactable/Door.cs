using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//works both ways (direct player interact or button, lever...), can be one or the other or both, depending on layers and tag chosen.
public class Door : InteractablePuzzleReceiver
{
    public bool isOpen = false;

    private void Start()
    {
        if (isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    public override void OnPress(int num)
    {
        OnPressFromSwitch(1);
        UpdateMessage();
    }

    public override void OnPressFromSwitch(int n)
    {
        //TEMP, ADD ANIMATION???
        if (isOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }

    }

    private void OpenDoor()
    {
        gameObject.transform.localScale = new Vector3(2, 2, 2);
        isOpen = true;
    }

    private void CloseDoor()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        isOpen = false;
    }

    public override void OnRay()
    {
        UpdateMessage();
    }

    public override void OnRayExit()
    {
        ShowMessageExit();
    }

    private void UpdateMessage()
    {
        if (isOpen)
        {
            ShowMessage(1, "Close Door");
        }
        else
        {
            ShowMessage(1, "Open Door");
        }
    }
}

