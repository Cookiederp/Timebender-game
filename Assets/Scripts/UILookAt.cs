using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAt : MonoBehaviour
{
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }
    void LateUpdate()
    {
        transform.forward = camera.transform.forward;
    }
}
