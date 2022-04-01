using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    private Animator animator;
    private Collider animCollider;
    private Rigidbody animRb;

    private Rigidbody animRigid;
    private Collider[] ragdollColliders;
    private Rigidbody[] ragdollRigids;

    public GameObject followerFollowThis;
    public GameObject followertimeTravelPrefab;
    private TimeTravelReceiver followerR;
    private MoveRagdollTime followerM;
    private TimeTravelReceiver gameObjectR;


    public GameObject swordObj;
    // Start is called before the first frame update
    void Start()
    {
        gameObjectR = gameObject.GetComponent<TimeTravelReceiver>();
        animator = gameObject.GetComponent<Animator>();
        animRb = gameObject.GetComponent<Rigidbody>();
        animCollider = gameObject.GetComponent<CapsuleCollider>();
        ragdollColliders = gameObject.GetComponentsInChildren<Collider>(true);
        ragdollRigids = gameObject.GetComponentsInChildren<Rigidbody>(true);
        DisableRagdoll();
    }

    private void EnableRagdoll()
    {
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = true;
        }

        foreach (Rigidbody rb in ragdollRigids)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
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
        swordObj.transform.parent = null;
        //colliderForTimeTravel.gameObject.layer = 12;

        //StartCoroutine(BoxFollow());
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
        }

        animator.enabled = true;
        animCollider.enabled = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        //ADD IF COLLISION IS NOT STRONG ENOUGHT, DONT DO THE STUFF BELOW
        EnableRagdoll();
        float velSum = collision.rigidbody.velocity.x + collision.rigidbody.velocity.y + collision.rigidbody.velocity.z;
        float knockInPowerProp = Mathf.Clamp(velSum, 3f, 7f);
        float knockbackPower = Mathf.Clamp(velSum, 3f, 12f);
        Debug.Log(knockInPowerProp);
        foreach (Rigidbody rb in ragdollRigids)
        {
            rb.velocity = (rb.transform.position - collision.transform.position) * knockbackPower;
        }
        collision.rigidbody.velocity =  new Vector3(gameObject.transform.position.x - collision.transform.position.x, collision.rigidbody.velocity.y / knockInPowerProp, gameObject.transform.position.z - collision.transform.position.z) * knockInPowerProp;

    }

    IEnumerator BoxFollow()
    {
        //while (true)
        //{
        //    yield return new WaitForSecondsRealtime(0.066f);
        //    colliderForTimeTravel.transform.position = followThis.transform.position;
       // }

        // Debug.Log(collision.rigidbody.velocity);
        // collision.rigidbody.
        // collision.rigidbody.velocity = new Vector3(5, 0, 0);

        yield return null;
    }
}
