using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFinisherState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        //Attack
        attackIndex = 3;
        duration = 0.75f;
        animator.SetTrigger("Attack" + attackIndex);
        Debug.Log($"Player attack {attackIndex} fired");
    }

    public override void OnUpdate()
    {
        if(fixedTime >= duration)
        {
            stateMachine.SetNextStateToMain();
        }
    }
}
