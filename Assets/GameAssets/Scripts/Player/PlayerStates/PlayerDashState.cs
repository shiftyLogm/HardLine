using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : State
{
    public override void Enter()
    {
        animator.Play("DashRight");
    }

    public override void Do()
    {
        animator.Play("DashRight");
        Dash();
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("DashRight") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) isComplete = true;
    }

    public override void Exit()
    {

    }

    public void Dash()
    {
        rb.velocity = mov * entityStats.dashForce;
    }
}
