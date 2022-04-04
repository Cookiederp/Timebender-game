using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHand : MonoBehaviour
{

    private GameManager gameManager;
    private GameObject potionObj;
    // Start is called before the first frame update
    void Awake()
    {
        potionObj = gameObject.transform.GetChild(0).gameObject;
        gameManager = GameManager.instance;
    }

    private void OnEnable()
    {
        HavePotionInHand();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(gameManager.playerHealthManager.GetHP() < 100)
            {
                if(gameManager.inventoryManager.GetPotionAmount() > 0)
                {
                    //drink potion, play anim (green particles out of the potion, going to the player)??
                    gameManager.playerHealthManager.PotionConsumed();
                    gameManager.inventoryManager.PotionConsumed();
                    HavePotionInHand();
                }
            }
        }
    }

    public void HavePotionInHand()
    {
        if (gameManager.inventoryManager.GetPotionAmount() > 0)
        {
            potionObj.SetActive(true);
        }
        else
        {
            potionObj.SetActive(false);
        }
    }
}
