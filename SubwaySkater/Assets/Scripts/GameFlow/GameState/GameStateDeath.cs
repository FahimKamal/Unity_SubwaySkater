public class GameStateDeath : GameState
{
    public override void Construct()
    {
        base.Construct();
    }

    public override void Destruct()
    {
        base.Destruct();
    }

    public override void UpdateState()
    {
        if (InputManager.Instance.SwipeDown)
        {
            ToMenu();
        }

        if (InputManager.Instance.SwipeUp)
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        
    }
    public void ToMenu()
    {
        
    }
}
