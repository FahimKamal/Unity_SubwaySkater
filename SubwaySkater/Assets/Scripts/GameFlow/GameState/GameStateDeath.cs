public class GameStateDeath : GameState
{
    public override void Construct()
    {
        base.Construct();
        GameManager.Instance.motor.PausePlayer();
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
            RespawnGame();
        }
    }

    public void RespawnGame()
    {
        GameManager.Instance.motor.RespawnPlayer();
        Brain.ChangeState(GetComponent<GameStateGame>());
    }
    public void ToMenu()
    {
        Brain.ChangeState(GetComponent<GameStateinit>());
    }
}
