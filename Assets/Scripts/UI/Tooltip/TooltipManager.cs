using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager _instance;

    public TextMeshProUGUI tooltipTitle;
    public TextMeshProUGUI tooltipText;
    public TextMeshProUGUI tooltipGoldCost;
    public TextMeshProUGUI tooltipIronCost;
    public TextMeshProUGUI tooltipWoodCost;
    public TextMeshProUGUI tooltipLeatherCost;
    public TextMeshProUGUI tooltipGeneralText;
    public Image coinImage;
    public Image ironImage;
    public Image woodImage;
    public Image leatherImage;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void SetAndShowTooltip(string title, string shopMessage, int goldCost, int materialCost, string generalMessage, bool isShopItem, bool isEquipment, bool isStats, int materialType)
    {
        gameObject.SetActive(true);
        tooltipGoldCost.text = "";
        tooltipIronCost.text = "";
        tooltipWoodCost.text = "";
        tooltipLeatherCost.text = "";
        coinImage.gameObject.SetActive(false);
        ironImage.gameObject.SetActive(false);
        woodImage.gameObject.SetActive(false);
        leatherImage.gameObject.SetActive(false);
        if (isEquipment)
        {
            tooltipTitle.text = title;
            tooltipText.text = generalMessage;
            
            tooltipGeneralText.text = "";
        }
        else if (isShopItem)
        {
            switch (materialType)
            {
                case 0:
                    tooltipGoldCost.text = goldCost.ToString();
                    coinImage.gameObject.SetActive(true);
                    break;
                case 1:
                    tooltipGoldCost.text = goldCost.ToString();
                    tooltipIronCost.text = materialCost.ToString();
                    ironImage.gameObject.SetActive(true);
                    break;
                case 2:
                    tooltipGoldCost.text = goldCost.ToString();
                    tooltipWoodCost.text = materialCost.ToString();
                    woodImage.gameObject.SetActive(true);
                    break;
                case 3:
                    tooltipGoldCost.text = goldCost.ToString();
                    tooltipLeatherCost.text = materialCost.ToString();
                    leatherImage.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
            tooltipGoldCost.text = goldCost.ToString();
            coinImage.gameObject.SetActive(true);
            tooltipTitle.text = title;
            tooltipText.text = shopMessage;
            tooltipGeneralText.text = "";
        }
        else if (isStats)
        {
            tooltipGeneralText.text = generalMessage;
            tooltipTitle.text = "";
            tooltipText.text = "";
        }
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
        tooltipText.text = string.Empty;
    }

/*    public override bool Equals(object obj)
    {
        return obj is TooltipManager manager &&
               base.Equals(obj) &&
               EqualityComparer<TextMeshProUGUI>.Default.Equals(tooltipGoldCost, manager.tooltipGoldCost);
    }*/
}
