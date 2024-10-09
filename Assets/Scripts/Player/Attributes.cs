using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Attributes : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI strength;
    [SerializeField] TextMeshProUGUI dexterity;
    public TextMeshProUGUI constitutution;
    [SerializeField] TextMeshProUGUI intelligence;
    [SerializeField] TextMeshProUGUI wisdom;
    public TextMeshProUGUI luck;
    public TextMeshProUGUI playerLevelText;
    public int attributePoints = 0;
    [SerializeField] List<Button> attributeButtons;

    float melee;
    float range;
    float magic;
    float critical;
    float health;
    float mana;
    float defense;
    float exp;

    //public static Attributes attributes;

    PlayerEXP playerLevel;
    [SerializeField] PlayerHealthManager playerHealth;
    WeaponAttack weaponAttack;
    PlayerManaManager playerMana;
    LootToDrop lootToDrop;
    Skill skill;

    void Start()
    {
        playerLevel = FindObjectOfType<PlayerEXP>();
        //playerHealth = FindObjectOfType<PlayerHealthManager>();
        weaponAttack = FindObjectOfType<WeaponAttack>();
        lootToDrop = FindObjectOfType<LootToDrop>();
        playerMana = FindObjectOfType<PlayerManaManager>();
        skill = FindObjectOfType<Skill>();

        attributeButtons.AddRange(GetComponentsInChildren<Button>());
        if (attributePoints >= 1)
            ActivateOrDeactivateAttributeButtons(true);
        else
            ActivateOrDeactivateAttributeButtons(false);
        playerLevelText.text = "Level: " + playerLevel.playerLevel.ToString();
    }

    public void IncreaseAttribute(TextMeshProUGUI attribute)
    {
        if (attributePoints >= 1)
        {
            attribute.text = (int.Parse(attribute.text) + 1).ToString();
            attributePoints -= 1;
            if (attributePoints == 0)
                ActivateOrDeactivateAttributeButtons(false);
            SetAttributes(attribute.name);
        }
        else
            ActivateOrDeactivateAttributeButtons(false);
    }

    public void SetAttributes(string name)
    {
        switch (name)
        {
            case "StrTextNumber":
                melee = float.Parse(strength.text);
                Debug.Log(melee);
                weaponAttack.damageToGive = 2 + melee/2;
                StatsManager.statsManager.melee.text = melee.ToString() + "%";
                break;
            case "DexTextNumber":
                range = float.Parse(dexterity.text) * 0.5f;
                // change damage for range...
                StatsManager.statsManager.range.text = range.ToString() + "%";
                break;
            case "ConTextNumber":
                //playerHealth.maxHealth = 100;
                playerHealth.SetMaxHealth(10);
                StatsManager.statsManager.health.text = (int.Parse(StatsManager.statsManager.health.text) + 10).ToString();
                break;
            case "IntTextNumber":
                mana = float.Parse(intelligence.text) * 10f;
                playerMana.SetMaxMana(mana);
                StatsManager.statsManager.mana.text = playerMana.maxMana.ToString();
                break;
            case "WisTextNumber":
                magic = float.Parse(intelligence.text) * 0.5f;
                // change damage for magic...
                StatsManager.statsManager.magic.text = magic.ToString() + "%";
                break;
            case "LuckTextNumber":
                critical = float.Parse(luck.text) * 2f;
                lootToDrop.increaseDropChance = int.Parse(luck.text);
                StatsManager.statsManager.critical.text = critical.ToString() + "%";
                break;
        }
    }

    public void ResetAttributes()
    {
        strength.text = "1";
        dexterity.text = "1";
        constitutution.text = "1";
        intelligence.text = "1";
        wisdom.text = "1";
        luck.text = "1";

        if (playerLevel.playerLevel > 1)
        {
            attributePoints = (playerLevel.playerLevel * 2) - 2;
            ActivateOrDeactivateAttributeButtons(true);
        }

        StatsManager.statsManager.SetStats();

        weaponAttack.damageToGive = 2f;
        if (skill.skillTree.skill1Activated)
        {
            if (playerHealth.currentHealth > 110)
                playerHealth.currentHealth = 110;
            playerHealth.maxHealth = 110;
        }else if (playerHealth.currentHealth > 100)
        {
            playerHealth.currentHealth = 100;
            playerHealth.maxHealth = 100;
        }
        else
            playerHealth.maxHealth = 100;

        playerHealth.SetHealth();
        lootToDrop.increaseDropChance = 1;
    }

    public void ActivateOrDeactivateAttributeButtons(bool isActive)
    {
        foreach (Button item in attributeButtons)
        {
            item.gameObject.SetActive(isActive);
        }
    }
}
