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

    public AudioSource audioSource;

    private Sway wandSway;

    private void Awake()
    {
        wandSway = gameObject.transform.GetChild(0).GetComponent<Sway>();
        gameManager = GameManager.instance;
    }

    private void OnEnable()
    {
        gameManager.uiInteractManager.UpdateControlInfoText("LMB - TIME TRAVEL");
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
                    audioSource.Play();
                    wandSway.MoveUp(0.15f, true);
                    presentPostProcessObj.SetActive(false);
                    futurePostProcessObj.SetActive(true);
                    timeTravelManager.OnFuture();
                    GameObject g = Instantiate(travelParticle, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), Quaternion.identity);
                    Destroy(g, 3f);

                }
                else
                {
                    //to present
                    audioSource.Play();
                    wandSway.MoveUp(-0.15f, true);
                    presentPostProcessObj.SetActive(true);
                    futurePostProcessObj.SetActive(false);
                    timeTravelManager.OnPresent();
                    GameObject g = Instantiate(travelParticle, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 2.5f, gameObject.transform.position.z), Quaternion.identity);
                    Destroy(g, 3f);
                }
            }
        }
    }

    private void OnDisable()
    {
        gameManager.uiInteractManager.UpdateControlInfoText("");
    }
}
