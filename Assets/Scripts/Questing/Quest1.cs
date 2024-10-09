using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest1 : MonoBehaviour, IDataPersistence
{
    public bool isActive;
    public bool completed;
    public Quest quest;
    public string enemyTag;
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
        text = "Kill " + quest.goal.requiredAmount + " " + enemyTag + ".   ";
        questText.text = text;
    }

    public void GiveRewards()
    {
        FindObjectOfType<PlayerEXP>().ExpToGive(quest.experienceReward);
        FindObjectOfType<CoinsAndGatherables>().AddCoins(quest.goldReward);
        questText.text = "";
        completed = true;
    }

    public void IncreaseEnemiesKilled(string tag)
    {
        if (!completed)
        {
            quest.goal.EnemyKilled(tag); // Increase the enemies killed
            questText.text = text + quest.goal.currentAmount + "/" + quest.goal.requiredAmount;
            if (quest.goal.IsReached()) // Check if the quest goal is reached
            {
                SetRewards();
                npc.questCompleted = true;
                questText.text =  text + "Completed";
            }
        }
    }

    public void SetRewards()
    {
        goldRewardText.text = quest.goldReward.ToString();
        EXPRewardText.text = quest.experienceReward.ToString() + " Experience"; ;
    }

    public void LoadData(GameData data)
    {
        this.completed = data.quest1Completed;
        if (data.quest1Completed)
            npc.DisableQuestNPC();
    }

    public void SaveData(GameData data)
    {
        data.quest1Completed = this.completed;
    }
}
