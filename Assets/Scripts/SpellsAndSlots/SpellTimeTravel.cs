using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTimeTravel : MonoBehaviour
{
    public GameObject presentPostProcessObj;
    public GameObject futurePostProcessObj;
    public GameObject travelParticle;

    public TimeTravelManager timeTravelManager;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    void Update()
    {
        if (!gameManager.isGamePaused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (presentPostProcessObj.activeSelf)
                {
                    //to future
                    presentPostProcessObj.SetActive(false);
                    futurePostProcessObj.SetActive(true);
                    timeTravelManager.OnFuture();
                    GameObject g = Instantiate(travelParticle, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), Quaternion.identity);
                    Destroy(g, 3f);

                }
                else
                {
                    //to present
                    presentPostProcessObj.SetActive(true);
                    futurePostProcessObj.SetActive(false);
                    timeTravelManager.OnPresent();
                    GameObject g = Instantiate(travelParticle, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), Quaternion.identity);
                    Destroy(g, 3f);
                }
            }
        }
    }
}
