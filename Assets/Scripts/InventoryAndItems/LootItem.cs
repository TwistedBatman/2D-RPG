using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Item", menuName = "Scriptable Object/Loot Item")]
public class LootItem : ScriptableObject
{
    public string itemName;
    public Sprite image;
    public int dropChance;

    public LootItem(string itemName, int dropChance)
    {
        this.itemName = itemName;
        this.dropChance = dropChance;
    }
}
