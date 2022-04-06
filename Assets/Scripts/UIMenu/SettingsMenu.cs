using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{

    LoadScene loadScene;
    public GameObject MainObj;

    public GameObject SettingsMenuObj;
    public Slider mouseSensSlider;
    public TextMeshProUGUI mouseSensPlaceholder;

    private float mouseSens = 0f;
    private float defSens = 1f;

    void Start()
    {
        loadScene = gameObject.GetComponent<LoadScene>();
        SettingsMenuObj.SetActive(false);
    }

    public void OnSettings()
    {
        MainObj.SetActive(false);

        float msens = PlayerPrefs.GetFloat("mouseSensitivity");
        if(msens > 0)
        {
            mouseSensSlider.value = msens;
            mouseSensPlaceholder.text = msens.ToString();
        }
        else
        {
            mouseSensSlider.value = defSens;
            mouseSensPlaceholder.text = defSens.ToString();
        }


        SettingsMenuObj.SetActive(true);
    }

    public void OnBack()
    {
        MainObj.SetActive(true);
        SettingsMenuObj.SetActive(false);
        if (mouseSens > 0)
        {
            PlayerPrefs.SetFloat("mouseSensitivity", mouseSens);
            PlayerPrefs.Save();
        }
    }


    public void OnMouseSensitivityChange()
    {
        mouseSens = Mathf.Round(mouseSensSlider.value * 100f) / 100f;
        if (mouseSens % 1 == 0)
        {
            mouseSensPlaceholder.text = mouseSens.ToString() + ".00";
        }
        else
        {
            mouseSensPlaceholder.text = mouseSens.ToString();
        }
    }
}
