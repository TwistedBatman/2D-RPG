using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public AttackState attackState;
    public bool isAttacking;

    public override State RunCurrentState()
    {
        if (isAttacking)
        {
            return attackState;
        }
        else
        {
            return this;
        }
    }

}
