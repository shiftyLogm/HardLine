using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBaseState : State
{
    // How long this state should be active before moving on
    public float duration;
    protected Animator animator;
    // Bool to check whether or not the next attack in the sequence should be played or not
    protected bool shouldCombo;
    // The attack index in the sequence of attacks
    protected int attackIndex;

    public override void OnEnter(StateMachine _stateMachine)
    {
        animator = GetComponent<Animator>();
    }

    public override void OnUpdate()
    {
        shouldCombo = true;
        Debug.Log("meleebasestate");
    }

    public override void OnExit()
    {
        
    }
}
