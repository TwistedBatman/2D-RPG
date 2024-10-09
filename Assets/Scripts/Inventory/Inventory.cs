using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found.");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;
    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if(!item.stackable) // Check if the inteactable item can be picked up
        {
            if (items.Count >= space) // Check if the inventory is full
            {
                Debug.Log("Not enough inventory space.");
                return false;
            }
            items.Add(item); // Add item to inventory

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke(); // Trigger event
            return true;
        }
        Debug.Log("hhh");
        return true;
    }

    public void Remove(Item item) // Remove item from inventory
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke(); // Trigger event
    }
}
