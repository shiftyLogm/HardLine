using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State
{
    public override void Enter()
    {
        string animationToPlay = transitions[direction];
        animator.Play($"Idle{animationToPlay}");
    }

    public override void Do()
    {
        string animationToPlay = transitions[direction];
        animator.Play($"Idle{animationToPlay}");
        
        if(rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {
    
    }
}
