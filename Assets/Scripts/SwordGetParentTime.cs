using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordGetParentTime : MonoBehaviour
{
    public TimeTravelReceiver parentTimeTravelReceiver;
    TimeTravelReceiver timeTravelReceiver;
    TimeTravelManager timeTravelManager;

    void Start()
    {
        timeTravelManager = TimeTravelManager.instance;

        timeTravelReceiver = gameObject.GetComponent<TimeTravelReceiver>();


        timeTravelReceiver.isObjInPresent = parentTimeTravelReceiver.isObjInPresent;
        timeTravelReceiver.isObjInFuture = parentTimeTravelReceiver.isObjInFuture;

        timeTravelReceiver.ReIni(timeTravelManager.isPresent);
    }
}
