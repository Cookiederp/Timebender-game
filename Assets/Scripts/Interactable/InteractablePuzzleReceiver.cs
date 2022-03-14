using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePuzzleReceiver : InteractablePuzzle
{

    //onpress from switch (button, lever..)
    public virtual void OnPressFromSwitch(int num)
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
