using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITimeSelection : MonoBehaviour
{
    private static UITimeSelection _instance;
    public TextMeshProUGUI selectText;

    private string st0;
    private string st1;
    private string st2;

    public static UITimeSelection instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UITimeSelection>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        st0 = "Present -> P+F [E]";
        st1 = "[Q] P <- Present+Future -> F [E]";
        st2 = "[Q] P+F <- Future";
        selectText = gameObject.GetComponent<TextMeshProUGUI>();
        _instance = this;
        gameObject.SetActive(false);
    }

    //p = 0, pf = 1, f = 2
    public void SetText(int index)
    {
        if(index == 0)
        {
            selectText.text = st0;
        }
        else if (index == 1)
        {
            selectText.text = st1;
        }
        else if (index == 2)
        {
            selectText.text = st2;
        }
        else
        {
            //other
        }
    }

    public void SetActive(bool b)
    {
        gameObject.SetActive(b);
    }
}

