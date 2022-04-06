using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        gameObjectR = gameObject.GetComponent<TimeTravelReceiver>();
        animator = gameObject.GetComponent<Animator>();
        animRb = gameObject.GetComponent<Rigidbody>();
        animCollider = gameObject.GetComponent<CapsuleCollider>();
        triggerCollider = gameObject.GetComponent<BoxCollider>();
        ragdollColliders = gameObject.GetComponentsInChildren<Collider>(true);
        ragdollRigids = gameObject.GetComponentsInChildren<Rigidbody>(true);
        DisableRagdoll();
    }

    private void EnableRagdoll()
    {
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
            if (velSum > 5.6f)
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
            else if(velSum > 4f)
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
