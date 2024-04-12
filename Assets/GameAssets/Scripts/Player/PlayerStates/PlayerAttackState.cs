using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : State
{
    public override void Enter()
    {
        string animationToPlay = transitions[direction];
        animator.Play($"Attack{animationToPlay}");
    }

    public override void Do()
    {
        string animationToPlay = transitions[direction];
        animator.Play($"Attack{animationToPlay}");
        if(animator.GetCurrentAnimatorStateInfo(0).IsName($"Attack{animationToPlay}") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) isComplete = true;
    }

    public override void Exit()
    {

    }
}
