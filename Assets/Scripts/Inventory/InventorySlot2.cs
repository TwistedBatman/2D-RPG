using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot2 : MonoBehaviour, IDropHandler
{
    public Image icon;

    Item item;
    // Old inventory not used
    public void AddItem(Item newItem) // Add item to inventory slot
    {
        item = newItem;

        icon.sprite = item.image;
        icon.enabled = true;
    }

    public void ClearSlot() // Clear all empty inventory slots
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnDrop(PointerEventData eventData) // Triggered when item is dropped on it
    {
        // Find the parent on where the item dropped
        if (transform.childCount <= 1) // Add object to the slot only if there are no other objects (except the icon)
        {
            GameObject dropped = eventData.pointerDrag;
            InventoryItem draggableItem = dropped.GetComponent<InventoryItem>();
            draggableItem.parentAfterDrag = transform;
        }
    }

    public void UseItem() // Use item when pressed on inventory
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
