using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerRagdoll : InteractablePuzzleCaller
{
    public bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void UpdateMessage()
    {
        /*
        if (isOn)
        {
            ShowMessage(0, "...");
        }
        else
        {
            ShowMessage(0, "Needs something...");
        }
        */
    }

    private void TurnOn()
    {
        isOn = true;
        UpdateMessage();
        base.OnPress(1);
    }

    private void TurnOff()
    {
        isOn = false;
        UpdateMessage();
        base.OnPress(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        //12 = ragdoll layer
        if(other.gameObject.layer == 12)
        {
            TurnOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 12)
        {
            TurnOff();
        }
    }

    private void OnEnable()
    {
        TurnOff();
    }
}
