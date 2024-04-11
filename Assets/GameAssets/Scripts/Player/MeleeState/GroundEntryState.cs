using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEntryState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {

        //Attack
        attackIndex = 1;
        duration = 0.5f;
        if(shouldCombo) Debug.Log("shouldcombo");
        else Debug.Log("notshould");
        animator.SetTrigger("Attack" + attackIndex);
        Debug.Log($"Player attack {attackIndex} fired");
    }

    public override void OnUpdate()
    {

        if(fixedTime >= duration)
        {
            if(shouldCombo)
            {
                stateMachine.SetNextState(new GroundComboState());
            }
            else
            {
                stateMachine.SetNextStateToMain();
            }
        }
    }
}
