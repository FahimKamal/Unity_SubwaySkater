public class GameStateGame : GameState
{
    public override void Construct()
    {
        base.Construct();
        GameManager.Instance.motor.ResumePlayer();
    }

    public override void Destruct()
    {
        base.Destruct();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
