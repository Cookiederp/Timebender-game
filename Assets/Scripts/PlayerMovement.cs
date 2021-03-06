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

    private bool IsMoving;
    AudioSource step1;
    public AudioClip[] clips;
    public AudioSource jumpSound;
    public AudioClip jumpClip;

    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        step1 = GetComponent<AudioSource>();
        jumpSound.clip = jumpClip;

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
        move = Vector3.ClampMagnitude(move, 1f);

        character.Move(move * speed * Time.deltaTime);

        
        if (Input.GetAxis("Vertical") > 0)
        {
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }

        if (IsMoving && !step1.isPlaying)
        {
            int randomIndex = Random.Range(0, clips.Length);
            AudioClip randomClip = clips[randomIndex];
            step1.clip = randomClip;
            step1.Play();
        }
        if (!IsMoving)
        {
            step1.Stop();
        }
        if (!isOnGround)
        {
            step1.Stop();
        }
        if (gameManager.isGamePaused)
        {
            step1.Stop();
        }


        if (Input.GetKeyDown("space") && isOnGround)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
            if (!jumpSound.isPlaying)
            {
                jumpSound.Play();
            }
        }

        velocity.y += gravity * Time.deltaTime;

        character.Move(velocity * Time.deltaTime);
    }
}
