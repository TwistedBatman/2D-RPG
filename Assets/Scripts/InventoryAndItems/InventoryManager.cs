using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IDataPersistence
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public GameObject equipmentItemPrefab;
    public int maxStack;
    public GameObject inventoryUI;
    public GameObject skillsUI;
    public CoinsAndGatherables coinsAndGatherables;
    [SerializeField] GameObject characterUI;
    [SerializeField] GameObject questUI;
    public GameObject pauseMenu;

    public List<ScriptableObject> droppedItems;
    public List<GameObject> equipedItems;
    public List<Item> allItems;

    public Quest2 quest2;

    [SerializeField] UseItem useItem;

    SerializableDictionary<string, int> itemsInInventory;

    bool selected = false;
    int selectedSlot = -1;

    void Update()
    {
        // Check the buttons pressed if they are numbers from the Toolbar
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number >= 1 && number <= 8)
                ChangeSelectedSlot(number - 1);
        }

        if (Input.GetButtonDown("Inventory")) // Open/Close inventory when the button is pressed
            inventoryUI.SetActive(!inventoryUI.activeSelf); // Set at reverse state

        if (Input.GetButtonDown("Skills")) // Open/Close skills when the button is pressed
            skillsUI.SetActive(!skillsUI.activeSelf); // Set at reverse state
        
        if (Input.GetButtonDown("Character"))
            characterUI.SetActive(!characterUI.activeSelf); 

        if (Input.GetButtonDown("Quest"))
            questUI.SetActive(!questUI.activeSelf);

        if (Input.GetButtonDown("Escape"))
        {
            inventoryUI.SetActive(false);
            skillsUI.SetActive(false);
            characterUI.SetActive(false);
            questUI.SetActive(false);
            pauseMenu.SetActive(false);

        }

    }

    public void ChangeSelectedSlot(int value) // Called when Toolbar slot button is pressed or clicked
    {
        if (selectedSlot >= 0 && value  < 8) // Always deselect first for when changing slots
            inventorySlots[selectedSlot].Deselect();

        // When the same button is pressed deselect if it's selected or select if it's not selected, for toolbar only
        if (value < 8)
        {
            if (selectedSlot == value)
            {
                if (selected)
                {
                    inventorySlots[value].Deselect();
                    selected = false;
                }
                else
                {
                    inventorySlots[selectedSlot].Select();
                    selected = true;
                }
            }

            if (selectedSlot != value) // Every time a different button is pressed select the slot
            {
                inventorySlots[value].Select();
                selected = true;
            }
        }

        selectedSlot = value;
        GetSelectedItem();
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++) // Stack items in slots with the same items until max stacks
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemSlot != null && itemSlot.item == item && itemSlot.count < maxStack && itemSlot.item.stackable == true)
            {
                AddResource(item);
                itemSlot.count++;
                AddGatherable(item);
                itemSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++) // Loop through all the slots until you find empty to add item
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemSlot == null)
            {
                AddResource(item);
                AddGatherable(item);
                SpawnNewItem(item, slot, inventoryItemPrefab);
                return true;
            }
        }
        return false;
    }

    public bool AddEquipment(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++) // Loop through all the slots until you find empty to add item
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemSlot == null)
            {
                SpawnNewItem(item, slot, equipmentItemPrefab);
                return true;
            }
        }
        return false;
    }

    public void RemoveResourceFromInventory(string name, int amount)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemSlot != null && itemSlot.item.name == name)
            {
                itemSlot.count -= amount;
                itemSlot.RefreshCount();
                if (itemSlot.count <= 0)
                    Destroy(itemSlot.gameObject);
                    
            }
        }
    }

    public int CheckForNumberOfItems(string name)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemSlot != null && itemSlot.item.name == name)
                return itemSlot.count;
        }
        return 0;
    }

    public void AddResource(Item item) 
    {
        if (item.name == "Iron")
            coinsAndGatherables.currentIron++;
        else if (item.name == "Wood")
            coinsAndGatherables.currentWood++;
        else if (item.name == "Leather")
            coinsAndGatherables.currentLeather++;
    }

    public void AddGatherable(Item item)
    {
        if (item.name == "Apple")
            quest2.IncreaseItemsGathered();
    }

    public void LoadData(GameData data)
    {
        foreach (KeyValuePair<string, int> invItem in data.invenoryItems)
        {
            foreach (Item item in allItems)
            {            
                if (invItem.Key == item.name)
                    for (int i = 0; i < invItem.Value; i++)
                        AddItem(item);
            }
        }
        data.invenoryItems.Clear();
    }

    public void SaveData(GameData data)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemSlot != null && !data.invenoryItems.ContainsKey(itemSlot.item.name))
                data.invenoryItems.Add(itemSlot.item.name, itemSlot.count);
        }

    }


    // Steps to add new Equipment item
    // Create a scriptable item, fill name, type, check is equipment bool and add image
    // Add the equipment prefab to the hierarchy, name it, tag it, add scriptable item to Item Pickup script, fill tooltip, add image
    // Add it to inventory manager list
    // The scriptable item name should be the same as the item name, the type shoul be the same as prefab itm tag


    public void SpawnNewItem(Item originalItem, InventorySlot slot, GameObject typeOfItem) // Create the item at empty slot
    {
        GameObject newItemObject = Instantiate(typeOfItem, slot.transform);
        InventoryItem newItem = newItemObject.GetComponent<InventoryItem>();

        // Create a copy of the equipedItems list to avoid modifying it while iterating
        List<GameObject> copyEquipedItems = new List<GameObject>(equipedItems);

        // Search in the list of items for the type and name of the scriptable item equipment with the tag (for type) and name of prefab item
        foreach (GameObject item in copyEquipedItems)
        {            
            //Debug.Log("item " + item.name + " original  " + originalItem.name + " type " + originalItem.type);
            if (item != null && item.tag == originalItem.type && item.name == originalItem.name)
            {
                newItemObject.name = originalItem.name;
                Tooltip originalTooltip = item.GetComponent<Tooltip>();
                if (originalTooltip != null)
                {
                    // Access the Tooltip component from the original item and copy its properties
                    Tooltip newTooltip = newItemObject.AddComponent<Tooltip>();
                    newTooltip.title = originalTooltip.title;
                    newTooltip.generalMessage = originalTooltip.generalMessage;
                    newTooltip.isEquipment = originalTooltip.isEquipment;
                }
                //equipedItems.Remove(item);
                break; // Exit the loop after finding the matching item
            }
        }
        // Copy properties from the original ScriptableObject Item
        newItem.InitialiseItem(originalItem);
    }

    public void SpawnNewEquipment(Item originalItem, Equipment slot, GameObject typeOfItem) // Create the item at empty slot
    {
        GameObject newItemObject = Instantiate(typeOfItem, slot.transform);
        InventoryItem newItem = newItemObject.GetComponent<InventoryItem>();

        // Create a copy of the equipedItems list to avoid modifying it while iterating
        List<GameObject> copyEquipedItems = new List<GameObject>(equipedItems);

        // Search in the list of items for the type and name of the scriptable item equipment with the tag (for type) and name of prefab item
        foreach (GameObject item in copyEquipedItems)
        {
            if (item != null && item.tag == originalItem.type && item.name == originalItem.name)
            {
                //Debug.Log("item " + item.name + " original  " + originalItem.name + " type " + originalItem.type);
                newItemObject.name = originalItem.name;
                Tooltip originalTooltip = item.GetComponent<Tooltip>();
                if (originalTooltip != null)
                {
                    // Access the Tooltip component from the original item and copy its properties
                    Tooltip newTooltip = newItemObject.AddComponent<Tooltip>();
                    newTooltip.title = originalTooltip.title;
                    newTooltip.generalMessage = originalTooltip.generalMessage;
                    newTooltip.isEquipment = originalTooltip.isEquipment;
                }
                //equipedItems.Remove(item);
                break; // Exit the loop after finding the matching item
            }
        }
        // Copy properties from the original ScriptableObject Item
        newItem.InitialiseItem(originalItem);
    }





    /*    public void SpawnNewItem(Item item, InventorySlot slot, GameObject typeOfItem) // Create the item at empty slot
        {
            GameObject newItem = Instantiate(typeOfItem, slot.transform);
            newItem.name = item;
            newItem.
            InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
            newItem.AddComponent<Tooltip>()
            Debug.Log(item.GetInstanceID());
            Debug.Log(newItem.GetInstanceID());
            inventoryItem.InitialiseItem(item);
        }*/


    public void GetSelectedItem()
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            bool canUse = useItem.Use(item);

            if (item.isUsable && canUse)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                    Destroy(itemInSlot.gameObject);
                else
                    itemInSlot.RefreshCount();
                if (selectedSlot < 8)
                {
                    inventorySlots[selectedSlot].Deselect();
                    selected = false;
                }

            }
            
            item.Use();
            //return item;
        }
       // return null;
    }

    public void CloseCurrentWindow(GameObject gameObject)
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
