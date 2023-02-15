public class GameStateGame : GameState
{
    public override void Construct()
    {
        base.Construct();
        GameManager.Instance.motor.ResumePlayer();
        GameManager.Instance.ChangeCamera(GameCamera.Game);
    }

    public override void Destruct()
    {
        base.Destruct();
    }

    public override void UpdateState()
    {
        GameManager.Instance.worldGenerator.ScanPosition();
        GameManager.Instance.SceneChunkGenerator.ScanPosition();
    }
}
