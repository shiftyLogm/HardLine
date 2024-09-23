using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedClass : State
{
    public override void Enter()
    {
        entityStats.maxHp = 15;
        entityStats.moveSpeed = 180;
        entityStats.attackRange = 1;
        entityStats.attackDamage = 3.5f;
        entityStats.dashForce = 12;
        entityStats.SetStatus();

        

        attackType = "arrowAttack";
    }

    public override void Do()
    {
        
    }

    public override void Exit()
    {

    }
}
