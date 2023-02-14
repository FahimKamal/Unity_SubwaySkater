using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    public PlayerMotor motor;
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
}
