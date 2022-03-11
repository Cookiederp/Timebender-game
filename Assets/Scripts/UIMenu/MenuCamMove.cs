using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamMove : MonoBehaviour
{
    private Camera camera;

    void Start()
    {
        camera = gameObject.GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        camera.transform.eulerAngles = new Vector3(-ray.direction.y * 0.5f, ray.direction.x * 0.5f, 0f);
    }
}
