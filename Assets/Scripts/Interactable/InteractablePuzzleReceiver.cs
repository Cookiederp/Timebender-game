using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePuzzleReceiver : InteractablePuzzle
{
    public bool PressOnPressFromCaller;
    public bool destroyOnPressFromCaller;
    public bool unlockOnPressFromCaller;
    public bool unlockOnKeyFromCaller;

    //onpress from switch (button, lever..)
    public virtual void OnPressFromSwitch(int num)
    {

    }

    public virtual void OnKeyFromCaller(int num)
    {

    }

    public virtual void OnKeyUnlock()
    {

    }

    public override void ShowMessage(int index, string message)
    {
        base.ShowMessage(index, message);
    }

    public override void ShowMessageExit()
    {
        base.ShowMessageExit();
    }
}
