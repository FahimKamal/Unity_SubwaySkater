using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum GameCamera
{
    Init, Game, Shop, Respawn
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    public PlayerMotor motor;
    public WorldGenerator worldGenerator;
    public SceneChunkGenerator SceneChunkGenerator;

    public GameObject[] cameras;
    
    private GameState _state;
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
        _state = GetComponent<GameStateinit>();
        _state.Construct();
    }

    private void Update()
    {
        _state.UpdateState();
    }

    public void ChangeState(GameState s)
    {
        _state.Destruct();
        _state = s;
        _state.Construct();
    }

    public void ChangeCamera(GameCamera c)
    {
        foreach (var go in cameras)
        {
            go.SetActive(false);
        }
        cameras[(int)c].SetActive(true);
    }
}
