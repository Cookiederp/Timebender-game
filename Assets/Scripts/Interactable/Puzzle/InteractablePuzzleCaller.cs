using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePuzzleCaller : InteractablePuzzle
{
    //targets needs to contain a script that references to InteractableReceiver, so it can ref Interactable and get called with GetComp<Interactable>
    public GameObject[] receiversObj;

    //cache
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

    //player press caller, tell receivers
    public override void OnPress(int num)
    {
        //num = 0 = off signal from switch
        //num = 1 = on signal from switch
        for (int i = 0; i < receiversObj.Length; i++)
        {

            if (receivers[i].PressOnPressFromCaller)
            {
                receivers[i].OnPressFromSwitch(num);
            }

            if (num > 0)
            {
                if (receivers[i].unlockOnPressFromCaller)
                {
                    receivers[i].OnKeyUnlock();
                }
            }

            if (num > 0)
            {
                if (receivers[i].destroyOnPressFromCaller)
                {
                    receiversObj[i].SetActive(false);
                }
            }
        }
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
