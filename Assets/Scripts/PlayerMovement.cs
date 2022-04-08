using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Josh
public class PlayerMovement : MonoBehaviour
{

    public CharacterController character;
    public float speed = 6f;
    Vector3 velocity;
    public float gravity = -19.81f;
    public float jump = 1.5f;

    public Transform onGround;
    public float dist = 0.4f;
    public LayerMask ground;
    bool isOnGround;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isOnGround = Physics.CheckSphere(onGround.position, dist);
        if(isOnGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        character.Move(move * speed * Time.deltaTime);

        if(Input.GetKeyDown("space") && isOnGround)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        character.Move(velocity * Time.deltaTime);
    }
}
