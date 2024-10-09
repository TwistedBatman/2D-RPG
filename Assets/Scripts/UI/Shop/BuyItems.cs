using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data;

public class BuyItems : MonoBehaviour
{
    private InventoryManager inventoryManager;
    public GameObject shopUI;
    public GameObject blacksmithUI;
    public GameObject dialogueUI;
    private CoinsAndGatherables coinsAndGatherable;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        coinsAndGatherable = FindObjectOfType<CoinsAndGatherables>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            shopUI.SetActive(false);
            TooltipManager._instance.HideTooltip();
            Time.timeScale = 1;
        }
    }

    public void OnClickBuy(Item item)
    {
        //bool bought = false;
        if (coinsAndGatherable.currentCoins >= item.goldCost)
        {
            if (item.ironCost + item.woodCost + item.leatherCost == 0)
            {
                coinsAndGatherable.SubCoins(item.goldCost);
                inventoryManager.AddItem(item);
                audioManager.PlaySound("Coins", 0f);
            }
            else
            {
                if (coinsAndGatherable.currentIron >= item.ironCost && item.ironCost > 0)
                {
                    coinsAndGatherable.SubIron(item.ironCost);
                    inventoryManager.RemoveResourceFromInventory("Iron", item.ironCost);
                    coinsAndGatherable.SubCoins(item.goldCost);
                    inventoryManager.AddItem(item);
                    audioManager.PlaySound("Coins", 0f);
                    //bought = true;
                }
                else if (coinsAndGatherable.currentWood >= item.woodCost && item.woodCost > 0)
                {
                    coinsAndGatherable.SubWood(item.woodCost);
                    inventoryManager.RemoveResourceFromInventory("Wood", item.woodCost);
                    coinsAndGatherable.SubCoins(item.goldCost);
                    inventoryManager.AddItem(item);
                    audioManager.PlaySound("Coins", 0f);
                    //bought = true;
                }
                else if (coinsAndGatherable.currentLeather >= item.leatherCost && item.leatherCost > 0)
                {
                    coinsAndGatherable.SubLeather(item.leatherCost);
                    inventoryManager.RemoveResourceFromInventory("Leather", item.leatherCost);
                    coinsAndGatherable.SubCoins(item.goldCost);
                    inventoryManager.AddItem(item);
                    audioManager.PlaySound("Coins", 0f);
                    //bought = true;
                }
                else
                    Debug.Log("Not enough materials");
            }
        }
        else
        {
            Debug.Log("Not enough gold");
        }

    }
    public void OnClickBuyWithGold(Item item)
    {
        if (coinsAndGatherable.currentCoins >= item.goldCost)
        {
            coinsAndGatherable.SubCoins(item.goldCost);
            inventoryManager.AddItem(item);
        }
        else
        {
            Debug.Log("Not enough gold");
        }
    }

    public void OpenShop(int shop)
    {
        Time.timeScale = 0f;
        dialogueUI.SetActive(false);
        if (shop == 0)
            shopUI.SetActive(true);
        else
            blacksmithUI.SetActive(true);
    }
}
