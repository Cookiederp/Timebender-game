using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SelectablePlateController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isCorrectPlate;
    public int index;
    public OnTriggerRagdoll OnTriggerRagdoll;
    public GlyphPuzzleController controller;
    public Light SpotLight;
    private bool completed;

    void Start()
    {
        completed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OnTriggerRagdoll.isOn && !completed)
        {
            if (isCorrectPlate)
            {
                SpotLight.color = Color.green;
                controller.CompletedPlate(index);
                completed = true;
                //Good
            }
            else
            {
                SpotLight.color = Color.red;
                StartCoroutine(WrongPlate());
            }
        }
        else if(!completed)
        {
            SpotLight.color = Color.clear;
        }
    }

    IEnumerator WrongPlate()
    {
        yield return new WaitForSeconds(1f);
        controller.KillPlayer();
        yield return null;
    }
}
