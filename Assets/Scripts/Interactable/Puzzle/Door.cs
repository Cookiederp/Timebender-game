using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//works both ways (direct player interact or button, lever...), can be one or the other or both, depending on layers and tag chosen.
public class Door : InteractablePuzzleReceiver
{
    public bool isOpen = false;
    public bool isLocked = false;

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

    //press from player
    public override void OnPress(int num)
    {
        OnPressFromSwitch(1);
        UpdateMessage();
    }

    public override void OnRay()
    {
        UpdateMessage();
    }

    public override void OnRayExit()
    {
        base.ShowMessageExit();
    }

    //press from buttons.. levers.. not player.
    public override void OnPressFromSwitch(int n)
    {
        //TEMP, ADD ANIMATION???
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
}

