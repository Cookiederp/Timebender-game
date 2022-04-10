using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnCol : MonoBehaviour
{
    public AudioSource audioSource;
    private Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(rb.velocity.sqrMagnitude > 5)
        audioSource.Play();
    }
}
