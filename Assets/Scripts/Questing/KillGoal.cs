using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal
{

/*    public KillGoal(string enemyName, string enemyDescription, bool goalCompleted, int currentAmount, int requiredAmount)
    {
        this.Name = enemyName;
        description = enemyDescription;
        completed = goalCompleted;
        curAmount = currentAmount;
        reqAmount = requiredAmount;
        Debug.Log("enemy " + enemyName);
        Debug.Log("this " + Name);
    }

*//*    public override void Init()
    {
        //base.Init();
        Debug.Log("kill " + reqAmount + " " +  name);

    }*//*

    public void EnemyDied(string enemyName)
    {
       // KillGoal()
        Debug.Log("eee" + enemyName);
        Debug.Log("eeethi" + Name);
        Debug.Log("eeethis" + this.Name);
        if (string.Equals(this.name, enemyName))
        {
            curAmount++;
            Debug.Log("You killed " + curAmount);
            Evaluate();
        }

    }*/
}
