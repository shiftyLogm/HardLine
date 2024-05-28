using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class PlayerDashState : State
{
    private bool _canDash = false;
    public override void Enter()
    {
        animator.Play("DASHRIGHT");
        _canDash = true;
    }

    public override void Do()
    {
        animator.Play("DASHRIGHT");
        if(_canDash) Dash();
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("DASHRIGHT") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) isComplete = true;
    }

    public override void Exit()
    {

    }

    public void Dash()
    {   
        _canDash = false;
        mov.Normalize();
        Debug.Log(mov);
        rb.AddForce(mov * (entityStats.dashForce/10000), ForceMode2D.Impulse);
    }
}
