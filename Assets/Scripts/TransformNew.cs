using UnityEngine;
//transform needs a gameobject, this does not
public class TransformNew
{

    public Vector3 position;
    public Vector3 eulerRotation;
    public Vector3 localScale;
    public Vector3 localPosition;
    public Vector3 localEulerRotation;

    public TransformNew()
    {

    }

    public TransformNew(Transform transform)
    {
        position = transform.position;
        eulerRotation = transform.eulerAngles;
        localScale = transform.localScale;

        localPosition = transform.localPosition;
        localEulerRotation = transform.localEulerAngles;
        localScale = transform.localScale;
    }
}
