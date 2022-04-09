using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public TextMeshProUGUI hpValueText;
    public Image hpValueImage;

    private Coroutine lcoroutine;

    public Image playerHurtMaskImage;
    private float maxAlpha = 0.25f;
    private float alphaIntensity = 0.1f;
    private float regenTime = 10f;

    bool canRegen = true;

    private float hp = 100;


    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        //TEMP, NEED TO ADD DAMAGE TAKEN FROM SKELETON HERE INSTEAD
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            hp -= 25;
            if(lcoroutine != null)
            {
                StopCoroutine(lcoroutine);
            }
            lcoroutine = StartCoroutine(HitInPast());
            UpdateUI();
        }
    }

    public void PotionConsumed()
    {
        hp += 25;
        if(hp > 100)
        {
            hp = 100;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if(hp <= 0)
        {
            //dead
            hpValueImage.fillAmount = 0;
            hpValueText.text = "0";
            PlayerDead();
        }
        else
        {
            //not dead
            hpValueImage.fillAmount = hp / 100;
            hpValueText.text = hp.ToString();
            playerHurtMaskImage.color = new Color(playerHurtMaskImage.color.r, playerHurtMaskImage.color.g, playerHurtMaskImage.color.b, Mathf.Clamp(alphaIntensity / (hp / 100) - alphaIntensity, 0, maxAlpha));
        }
    }

    public float GetHP()
    {
        return hp;
    }

    public void RemoveHP(float hp_)
    {
        hp -= hp_;
        UpdateUI();
    }

    //call GameManager, say player is dead, play anim, respawn
    private void PlayerDead()
    {
        //temp
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator RegenHp()
    {
        while (true)
        {
            if (canRegen)
            {
                yield return new WaitForSeconds(0.2f);
                if (hp < 100)
                {
                    hp++;
                    UpdateUI();
                }
                else
                {
                    yield return null;
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator HitInPast()
    {
        canRegen = false;
        yield return new WaitForSeconds(regenTime);
        canRegen = true;
        StartCoroutine(RegenHp());
        yield return null;
    }
}
