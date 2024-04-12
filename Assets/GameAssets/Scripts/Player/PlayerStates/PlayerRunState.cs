using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : State
{
    
    public override void Enter()
    {
        string animationToPlay = transitions[direction];
        animator.Play($"Walk{animationToPlay}");
    }

    public override void Do()
    {
        string animationToPlay = transitions[direction];
        animator.Play($"Walk{animationToPlay}");
        
        if(rb.velocity == new Vector2(0,0))
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {
    
    }
}
