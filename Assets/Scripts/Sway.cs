using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{

    Quaternion defRot;
    Vector3 defPos;
    public float intensityRotation;
    public float smoothRotation;

    public float intensityPosition;
    public float smoothPosition;

    private CharacterController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<CharacterController>();
        defRot = gameObject.transform.localRotation;
        defPos = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 xMove = new Vector3(-intensityPosition * horizontalInput, 0, 0);
        Vector3 yMove = new Vector3(0, playerController.velocity.y * -intensityPosition * 0.5f, -intensityPosition * verticalInput);

        Vector3 target_position = defPos + xMove + yMove;

        transform.localPosition = Vector3.Lerp(transform.localPosition, target_position, Time.deltaTime * smoothPosition);


        //base, put in credits,
        //https://www.youtube.com/watch?v=nlcIz-czKyI
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Quaternion xRot = Quaternion.AngleAxis(intensityRotation * mouseX, Vector3.up);
        Quaternion yRot = Quaternion.AngleAxis(-intensityRotation * mouseY, Vector3.right);
        Quaternion target_rotation = defRot * xRot * yRot;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * smoothRotation);
        //
    }
}
