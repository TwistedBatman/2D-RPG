using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsAndGatherables : MonoBehaviour, IDataPersistence
{
    public InventoryItem inventoryItem;
    public TextMeshProUGUI coinsText;
    public int currentCoins = 0;
    public int currentIron= 0;
    public int currentWood = 0;
    public int currentLeather = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCoins(int amountToAdd)
    {
        currentCoins += amountToAdd;
        coinsText.text = currentCoins.ToString();
    }

    public void SubCoins(int amountToSub)
    {
        if (currentCoins > amountToSub)
        {
            currentCoins -= amountToSub;
            coinsText.text = currentCoins.ToString();
        }
        else
        {
            currentCoins = 0;
            coinsText.text = "0";
        }
    }
    public void AddIron(int amountToAdd)
    {
        currentIron += amountToAdd;
        //coinsText.text = currentCoins.ToString();
    }

    public void SubIron(int amountToSub)
    {
/*        currentIron = inventoryItem.ReturnCount(Item);
        Debug.Log(currentIron);*/
        currentIron -= amountToSub;
       // coinsText.text = currentCoins >= 0 ? currentCoins.ToString() : 0.ToString();
    }
    public void AddWood(int amountToAdd)
    {
        currentWood += amountToAdd;
        //coinsText.text = currentCoins.ToString();
    }

    public void SubWood(int amountToSub)
    {
        currentWood -= amountToSub;
        //coinsText.text = currentCoins >= 0 ? currentCoins.ToString() : 0.ToString();
    }
    public void AddLeather(int amountToAdd)
    {
        currentLeather += amountToAdd;
        //coinsText.text = currentCoins.ToString();
    }

    public void SubLeather(int amountToSub)
    {
        currentLeather -= amountToSub;
        //coinsText.text = currentCoins >= 0 ? currentCoins.ToString() : 0.ToString();
    }

    public void LoadData(GameData data)
    {
        this.currentCoins = data.coins;
/*        this.currentIron = data.iron;
        this.currentWood = data.wood;
        this.currentLeather = data.leather;*/
        coinsText.text = currentCoins.ToString();
    }

    public void SaveData(GameData data)
    {
        data.coins = this.currentCoins;
/*        data.iron = this.currentIron;
        data.wood = this.currentWood;
        data.leather = this.currentLeather;*/
    }
}
