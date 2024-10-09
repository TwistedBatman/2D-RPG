using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class LootToDrop : MonoBehaviour
{
    public GameObject itemToDrop;
    public List<Item> lootList = new List<Item>();
    public Item droppedItem;
    public int increaseDropChance;
    public bool multipleDrops = false;
    List<Item> possibleItems;
    //[SerializeField] Attributes attributes;

    public Item GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        Debug.Log("Random number is: " + randomNumber);
        possibleItems = new List<Item>();
        foreach (Item item in lootList)
        {
            if (randomNumber <= (item.dropChance + increaseDropChance))
            {
                possibleItems.Add(item);
            }
        }
        if (possibleItems.Count > 0)
        {
            if (!multipleDrops)
            {
                Item droppedItem = possibleItems.Last();
                return droppedItem;
            }
            else
            {
                foreach (Item item in possibleItems)
                {
                    return item;
                }
            }
        }
        Debug.Log("No loot");
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        // Thelei ftiaksimo to id
        droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            for (int i = 0; i < possibleItems.Count; i++)
            {
                GameObject lootGameObject = Instantiate(itemToDrop, spawnPosition, Quaternion.identity);
                lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.image;
                FindObjectOfType<ItemPickup>().item = droppedItem;
                Debug.Log(droppedItem);
            }
        }
    }
}
