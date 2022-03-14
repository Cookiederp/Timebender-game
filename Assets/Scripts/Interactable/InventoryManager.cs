using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    //add item to list, remove from game world
    public void TakeItem(Transform thisItemObj)
    {
        ItemObject itemObj = thisItemObj.gameObject.GetComponent<ItemObject>();
        if (!itemObj.IsItemTaken())
        {
            //add item to inventory list
            Item item = ScriptableObject.CreateInstance<Item>();
            item.Initialize(itemObj);
            items.Add(item);

            itemObj.OnPress(1);
        }
    }

    //TEMP??
    public void DropItem(GameObject droppedItem)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            Vector3 dropLocation = hit.point;
            Quaternion rotationOfObjHit = hit.transform.rotation;

            //this ensures when an object is dropped, half of the object is not under the mesh.
            float offset;
            offset = droppedItem.GetComponent<BoxCollider>().size.y / 2;
            dropLocation.y += offset;

            //if surface is flat, y rotation random
            if (rotationOfObjHit.eulerAngles.x == 0 && rotationOfObjHit.eulerAngles.z == 0)
            {
                rotationOfObjHit = Quaternion.Euler(rotationOfObjHit.eulerAngles.x, Random.Range(0, 360), rotationOfObjHit.eulerAngles.z);
            }

            Instantiate(items[0].prefab, dropLocation, rotationOfObjHit);
        }
    }

    //TEMP?? will change, probably remove, or if not removed, will use rigidbody instead of ray.
    public void DropItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            Vector3 dropLocation = hit.point;
            Quaternion rotationOfObjHit = hit.transform.rotation;

            //this ensures when an object is dropped, half of the object is not under the mesh.
            float offset;
            offset = items[0].prefab.GetComponent<BoxCollider>().size.y / 2;
            dropLocation.y += offset;

            //if surface is flat, y rotation random
            if (rotationOfObjHit.eulerAngles.x == 0 && rotationOfObjHit.eulerAngles.z == 0)
            {
                rotationOfObjHit = Quaternion.Euler(rotationOfObjHit.eulerAngles.x, Random.Range(0, 360), rotationOfObjHit.eulerAngles.z);
            }

            Instantiate(items[0].prefab, dropLocation, rotationOfObjHit);
        }
    }
}
