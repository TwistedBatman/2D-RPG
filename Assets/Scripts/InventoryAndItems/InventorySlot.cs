using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;
    InventoryItem draggableItem;
    [SerializeField] InventoryManager inventoryManager;

    public void Select() // Change the selected slot color
    {
        image.color = selectedColor;
    }

    public void Deselect() // Change back to the original color
    {
        image.color = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData) // Triggered when item is dropped on it
    {
        // Find the parent on where the item dropped
        if (transform.childCount == 0) // Add object to the slot only if there are no other objects (except the icon)
        {
            GameObject dropped = eventData.pointerDrag;
            draggableItem = dropped.GetComponent<InventoryItem>();
            draggableItem.parentAfterDrag = transform;

//            inventoryManager.SpawnNewItem(draggableItem.item, this, draggableItem.gameObject);
        }
    }

/*    public void LoadData(GameData data)
    {
        data.inventoryItems.TryGetValue(draggableItem.name, out draggableItem);
        draggableItem.parentAfterDrag = transform;
    }

    public void SaveData(GameData data)
    {
        if (transform.childCount == 1)
        {
            data.inventoryItems.Add(draggableItem.name, draggableItem);
            //Debug.Log(draggableItem.name);
        }
    }*/
}
