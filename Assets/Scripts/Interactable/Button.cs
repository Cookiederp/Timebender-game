using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : InteractablePuzzleCaller
{

    public override void OnPress(int n)
    {
        base.OnPress(n);
    }

    public override void OnRay()
    {
        ShowMessage(1, "Press Button");
    }

    public override void OnRayExit()
    {
        ShowMessageExit();
    }
}
