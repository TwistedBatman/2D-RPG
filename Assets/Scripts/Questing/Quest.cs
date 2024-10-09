using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Quest
{
    //public bool isActive;

    public string title;
    public string description;
    public string targetName;
    public int experienceReward;
    public int goldReward;
    public Sprite targetImage;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI nameText;
    public Image image;

    public Goal goal;
    public Quest1 quest1;
    public Quest2 quest2;


/*    public void Complete()
    {
        //isActive = false;
        quest1.GiveRewards();
        Debug.Log(title + " was completed.");
    }*/

}

/*public List<Goal> goals = new List<Goal>();
public string questName;
public string description;
public int expReward;
public bool completed;

public void CheckGoals()
{
    // Using the Linq like an sql querry to check if all goals are complted
    completed = goals.All(g => g.completed); // Checks all goals, returns true if all completed
    if (completed)
        GiveReward();
}

void GiveReward()
{
    Debug.Log("Completed Quest!");
}*/
