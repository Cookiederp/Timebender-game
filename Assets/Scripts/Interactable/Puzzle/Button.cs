using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : InteractablePuzzleCaller
{

    public override void OnPress(int n)
    {
        ShowMessage(0, "Press Button");
        base.OnPress(n);
    }

    public override void OnRay()
    {
        ShowMessage(0, "Press Button");
    }

    public override void OnRayExit()
    {
        ShowMessageExit();
    }
}
