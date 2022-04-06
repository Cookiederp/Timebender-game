using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : InteractablePuzzleReceiver
{
    public bool isOn = false;
    public GameObject[] receiversObj;

    private InteractablePuzzleReceiver[] receivers;


    private bool ini = false;

    private void OnEnable()
    {
        //cache comps
        if (!ini)
        {
            receivers = new InteractablePuzzleReceiver[receiversObj.Length];
            for (int i = 0; i < receiversObj.Length; i++)
            {
                receivers[i] = receiversObj[i].GetComponent<InteractablePuzzleReceiver>();
            }
            ini = true;
        }
    }

    void Start()
    {
        if (isOn)
        {
            Debug.Log("On");
        }
        else
        {
            Debug.Log("Off");
        }
    }

    public override void OnRay()
    {
        
    }

    public override void OnRayExit()
    {

    }

    public override void OnPress(int num)
    {
        
    }

    public override void OnPressFromSwitch(int num)
    {
        if (isOn)
        {
            Debug.Log("Off");
            isOn = false;

            foreach(InteractablePuzzleReceiver receiver in receivers)
            {
                receiver.OnPressFromSwitch(0);
            }
        }
        else
        {
            Debug.Log("On");
            isOn = true;

            foreach (InteractablePuzzleReceiver receiver in receivers)
            {
                receiver.OnPressFromSwitch(1);
            }
        }
    }
}
