using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : State
{
    public override void Enter()
    {
        animator.Play("DASHRIGHT");
    }

    public override void Do()
    {
        animator.Play("DASHRIGHT");
        Dash();
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("DASHRIGHT") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) isComplete = true;
    }

    public override void Exit()
    {

    }

    public void Dash()
    {
        rb.velocity = mov * entityStats.dashForce;
    }
}
