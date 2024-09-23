using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorClass : State
{
    public override void Enter()
    {
        entityStats.maxHp = 20;
        entityStats.moveSpeed = 150;
        entityStats.attackRange = 0.35f;
        entityStats.attackDamage = 3;
        entityStats.dashForce = 10;
        entityStats.SetStatus();

        

        attackType = "meleeAttack";
    }

    public override void Do()
    {
        
    }

    public override void Exit()
    {

    }
}
