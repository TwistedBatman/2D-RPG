using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public TextMeshProUGUI countText;

    public Item item;
    public InventoryManager inventoryManager;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    [SerializeField] Tooltip tooltip;

    public bool isDragged = false;

    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        //Debug.Log("item name " + item.name);
        image.sprite = newItem.image;
        image.preserveAspect = true;
        RefreshCount();
    }

    public void RefreshCount() // Set the number of stackable items
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public int ReturnCount(Item item)
    {
        for (int i = 0; i < inventoryManager.inventorySlots.Length; i++) // Stack items in slots with the same items until max stacks
        {
            InventorySlot slot = inventoryManager.inventorySlots[i];
            InventoryItem itemSlot = slot.GetComponent<InventoryItem>();
            if (itemSlot.item.name == "Iron")
            {
                count++;
            }
        }
        return count;
    }

    // When start dragging change the parent of the object to the root and make it the last item in hierarchy
    // So when you are dragging it, it shows above all the other objects
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragged = true;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false; // Disable the raycast on dragging object to find the object the mouse points
        // If it's true the mouse points on the draggable item instead of the place to put it
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition; // While dragging make the object position equals the mouse position
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag); // Make the place where the item dropped the new parent
        transform.SetAsFirstSibling();
        image.raycastTarget = true; // Make it true again so you can interact with the object
        isDragged = false;
    }

    public void LoadData(GameData data)
    {
/*        if (item != null)
        {
            item = data.saveItem.item;
            //count.te
            transform.SetParent(data.saveItem.parentAfterDrag);
            transform.SetAsFirstSibling();
            image.raycastTarget = true;
        }
            Debug.Log(parentAfterDrag);*/
    }

/*    public void SaveData(GameData data) 
    {
        *//*        if (item != null)
                {
                    data.saveItem.item = item;
                    data.saveItem.count = count;
                    data.saveItem.parentAfterDrag = parentAfterDrag;
                }
                    Debug.Log(item);*//*
        if (item != null)
        {
            data.inventoryItems.Add(transform.parent.name, item);
        }
           // Debug.Log(transform.parent.name);
    }*/

}
