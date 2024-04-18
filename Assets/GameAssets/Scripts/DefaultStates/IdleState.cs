public class IdleState : State
{

    public override void Enter()
    {
        if(direction != "")
        {
            string animationToPlay = directions[direction];
            animator.Play($"Idle{animationToPlay}");
        }
    }

    public override void Do()
    {
        if(direction != "")
        {
            string animationToPlay = directions[direction];
            animator.Play($"Idle{animationToPlay}");
        }
  
    }

    public override void Exit()
    {
    
    }
}
