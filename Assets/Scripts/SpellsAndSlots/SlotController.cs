using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    public GameObject hotbar;
    private int slotAmount = 4;
    private SlotTile[] slots;
    private int pastIndexSelected = -1;

    private void Start()
    {
        slots = new SlotTile[slotAmount];
        for(int i = 0; i< slotAmount; i++)
        {
            slots[i] = hotbar.transform.GetChild(i).GetComponent<SlotTile>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectSpell(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSpell(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectSpell(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectSpell(3);
        }
    }

    public void SelectSpell(int slotIndex)
    {
        if (pastIndexSelected == -1)
        {
            //no slot selected before, select only
            pastIndexSelected = slotIndex;
            slots[slotIndex].OnSelect();
        }
        else if (pastIndexSelected != slotIndex)
        {
            //a slot was selected before, deselect, then select new one
            slots[pastIndexSelected].OnDeselect();
            pastIndexSelected = slotIndex;
            slots[slotIndex].OnSelect();
        }
        else
        {
            if (pastIndexSelected == slotIndex)
            {
                //slot selected before is selected again, deselect it
                pastIndexSelected = -1;
                slots[slotIndex].OnDeselect();
            }
        }
    }
}

