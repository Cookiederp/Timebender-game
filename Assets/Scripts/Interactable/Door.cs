using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//works both ways (direct player interact or button, lever...), can be one or the other or both, depending on layers and tag chosen.
public class Door : Interactable
{
    public override void OnPress(int n)
    {
        //TEMP, ADD ANIMATION??? 
        gameObject.transform.localScale = new Vector3(2, 2, 2);
    }
}

