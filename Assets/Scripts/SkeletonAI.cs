using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonAI : MonoBehaviour
{
    private Animator animator;
    private Collider animCollider;
    private Collider triggerCollider;
    private Rigidbody animRb;

    private Rigidbody animRigid;
    private Collider[] ragdollColliders;
    private Rigidbody[] ragdollRigids;

    public GameObject followerFollowThis;
    public GameObject followertimeTravelPrefab;
    private TimeTravelReceiver followerR;
    private MoveRagdollTime followerM;
    private TimeTravelReceiver gameObjectR;

    public GameObject deadParticlePrefab;


    public GameObject swordObj;

    private GameObject player;
    private GameManager gameManager;
    private NavMeshAgent navMesh;
    private float distanceFromPlayer;
    private bool isAttacking = false;
    private bool wasGoingToPlayer = false;

    private float attackRange = 2.65f;
    public float seePlayerRadius = 10f;
    public bool doesPatrol;
    private bool isPatroling = false;
    private bool hasSeenPlayer = false;
    public Transform patrolPointA;
    public Transform patrolPointB;
    public float timeBeforeSwitch;

    public AudioSource boneAudioSource;
    public AudioSource boneHitAudioSource;

    public float dieOnVel = 4f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        player = gameManager.player;
        navMesh = gameObject.GetComponent<NavMeshAgent>();

        swordObj.GetComponent<SwordGetParentTime>().enabled = false;
        swordObj.GetComponent<TimeTravelReceiver>().enabled = false;

        gameObjectR = gameObject.GetComponent<TimeTravelReceiver>();
        animator = gameObject.GetComponent<Animator>();
        animRb = gameObject.GetComponent<Rigidbody>();
        animCollider = gameObject.GetComponent<CapsuleCollider>();
        triggerCollider = gameObject.GetComponent<BoxCollider>();
        ragdollColliders = gameObject.GetComponentsInChildren<Collider>(true);
        ragdollRigids = gameObject.GetComponentsInChildren<Rigidbody>(true);
        DisableRagdoll();
    }

    void Update()
    {
        if (navMesh.enabled)
        {
            distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceFromPlayer < attackRange)
            {
                AttackPlayer();
            }
            else if (distanceFromPlayer < seePlayerRadius || hasSeenPlayer)
            {
                if (!isAttacking)
                {
                    GoToPlayer();
                }
            }
            else
            {
                if (!isPatroling)
                {
                    Patrol();               
                }
                else
                {
                    if(navMesh.velocity.sqrMagnitude == 0f)
                    {
                        animator.Play("idle");
                    }
                    else
                    {
                        animator.Play("walk");
                    }

                }
            }
        }
    }

    private void GoToPlayer()
    {
        if (navMesh.enabled)
        {
            hasSeenPlayer = true;
            isPatroling = false;
            StopAllCoroutines();
            isAttacking = false;
            animator.Play("walk", 0);
            navMesh.SetDestination(player.transform.position);
        }
    }

    private void AttackPlayer()
    {
        if (navMesh.enabled)
        {
            isPatroling = false;
            //not mine
            //https://www.youtube.com/watch?v=xppompv1DBg&t=2s
            Vector3 dir = (player.transform.position - gameObject.transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
            //

            if (isAttacking == false)
            {
                StopCoroutine(Attacking());
                isAttacking = true;
                StartCoroutine(Attacking());
            }
        }
    }

    private void Patrol()
    {
        if (doesPatrol)
        {
            if (navMesh.enabled)
            {
                isPatroling = true;
                StopAllCoroutines();
                StartCoroutine(Patrol(timeBeforeSwitch));
            }
        }
    }

    IEnumerator Attacking()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Strike_1"))
        {
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(0.15f);
        Debug.Log("Attacking");
        if(distanceFromPlayer < attackRange)
        {
            //hurt player
            gameManager.playerHealthManager.RemoveHP(40f);
        }
        yield return new WaitForSeconds(1.75f);
        isAttacking = false;
        yield return null;
    }

    IEnumerator Patrol(float time)
    {
        while (true)
        {
            navMesh.SetDestination(patrolPointA.position);
            yield return new WaitForSeconds(time);
            navMesh.SetDestination(patrolPointB.position);
            yield return new WaitForSeconds(time);
        }
    }


    private void EnableRagdoll()
    {
        swordObj.GetComponent<SwordGetParentTime>().enabled = true;
        swordObj.GetComponent<TimeTravelReceiver>().enabled = true;

        boneAudioSource.loop = false;
        boneAudioSource.playOnAwake = false;
        boneAudioSource.Stop();
        boneHitAudioSource.Play();

        StopAllCoroutines();
        GameObject particleDead = Instantiate(deadParticlePrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+1f, gameObject.transform.position.z), Quaternion.identity);
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = true;
        }

        foreach (Rigidbody rb in ragdollRigids)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
        //for moving ragdoll to present/future
        GameObject follower = Instantiate(followertimeTravelPrefab, gameObject.transform.position, Quaternion.identity);
        followerM = follower.GetComponent<MoveRagdollTime>();
        followerM.followThis = followerFollowThis;
        followerM.parent = gameObject;
        //

        animRb.useGravity = false;
        animRb.isKinematic = true;
        animator.enabled = false;
        animCollider.enabled = false;
        triggerCollider.enabled = false;
        swordObj.transform.parent = null;
        navMesh.enabled = false;
        Destroy(particleDead, 10f);
    }

    private void DisableRagdoll()
    {
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = false;
        }

        foreach (Rigidbody rb in ragdollRigids)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        }

        navMesh.enabled = true;
        animator.enabled = true;
        animCollider.enabled = true;
        triggerCollider.enabled = true;
    }


    private void OnTriggerEnter(Collider other)
    {   
        Rigidbody otherRb = other.attachedRigidbody;

        if(otherRb == null)
        {
            return;
        }
        float otherMass = otherRb.mass;
        //ADD IF COLLISION IS NOT STRONG ENOUGHT, DONT DO THE STUFF BELOW
        if (!other.CompareTag("Player"))
        {
            float velSum = otherRb.velocity.magnitude;
            if (velSum > dieOnVel)
            {
                if(otherMass > 0.2f)
                {
                    EnableRagdoll();

                    float knockInPowerProp = Mathf.Clamp(velSum / 2, 3f, 6f);
                    float knockbackPower = Mathf.Clamp(velSum / 2, 3f, 8f);
                    Debug.Log(velSum);
                    foreach (Rigidbody rb in ragdollRigids)
                    {
                        rb.velocity = (rb.transform.position - otherRb.transform.position) * knockbackPower;
                    }
                }
            }
            else if(velSum > dieOnVel)
            {
                if (other.CompareTag("Sword"))
                {
                    EnableRagdoll();

                    float knockInPowerProp = Mathf.Clamp(velSum / 2, 3f, 6f);
                    float knockbackPower = Mathf.Clamp(velSum / 1.5f, 5f, 10f);
                    Debug.Log(velSum);
                    foreach (Rigidbody rb in ragdollRigids)
                    {
                        rb.velocity = (rb.transform.position - otherRb.transform.position) * knockbackPower;
                    }
                }
            }
        }

        //otherRb.velocity =  new Vector3(gameObject.transform.position.x - otherRb.transform.position.x, otherRb.velocity.y / knockInPowerProp, gameObject.transform.position.z - otherRb.transform.position.z) * knockInPowerProp;

    }
}
