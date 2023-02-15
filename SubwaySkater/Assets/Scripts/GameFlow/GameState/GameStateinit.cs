
public class GameStateinit : GameState
{
    public override void Construct()
    {
        base.Construct();
        GameManager.Instance.ChangeCamera(GameCamera.Init);
    }

    public override void Destruct()
    {
        base.Destruct();
    }

    public override void UpdateState()
    {
        if (InputManager.Instance.Tap)
        {
            Brain.ChangeState(GetComponent<GameStateGame>());
        }
    }
}
