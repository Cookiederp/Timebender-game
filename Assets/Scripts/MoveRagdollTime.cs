using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//follow a ragdoll, makes it so ragdoll can be moved between present/future
public class MoveRagdollTime : Interactable
{
    [HideInInspector]
    public GameObject parent;
    [HideInInspector]
    public GameObject followThis;

    private TimeTravelReceiver pR;
    private TimeTravelReceiver cR;
    private TimeTravelManager timeTravelManager;

    private void Start()
    {
        pR = parent.GetComponent<TimeTravelReceiver>();
        cR = gameObject.GetComponent<TimeTravelReceiver>();

        timeTravelManager = TimeTravelManager.instance;
        timeTravelManager.addToList(cR);

        cR.isObjInFuture = pR.isObjInFuture;
        cR.isObjInPresent = pR.isObjInPresent;

        cR.ReIni(timeTravelManager.isPresent);
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Follow());
    }

    public override void OnPress(int num)
    {
        pR.OnPress(num);
        cR.OnPress(num);
    }

    IEnumerator Follow()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            gameObject.transform.position = followThis.transform.position;
            gameObject.transform.rotation = followThis.transform.rotation;
        }
    }
}
