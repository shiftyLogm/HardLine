using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class PlayerDashState : State
{
    private bool _canDash = false;
    public override void Enter()
    {

        animationToPlay = directions[direction];
        animator.Play($"DASH{animationToPlay}");
        _canDash = true;

    }

    public override void Do()
    {

        animator.Play($"DASH{animationToPlay}");
        entityStats.invencible = true;
        if(_canDash) Dash();
        if(animator.GetCurrentAnimatorStateInfo(0).IsName($"DASH{animationToPlay}") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) isComplete = true;

    }

    public override void Exit()
    {
        entityStats.invencible = false;
    }

    public void Dash()
    {   
        _canDash = false;
        mov.Normalize();
        Debug.Log(mov);
        rb.AddForce(mov * (entityStats.dashForce/100), ForceMode2D.Impulse);
    }
}
