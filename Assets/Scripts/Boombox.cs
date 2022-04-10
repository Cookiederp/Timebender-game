using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boombox : MonoBehaviour
{
    private Sway boomboxSway;
    private bool isOn = false;
    private int index = 0;
    private float size;

    public AudioClip[] musics;
    private AudioSource audioSource;

    private float volume = 0.5f;
    private float minVolume = 0.05f;
    private float maxVolume = 1f;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameManager.instance;
        size = musics.Length;
        audioSource = Camera.main.GetComponent<AudioSource>();
        boomboxSway = gameObject.transform.GetChild(0).GetComponent<Sway>();
        audioSource.volume = volume;
    }

    private void OnEnable()
    {
        gameManager.uiInteractManager.UpdateControlInfoText("LMB - ON OFF / RMB - NEXT / SCROLL - VOLUME");
    }

    private void OnDisable()
    {
        gameManager.uiInteractManager.UpdateControlInfoText("");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGamePaused)
        {
            //turn off/on
            if (Input.GetMouseButtonDown(0))
            {
                if (isOn)
                {
                    isOn = false;
                    audioSource.Stop();
                }
                else
                {
                    isOn = true;
                    audioSource.clip = musics[index];
                    audioSource.Play();
                }
                boomboxSway.MoveForward(0.25f);
            }

            //change music
            if (Input.GetMouseButtonDown(1))
            {
                if (index < size - 1)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }

                boomboxSway.MoveForward(-0.25f);
                audioSource.clip = musics[index];
                if (isOn)
                {
                    audioSource.Play();
                }
            }


            if (Input.mouseScrollDelta.y != 0)
            {
                if (Input.mouseScrollDelta.y > 0)
                {
                    if (!(volume >= maxVolume))
                    {
                        boomboxSway.MoveForward(0.15f);
                        volume += 0.025f;
                        audioSource.volume = volume;
                    }
                }
                else
                {
                    if (!(volume <= minVolume))
                    {
                        boomboxSway.MoveForward(-0.15f);
                        volume -= 0.025f;
                        audioSource.volume = volume;
                    }
                }

            }
        }
    }
}
