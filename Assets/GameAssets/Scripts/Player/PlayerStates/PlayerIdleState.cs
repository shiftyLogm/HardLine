using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State
{

    public override void Enter()
    {
        if(direction != "")
        {
            string animationToPlay = transitions[direction];
            animator.Play($"Idle{animationToPlay}");
        }
    }

    public override void Do()
    {
        if(direction != "")
        {
            string animationToPlay = transitions[direction];
            animator.Play($"Idle{animationToPlay}");
        }

        
    }

    public override void Exit()
    {
    
    }
}
