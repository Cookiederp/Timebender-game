using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{

    public AudioSource propSound;
    public AudioClip propClip;

    // Start is called before the first frame update
    void Start()
    {
        propSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisonEnter(Collision collision)
    {
        Debug.Log("adsfadsf");
        //propSound.clip = propClip;
        //propSound.Play();
        propSound.PlayOneShot(propClip);

    }
}
