using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorClass : State
{
    public override void Enter()
    {
        entityStats.maxHp = 20;
        entityStats.SetHp();

        entityStats.moveSpeed = 300;
        entityStats.attackRange = 0.35f;
        entityStats.attackDamage = 5;
        entityStats.dashForce = 10;
    }

    public override void Do()
    {
        
    }

    public override void Exit()
    {

    }
}