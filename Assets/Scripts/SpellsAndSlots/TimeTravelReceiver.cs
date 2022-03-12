using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelReceiver : MonoBehaviour
{
    public bool isInPresent = true;
    public bool isInFuture = true;


    private TransformNew presentTransform;
    private Rigidbody rb;
    Vector3 presentVelocity;
    Vector3 presentAngularVelocity;

    public void Awake()
    {
        presentTransform = new TransformNew(gameObject.transform);
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void OnPresent()
    {
        if (isInPresent)
        {
            gameObject.SetActive(true);
            if (isInFuture)
            {
                if(rb != null)
                {
                    rb.velocity = presentVelocity;
                    rb.angularVelocity = presentAngularVelocity;
                    gameObject.transform.position = presentTransform.position;
                    gameObject.transform.eulerAngles = presentTransform.eulerRotation;
                    gameObject.transform.localScale = presentTransform.localScale;
                }
            }

        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OnFuture()
    {
        if (isInFuture)
        {
            gameObject.SetActive(true);
            if (isInPresent)
            {

                presentVelocity = rb.velocity;
                presentAngularVelocity = rb.angularVelocity;
                presentTransform = new TransformNew(gameObject.transform);
                //StartCoroutine(s());
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    /*
    IEnumerator s()
    {

        yield return new WaitForEndOfFrame();
        Physics.autoSimulation = false;
        for (int i = 0; i < 60; i++)
        {
            Physics.Simulate(Time.deltaTime);
        }
        Physics.autoSimulation = true;
        yield return null;
    }
    */
}