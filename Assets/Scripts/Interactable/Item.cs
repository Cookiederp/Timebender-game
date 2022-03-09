using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public GameObject prefab;
    public string itemName;
    public int id;

    public void Initialize(ItemObject item)
    {
        prefab = item.itemData.prefab;
        itemName = item.itemData.itemName;
        id = item.itemData.id;
    }

    public Item(ItemObject item)
    {
        prefab = item.itemData.prefab;
        itemName = item.itemData.itemName;
        id = item.itemData.id;
    }
    // https://forum.unity.com/threads/rpg-inventory-scriptableobject-list-or-list-of-scriptableobjects.371686/ 
}

