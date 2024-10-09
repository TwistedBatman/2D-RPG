using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Equipment : MonoBehaviour, IDropHandler
{
    InventoryItem draggableItem;
    [SerializeField] EquipItem equipItem;
    public TooltipManager tooltipManager;
    [SerializeField] TextMeshProUGUI text;
    bool ChangedSlot = false;

    void Update()
    {
        // Check if the item after drag changed slot and reveal the equipment slot text
        if (transform.childCount == 0  && ChangedSlot) 
        {
            if (!draggableItem.isDragged)
            {
                text.gameObject.SetActive(true);
                ChangedSlot = false;
                equipItem.Unequip(draggableItem.item);
            }
        }
    }

    public void OnDrop(PointerEventData eventData) // Triggered when item is dropped on it
    {
        // Find the parent on where the item dropped
        if (transform.childCount == 0) // Add object to the slot only if there are no other objects
        {
            GameObject dropped = eventData.pointerDrag;
            draggableItem = dropped.GetComponent<InventoryItem>();
            if (draggableItem.item.isEquipment && name == draggableItem.item.type)
            {
                draggableItem.parentAfterDrag = transform;
                text.gameObject.SetActive(false);
                ChangedSlot = true;
                equipItem.Equip(draggableItem.item);
            }
        }
    }
}