using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public override void Enter()
    {
        rb.velocity = new Vector2(0,0);

        string animationToPlay = directions[direction];
        animator.Play($"Attack{animationToPlay}");
    }

    public override void Do()
    {
        rb.velocity = new Vector2(0,0);

        string animationToPlay = directions[direction];
        animator.Play($"Attack{animationToPlay}");
        if(animator.GetCurrentAnimatorStateInfo(0).IsName($"Attack{animationToPlay}") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) isComplete = true;
    }

    public override void Exit()
    {

    }
}
