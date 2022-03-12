using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMoveProps : MonoBehaviour
{
    int layerMaskMoveableProp = 1 << 10;
    private Camera camera;

    private float defaultRange;
    private float minRange;
    private float maxRange;
    private float breakDist;
    private float rayRange;
    private float throwForce;

    public Transform holdLocation;
    private GameObject selectedProp;
    private Rigidbody selectedPropRb;

    private void Start()
    {
        minRange = 2.3f;
        maxRange = 4.1f;
        breakDist = 6f;
        rayRange = 4.5f;
        throwForce = 25f;
        camera = Camera.main;
        defaultRange = holdLocation.localPosition.z;
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        //control selected prop, move it away or closer to player
        if (Input.mouseScrollDelta.y != 0)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                if (!(holdLocation.localPosition.z >= maxRange))
                {
                    holdLocation.localPosition = new Vector3(holdLocation.localPosition.x, holdLocation.localPosition.y, holdLocation.localPosition.z + (Input.mouseScrollDelta.y * 0.1f));
                }
            }
            else
            {
                if (!(holdLocation.localPosition.z <= minRange))
                {
                    holdLocation.localPosition = new Vector3(holdLocation.localPosition.x, holdLocation.localPosition.y, holdLocation.localPosition.z + (Input.mouseScrollDelta.y * 0.1f));
                }
            }



        }

        //move prop
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit, rayRange, layerMaskMoveableProp))
            {
                //case where player press input, selected prop stops being selected
                if (selectedProp == hit.transform.gameObject)
                {
                    DropSelectedProp();
                }
                else
                {
                    if (selectedProp != null)
                    {
                        DropSelectedProp();
                    }
                    //case where player press input, select hit prop, cache
                    //select prop, if in future only, make it present also now.
                    selectedProp = hit.transform.gameObject;
                    selectedPropRb = hit.rigidbody;
                    selectedPropRb.interpolation = RigidbodyInterpolation.Interpolate;
                    selectedPropRb.useGravity = false;
                }
            }
            else
            {
                //case where player press input, no layerMaskMoveableProp rayhit, selected prop stops being selected
                if (selectedProp != null)
                {
                    DropSelectedProp();
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            //throw
            if (selectedProp != null)
            {
                if (Physics.Raycast(ray, out hit, rayRange, layerMaskMoveableProp))
                {
                    selectedPropRb.AddForce(ray.direction * throwForce, ForceMode.Impulse);
                    DropSelectedProp();
                }
            }
            //bring to other time, might add later, change it long range instead of with 1 and 2, or maybe will be another spell
            /*
            else
            {
                if (Physics.Raycast(ray, out hit, rayRange+??, layerMaskMoveableProp))
                {
                    hit.transform.gameObject.GetComponent<TimeTravelReceiver>().TakeToOther();
                }
            }
            */
        }

    }


    //selected object follow holdLocation
    void FixedUpdate()
    {
        if (selectedProp != null)
        {

            Vector3 dir = holdLocation.position - selectedProp.transform.position;
            float dist = Vector3.Distance(holdLocation.position, selectedProp.transform.position);
            if (dist > breakDist)
            {
                DropSelectedProp();
            }
            else
            {
                dir *= dist;
                selectedPropRb.AddForce(dir, ForceMode.VelocityChange);
                if (Input.GetKey(KeyCode.W))
                {
                    selectedPropRb.velocity *= Mathf.Clamp(dist, 0.25f, 0.8f);
                }
                else
                {
                    selectedPropRb.velocity *= Mathf.Clamp(dist, 0.25f, 0.6f);
                }
            }
        }
    }
    //

    private void DropSelectedProp()
    {
        selectedPropRb.interpolation = RigidbodyInterpolation.None;
        selectedPropRb.useGravity = true;
        selectedProp = null;
        selectedPropRb = null;
        holdLocation.localPosition = new Vector3(0, 0, defaultRange);
    }


    private void OnDisable()
    {
        if(selectedProp != null)
        {
            DropSelectedProp();
        }
    }
}
