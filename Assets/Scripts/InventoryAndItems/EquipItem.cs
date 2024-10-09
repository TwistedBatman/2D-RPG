using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EquipItem : MonoBehaviour, IDataPersistence
{
    [SerializeField] PlayerHealthManager healthManager;
    [SerializeField] StatsManager statsManager;
    [SerializeField] List<Item> loadEquipedItems;
    [SerializeField] List<Item> equipedItems;
    public List<Equipment> equipmentSlots;
    [SerializeField] InventoryManager inventory;
    public GameObject equipmentItemPrefab;
    Item itemToSave;
    //Item itemToLoad;


    void Start()
    {
        //inventory = GetComponent<InventoryItem>();
    }

    public void EquipItemsOnSlot()
    {
        if (equipmentSlots != null)
        {
/*            Debug.Log(equipedItems.Count);
            Debug.Log(equipmentSlots.Count);*/
            foreach (Item item in equipedItems.ToList())
            {
                foreach (Equipment slot in equipmentSlots)
                {
                    if (item.type == slot.name && slot.transform.childCount == 0)
                    {
                        inventory.SpawnNewEquipment(item, slot, equipmentItemPrefab);
                        /*Debug.Log(item.type + " " + slot.name);
                        //Equipment equipSlot = slot;
                        Debug.Log(equipmentSlots.Count);
                        GameObject newItemObject = Instantiate(equipmentItemPrefab, slot.transform);
                        InventoryItem itemSlot = newItemObject.GetComponentInChildren<InventoryItem>();
                        newItemObject.name = item.name;
                        itemSlot.InitialiseItem(item);
                        //itemToLoad = item;
                        Equip(item);
                        //InventoryItem itemSlot = slot.GetComponentInChildren<InventoryItem>();
                        Debug.Log(itemSlot);
                            if (slot != null && slot.transform.childCount == 0)
                                itemSlot.transform.SetParent(slot.transform);*/

                    // na dw pws einai sto inventory slot kai me thn lista inventory slot tou inventory manager
                        /*                        Debug.Log(item);
                                                Debug.Log(inventory);
                                                inventory.item = item;

                                                inventory = item.GetComponent<InventoryItem>();
                                                inventory.equipedItems.transform.parent = slot.transform;*/


                        /*                        draggableItem = dropped.GetComponent<InventoryItem>();
                                                draggableItem.parentAfterDrag = transform;
                                                text.gameObject.SetActive(false);
                                                ChangedSlot = true;
                                                equipItem.Equip(draggableItem.item);*/
                }
                }
            }
        }
    }


    public void Equip(Item item)
    {
        switch (item.name)
        {
            case "LeatherBoots":
                healthManager.defense += 1;
                equipedItems.Add(item);
                break;
            case "IronBreast":
                healthManager.defense += 5;
                equipedItems.Add(item);
                break;
            case "IronHelmet":
                healthManager.defense += 3;
                equipedItems.Add(item);
                break;
            case "IronBracers":
                healthManager.defense += 2;
                equipedItems.Add(item);
                break;
            case "WoodenBracers":
                healthManager.defense += 1;
                equipedItems.Add(item);
                break;
            case "FullBodyBreast":
                healthManager.defense += 15;
                equipedItems.Add(item);
                break;
            case "EmeraldNecklace":
                equipedItems.Add(item);
                break;
            case "GarnetNecklace":
                equipedItems.Add(item);
                break;
            case "GarnetRing": 
                equipedItems.Add(item);
                break;
            case "EmeraldRing":
                equipedItems.Add(item);
                break;
            default:
                break;
        }
        statsManager.defense.text = healthManager.defense.ToString();
    }

    public void Unequip(Item item)
    {
        switch (item.name)
        {
            case "LeatherBoots":
                healthManager.defense -= 1;
                equipedItems.Remove(item);
                break;
            case "IronBreast":
                healthManager.defense -= 5;
                equipedItems.Remove(item);
                break;
            case "IronHelmet":
                healthManager.defense -= 3;
                equipedItems.Remove(item);
                break;
            case "IronBracers":
                healthManager.defense -= 2;
                equipedItems.Remove(item);
                break;
            case "WoodenBracers":
                healthManager.defense -= 1;
                equipedItems.Remove(item);
                break;
            case "FullBodyBreast":
                healthManager.defense -= 15;
                equipedItems.Remove(item);
                break;
            case "EmeraldNecklace":
                equipedItems.Remove(item);
                break;
            case "GarnetNecklace":
                equipedItems.Remove(item);
                break;
            case "GarnetRing":
                equipedItems.Remove(item);
                break;
            case "EmeraldRing":
                equipedItems.Remove(item);
                break;
            default:
                break;
        }
        statsManager.defense.text = healthManager.defense.ToString();
    }

    public void LoadData(GameData data)
    {
        this.equipedItems = data.equipedItems;
        EquipItemsOnSlot();
    }

    public void SaveData(GameData data)
    {
        data.equipedItems.Clear();
        foreach (Equipment slot in equipmentSlots)
        {
            if (slot.transform.childCount != 0) 
            {
                InventoryItem item = slot.transform.GetComponentInChildren<InventoryItem>();
                equipedItems.Add(item.item);
            }
        }
        data.equipedItems = this.equipedItems;
    }
}
