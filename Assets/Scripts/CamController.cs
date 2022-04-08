using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Josh
public class CamController : MonoBehaviour
{

    public float mouseSens = 1f;
    public float yOffset;
    private float pSens;

    public Transform player;

    private GameManager gameManager;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        pSens = PlayerPrefs.GetFloat("mouseSensitivity", 0.5f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Max, GameManager calls this
    public void UpdateMouseSens()
    {
        pSens = PlayerPrefs.GetFloat("mouseSensitivity");
    }
    //

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGamePaused)
        {
            //getting input from cursor movement in x and y axis
            float mouseX = Input.GetAxis("Mouse X") * mouseSens * pSens;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSens * pSens;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            //looking up and down
            transform.rotation = Quaternion.Euler(xRotation, player.eulerAngles.y, 0f);
            transform.position = new Vector3(player.position.x, player.position.y + yOffset, player.position.z);
            //looking left and right
            player.Rotate(Vector3.up * mouseX);
        }
    }
}
