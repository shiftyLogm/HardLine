using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageClass : State
{
    public override void Enter()
    {
        entityStats.maxHp = 10;
        entityStats.moveSpeed = 160;
        entityStats.attackRange = 3;
        entityStats.attackDamage = 7;
        entityStats.dashForce = 7;
        entityStats.SetStatus();

        

        attackType = "mageAttack";
    }

    public override void Do()
    {
        
    }

    public override void Exit()
    {

    }
}
