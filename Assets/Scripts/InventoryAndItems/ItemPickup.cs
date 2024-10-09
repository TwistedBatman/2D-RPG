using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    private List<Item> itemList = new List<Item>();
    public InventoryManager inventoryManager;
    private bool collected;
    [SerializeField] private string id;
    AudioManager audioManager;
/*    [ContextMenu("Genetate guid for id")]
    private void GenerateGuid()
    {
        // Thelei allagi prepei na mpei arithmos kai na apothikeuetai, na mhn allazei kathe fora
        id = this.name + transform.position.ToString();
    }*/

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    private void Start()
    {
        id = this.name + transform.position.ToString();
        audioManager = FindObjectOfType<AudioManager>();
        // Find the list of items in inventory manager and add the to a list
        inventoryManager = FindObjectOfType<InventoryManager>();
        foreach (Item i in inventoryManager.droppedItems)
        {
            itemList.Add(i);
        }
        // Search which item from the list of items was dropped
        foreach (Item i in itemList)
        {
            if (i == item)
            {
                item = i;
            }
        }
    }

    void PickUp() // Pick up item and add it to inventory
    {
        if (item.isEquipment)
            collected = inventoryManager.AddEquipment(item);
        else
            collected = inventoryManager.AddItem(item);

        if (collected)
        {
            audioManager.PlaySound("PickUp", 0f);
            Destroy(gameObject);
        }
        else
            Debug.Log("Not enough inventory space.");
    }

/*    public void LoadData(GameData data)
    {
        data.itemsCollected.TryGetValue(id, out collected);
        if (collected)
        {
            //Destroy(gameObject);
        }
    }

    public void SaveData(GameData data)
    {
        if (data.itemsCollected.ContainsKey(id))
        {
            //Debug.Log(id + " and is " + collected);
            data.itemsCollected.Remove(id);
        }
        data.itemsCollected.Add(id, collected);
    }*/
}
