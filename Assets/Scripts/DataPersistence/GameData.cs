 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int playerLevel;
    public int playerEXP;
    public int availableSkillPoints;
    public int coins;
    //public int iron;
    //public int wood;
    //public int leather;
    public Vector3 playerPosition;
    public List<Item> equipedItems;
    public bool quest1Completed;
    public bool quest2Completed;
    public bool skill1Activated;
    public bool skill2Activated;
    public bool skill3Activated;
    public bool skill4Activated;
    public bool skill5Activated;
    public bool skill6Activated;
    public bool skill7Activated;
    public bool skill8Activated;
    public bool skill9Activated;
    public bool skill10Activated;
    public int leftSkills;
    public int rightSkills;
    public SerializableDictionary<string, int> invenoryItems;
    //public List<bool> skillsActivated;//
    //public SerializableDictionary<string, bool> itemsCollected;
    //public SerializableDictionary<string, ScriptableObject> inventoryItems;
    //public InventoryItem saveItem;

    public GameData()
    {
        this.playerLevel = 1;
        this.playerEXP = 0;
        this.coins = 10;
        availableSkillPoints = 0;
        equipedItems = new List<Item>();
/*        this.iron = 0;
        this.wood = 0;
        this.leather = 0;*/
        this.quest1Completed = false;
        this.quest2Completed = false;
        this.skill1Activated = false;
        this.skill2Activated = false;
        this.skill3Activated = false;
        this.skill4Activated = false;
        this.skill5Activated = false;
        this.skill6Activated = false;
        this.skill7Activated = false;
        this.skill8Activated = false;
        this.skill9Activated = false;
        this.skill10Activated = false;
        leftSkills = 0;
        rightSkills = 0;
        playerPosition = new Vector3(49.5f, 8, -1);
        invenoryItems = new SerializableDictionary<string, int>();
        //itemsCollected = new SerializableDictionary<string, bool>();
        //inventoryItems = new SerializableDictionary<string, ScriptableObject>();
    }
}
