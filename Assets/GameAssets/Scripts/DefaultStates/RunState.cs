public class RunState : State
{
    
    public override void Enter()
    {
        if(direction != "")
        {
            string animationToPlay = directions[direction];
            animator.Play($"Walk{animationToPlay}");
        }
    }

    public override void Do()
    {
        if(direction != "")
        {
            string animationToPlay = directions[direction];
            animator.Play($"Walk{animationToPlay}");
        }
        
    }

    public override void Exit()
    {
    
    }
}
