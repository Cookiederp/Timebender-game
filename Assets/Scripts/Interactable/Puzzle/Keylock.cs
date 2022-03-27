using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keylock : InteractablePuzzleCaller
{
    public bool isLocked = true;
    public GameObject[] keys;

    //cache
    private InteractablePuzzleReceiver[] receivers_;

    public void Start()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        //cache comps
        receivers_ = new InteractablePuzzleReceiver[receiversObj.Length];
        for (int i = 0; i < receiversObj.Length; i++)
        {
            receivers_[i] = receiversObj[i].GetComponent<InteractablePuzzleReceiver>();
        }
    }

    public override void OnPress(int num)
    {
        if (isLocked)
        {
            ShowMessage(2, "Need a Key.");
        }
    }

    public override void OnRay()
    {
        if (isLocked)
        {
            ShowMessage(2, "Need a Key.");
        }
    }

    public override void OnRayExit()
    {
        ShowMessageExit();
    }

    private void OnTriggerEnter(Collider other)
    {
        bool success = false;
        for(int i = 0; i < keys.Length; i++)
        {
            if (other.gameObject == keys[i])
            {
                //the object that collided is a key
                for (int j = 0; j < receivers_.Length; j++)
                {
                    if (receivers_[j].unlockOnKeyFromCaller)
                    {
                        //for example, door.cs OnKeyUnlock gets called
                        success = true;
                        receivers_[j].OnKeyUnlock();
                    }
                }
            }
        }

        if (success)
        {
            //destroy key, might want to change it later to place the key at a position in keylock.
            ShowMessage(1, "Door Unlocked");
            other.gameObject.SetActive(false);
            gameObject.GetComponent<Collider>().enabled = false;
            isLocked = false;
        }
    }
}
