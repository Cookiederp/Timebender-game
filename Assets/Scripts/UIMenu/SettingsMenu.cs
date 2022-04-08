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
    public Slider soundSlider;
    public TextMeshProUGUI soundPlaceholder;
    public TextMeshProUGUI mouseSensPlaceholder;

    private float mouseSens = 0f;
    private float defSens = 1f;

    private float soundsValue = 0f;
    private float defSound = 0.5f;

    void Start()
    {
        loadScene = gameObject.GetComponent<LoadScene>();
        SettingsMenuObj.SetActive(false);
    }

    public void OnSettings()
    {
        MainObj.SetActive(false);

        float msens = PlayerPrefs.GetFloat("mouseSensitivity", 0.5f);
        mouseSensSlider.value = msens;
        mouseSensPlaceholder.text = msens.ToString();

        float sounds = PlayerPrefs.GetFloat("sound", 0.5f);
        soundSlider.value = sounds;
        soundPlaceholder.text = sounds.ToString();

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

        PlayerPrefs.SetFloat("sound", soundsValue);
        PlayerPrefs.Save();
        AudioListener.volume = PlayerPrefs.GetFloat("sound");
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


    public void OnSoundChange()
    {
        soundsValue = Mathf.Round(soundSlider.value * 100f) / 100f;
        if (soundsValue % 1 == 0)
        {
            soundPlaceholder.text = soundsValue.ToString() + ".00";
        }
        else
        {
            soundPlaceholder.text = soundsValue.ToString();
        }
    }
}
