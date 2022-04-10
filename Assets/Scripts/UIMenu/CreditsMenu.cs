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
        pages[index].SetActive(false);
        index--;
        if (index < 0)
        {
            OpenMMenu();
        }
        else
        {
            pages[index].SetActive(true);
        }

    }

    public void OnNext()
    {
        pages[index].SetActive(false);
        index++;
        if (index == pages.Length)
        {
            OpenMMenu();
        }
        else
        {
            pages[index].SetActive(true);
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
