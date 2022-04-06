using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreditsMenu : MonoBehaviour
{
    public GameObject mainObj;

    public GameObject creditsMenuObj;

    public GameObject[] pages;

    int index = 0;

    public void OnBack()
    {
        if(index == 0)
        {
            OpenMMenu();
        }
        else if(index == 1)
        {
            pages[index].SetActive(false);
            index--;
            pages[index].SetActive(true);
        }

    }

    public void OnNext()
    {
        if (index == 0)
        {
            pages[index].SetActive(false);
            index++;
            pages[index].SetActive(true);
        }
        else if (index == 1)
        {
            OpenMMenu();
        }
    }

    private void OpenMMenu()
    {
        index = 0;
        for(int i = 0; i<pages.Length; i++)
        {
            pages[i].SetActive(false);
        }

        mainObj.SetActive(true);
        creditsMenuObj.SetActive(false);
    }

    public void OnCredits()
    {
        mainObj.SetActive(false);
        creditsMenuObj.SetActive(true);
        index = 0;
        pages[index].SetActive(true);
    }
}
