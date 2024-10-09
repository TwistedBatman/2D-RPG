using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerEXP : MonoBehaviour, IDataPersistence
{
    public int playerLevel = 1;
    int maxLevel = 10;
    public int currentExp;
    public SkillTree availableSkillPoints;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Attributes attributes;
    PlayerHealthManager playerHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealthManager>();
        levelText.text = playerLevel.ToString();
        //availableSkillPoints.skillPoints = playerLevel;
        StatsManager.statsManager.exp.text = currentExp.ToString() + "/100";
    }

    // Amount of EXP to give
    public void ExpToGive(int amount)
    {
        // Every 100 EXP raise the player level by 1 but no more than 10
        if ((currentExp + amount) >= 100)
        {
            if (playerLevel < maxLevel)
            {
                playerLevel++;
                levelText.text = playerLevel.ToString();
                availableSkillPoints.skillPoints++; // Every time the player levels up also gains 1 skill point
                attributes.attributePoints += 2; // Every time the player levels up also gains 2 attribute point
                attributes.ActivateOrDeactivateAttributeButtons(true);
                attributes.playerLevelText.text = "Level: " + playerLevel.ToString();
                playerHealth.FullHealPlayer();
                currentExp = (currentExp + amount) - 100;
                StatsManager.statsManager.exp.text = currentExp.ToString() + "/100";
            }
            else
                playerLevel = 10;
        }
        else
        {
            currentExp += amount;
            StatsManager.statsManager.exp.text = currentExp.ToString() + "/100";
        }

        //Debug.Log("current exp " + currentExp);
    }

    public void LoadData(GameData data)
    {
        this.playerLevel = data.playerLevel;
        this.currentExp = data.playerEXP;
    }

    public void SaveData(GameData data)
    {
        data.playerLevel = this.playerLevel;
        data.playerEXP = this.currentExp;
    }
}
