public class IdleState : State
{

    public override void Enter()
    {
        if(direction != "")
        {
            animationToPlay = directions[direction];
            animator.Play($"IDLE{animationToPlay}");
        }
    }

    public override void Do()
    {
        if(direction != "")
        {
            animationToPlay = directions[direction];
            animator.Play($"IDLE{animationToPlay}");
        }
  
    }

    public override void Exit()
    {
    
    }
}
