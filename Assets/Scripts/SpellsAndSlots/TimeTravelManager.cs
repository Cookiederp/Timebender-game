using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelManager : MonoBehaviour
{
    private List<TimeTravelReceiver> receivers;

    private static TimeTravelManager _instance;
    [HideInInspector]
    public bool isPresent;
    public static TimeTravelManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TimeTravelManager>();
            }

            return _instance;
        }
    }

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
        isPresent = true;
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
        isPresent = false;
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

    public void addToList(TimeTravelReceiver thisReceiver)
    {
        receivers.Add(thisReceiver);
    }
}
