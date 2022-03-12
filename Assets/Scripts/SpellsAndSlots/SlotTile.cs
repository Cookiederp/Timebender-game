using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//go on a slot
public class SlotTile : MonoBehaviour
{
    public GameObject spellInSlot;

    private Image slotOutlineImage;
    private float slotOutlineDefAlpha;

    private Image cooldownImage;

    void Start()
    {
        cooldownImage = gameObject.transform.GetChild(0).GetComponentInChildren<Image>();
        slotOutlineImage = gameObject.GetComponent<Image>();
        slotOutlineDefAlpha = slotOutlineImage.color.a;
    }

    public void OnSelect()
    {
        spellInSlot.SetActive(true);

        Color slotOutlineColor = slotOutlineImage.color;
        slotOutlineColor.a = 1f;
        slotOutlineImage.color = slotOutlineColor;
    }

    public void OnDeselect()
    {
        spellInSlot.SetActive(false);

        Color slotOutlineColor = slotOutlineImage.color;
        slotOutlineColor.a = slotOutlineDefAlpha;
        slotOutlineImage.color = slotOutlineColor;
    }
}
