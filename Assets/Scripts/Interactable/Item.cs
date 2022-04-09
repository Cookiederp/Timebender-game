using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public GameObject prefab;
    public int id;
    public int noteId;
    public string itemName;
    public bool isToTake;

    public void Initialize(ItemObject item)
    {
        prefab = item.itemData.prefab;
        itemName = item.itemData.itemName;
        id = item.itemData.id;
        isToTake = item.itemData.isToTake;
        noteId = item.itemData.noteId;
    }

    public Item(ItemObject item)
    {
        prefab = item.itemData.prefab;
        itemName = item.itemData.itemName;
        id = item.itemData.id;
    }
    // https://forum.unity.com/threads/rpg-inventory-scriptableobject-list-or-list-of-scriptableobjects.371686/ 
}

