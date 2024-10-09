using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
   /* public Transform itemsParents;
    public GameObject inventoryUI;

    Inventory inventory;
    InventorySlot2[] slots;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParents.GetComponentsInChildren<InventorySlot2>(true); // Make it true so even if the inventory window is closed it still works
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory")) // Open/Close inventory when the button is pressed
        {
            Debug.Log("close");
            inventoryUI.SetActive(!inventoryUI.activeSelf); // Set at reverse state
        }
    }
    void UpdateUI() // Set the picked up object to UI
    {
        Debug.Log("Updating UI");

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]); // Set items to inventory window
            }
            else
            {
                slots[i].ClearSlot(); // Clear all the unused slots
            }
        }
    }*/
}
