using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTimeTravel : MonoBehaviour
{
    public GameObject presentPostProcessObj;
    public GameObject futurePostProcessObj;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (presentPostProcessObj.activeSelf)
            {
                //to future
                presentPostProcessObj.SetActive(false);
                futurePostProcessObj.SetActive(true);
            }
            else
            {
                //to present
                presentPostProcessObj.SetActive(true);
                futurePostProcessObj.SetActive(false);
            }
        }
    }
}
