using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
/*    public AttackState attackState;
    public bool isInAttackRange;*/
    public override State RunCurrentState()
    {
        /*        if (isInAttackRange)
                {
                    return attackState;
                }
                else
                {
                    return this;
                }*/
        Debug.Log("Attacking");
        return this;
    }
}
