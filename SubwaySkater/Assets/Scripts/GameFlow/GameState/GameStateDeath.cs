using UnityEngine;

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
        Brain.ChangeState(GetComponent<GameStateGame>());
        GameManager.Instance.motor.RespawnPlayer();
    }
    public void ToMenu()
    {
        Brain.ChangeState(GetComponent<GameStateinit>());

        GameManager.Instance.motor.ResetPlayer();
        GameManager.Instance.worldGenerator.ResetWorld();
        GameManager.Instance.SceneChunkGenerator.ResetWorld();
    }
}
