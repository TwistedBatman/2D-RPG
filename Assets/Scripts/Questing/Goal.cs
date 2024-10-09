using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Goal
{
    public GoalType goalType;
    public Quest1 quest1;
    public Quest2 quest2;

    public int requiredAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled(string tag)
    {
        if (goalType == GoalType.Kill) // check the tag for enemies
        {
            if (Equals(quest1.enemyTag, tag))
            {
                currentAmount++;
                Debug.Log("has killed " + currentAmount);
            }
        }
    }    
    public void ItemGathered()
    {
        if (goalType == GoalType.Gathering) // check the tag for type 
        {
            currentAmount++;
            /*if (count >= 0)
                currentAmount += count;*/
        }
    }

    public void ItemUsed()
    {
        if (goalType == GoalType.Gathering) // check the tag for type 
        {
            currentAmount--;
            Debug.Log("has used");
        }
    }

}

public enum GoalType
{
    Kill,
    Gathering
}


/*    public string Name;
    public string description;
    public bool completed;
    public int curAmount;
    public int reqAmount;

    public virtual void Init()
    {
        //Debug.Log("Begining Quest");
    }

    public void Evaluate()
    {
        if (curAmount >= reqAmount)
            Complete();
    }

    public void Complete()
    {
        completed = true;
    }*/
