using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string title;
    [SerializeField] string shopMessage;
    [SerializeField] int goldCost;
    [SerializeField] int ironCost;
    [SerializeField] int woodCost;
    [SerializeField] int leatherCost;
    public string generalMessage;
    public bool isShopItem = false;
    public bool isEquipment = false;
    public bool isStats = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (ironCost > 0)
            TooltipManager._instance.SetAndShowTooltip(title, shopMessage, goldCost, ironCost, generalMessage, isShopItem, isEquipment, isStats, 1);
        else if (woodCost > 0)
            TooltipManager._instance.SetAndShowTooltip(title, shopMessage, goldCost, woodCost, generalMessage, isShopItem, isEquipment, isStats, 2);
        else if (leatherCost > 0)
            TooltipManager._instance.SetAndShowTooltip(title, shopMessage, goldCost, leatherCost, generalMessage, isShopItem, isEquipment, isStats, 3);
        else
            TooltipManager._instance.SetAndShowTooltip(title, shopMessage, goldCost, 0, generalMessage, isShopItem, isEquipment, isStats, 0);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        TooltipManager._instance.HideTooltip();
    }
}
