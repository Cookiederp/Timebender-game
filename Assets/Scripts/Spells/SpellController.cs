using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    public GameObject spMovePropsObj;
    public GameObject spTimeTravelsObj;
    public GameObject spRegenObj;

    private GameObject currentSelected = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectSpell(spMovePropsObj);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSpell(spTimeTravelsObj);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectSpell(spRegenObj);
        }
    }

    public void SelectSpell(GameObject spellObj)
    {
        if (currentSelected == null)
        {
            currentSelected = spellObj;
            currentSelected.SetActive(true);
        }
        else if (currentSelected != spellObj)
        {
            currentSelected.SetActive(false);
            currentSelected = spellObj;
            currentSelected.SetActive(true);
        }
        else
        {

        }
    }
}

