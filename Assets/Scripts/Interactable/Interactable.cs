using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Skeleton class, so scripts can reach subclass from one GetComponent<Interactable>()
public class Interactable : MonoBehaviour
{

    public virtual void OnRay()
    {

    }

    public virtual void OnRayExit()
    {

    }

    public virtual void OnPress(int num)
    {
    }


}