using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest2 : MonoBehaviour, IDataPersistence
{
    public bool isActive;
    public bool completed;
    public Quest quest;
    public string itemTag;
    public TextMeshProUGUI questText;
    public TextMeshProUGUI goldRewardText;
    public TextMeshProUGUI EXPRewardText;
    public NPC npc;
    //public Dialogue dialogue;
    string text;
    //public PlayerEXP player;

    private void Start()
    {
        quest.titleText.text = quest.title;
        quest.infoText.text = quest.description;
        quest.nameText.text = quest.targetName;
        quest.image.sprite = quest.targetImage;

    }

    public void AcceptQuest()
    {
        npc.isActive = true;
        isActive = true;
        text = "Gather " + quest.goal.requiredAmount + " " + itemTag + ".   ";
        questText.text = text;
    }

    public void GiveRewards()
    {
        FindObjectOfType<InventoryManager>().RemoveResourceFromInventory(quest.targetName, quest.goal.requiredAmount);
        FindObjectOfType<PlayerEXP>().ExpToGive(quest.experienceReward);
        FindObjectOfType<CoinsAndGatherables>().AddCoins(quest.goldReward);
        questText.text = "";
        completed = true;
    }

    public void IncreaseItemsGathered()
    {
        if (!completed)
        {
            quest.goal.ItemGathered(); // Increase the items gathered
            if (isActive)
                questText.text = text + quest.goal.currentAmount + "/" + quest.goal.requiredAmount;
            if (quest.goal.IsReached()) // Check if the quest goal is reached
            {
                SetRewards();
                npc.questCompleted = true;
                if (isActive)
                    questText.text = text + "Completed";
            }
        }
    }

    public void DecreaseItemsGathered()
    {
        if (!completed && isActive)
        {
            quest.goal.ItemUsed();
            npc.questCompleted = quest.goal.IsReached();
        }
    }

    public void SetRewards()
    {
        goldRewardText.text = quest.goldReward.ToString();
        EXPRewardText.text = quest.experienceReward.ToString() + " Experience";
    }

    public void LoadData(GameData data)
    {
        this.completed = data.quest2Completed;
        if (data.quest2Completed)
            npc.DisableQuestNPC();
    }

    public void SaveData(GameData data)
    {
        data.quest2Completed = this.completed;
    }
}
