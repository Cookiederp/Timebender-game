using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelManager : MonoBehaviour
{
    private List<TimeTravelReceiver> receivers;
    

    void Awake()
    {
        receivers = new List<TimeTravelReceiver>();
        TimeTravelReceiver[] temp = FindObjectsOfType<TimeTravelReceiver>();
        foreach (TimeTravelReceiver r in temp)
        {
            receivers.Add(r);
        }
        OnPresent();
    }

    public void OnPresent()
    {
        for(int i = 0; i<receivers.Count; i++)
        {
            if (receivers[i] == null)
            {
                receivers.RemoveAt(i);
                i--;
            }
            else
            {
                receivers[i].OnPresent();
            }
        }
    }

    public void OnFuture()
    {
        for (int i = 0; i < receivers.Count; i++)
        {
            if (receivers[i] == null)
            {
                receivers.RemoveAt(i);
                i--;
            }
            else
            {
                receivers[i].OnFuture();
            }
        }
    }
}
