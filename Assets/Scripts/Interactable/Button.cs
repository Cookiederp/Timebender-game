using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : InteractablePuzzle
{
    public override void OnPress(int n)
    {
        Debug.Log("yes");
        base.OnPress(n);
    }
}
