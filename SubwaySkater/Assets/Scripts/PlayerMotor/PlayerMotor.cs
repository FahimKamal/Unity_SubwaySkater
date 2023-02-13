using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [HideInInspector] public Vector3 moveVector;
    [HideInInspector] public float verticalVelocity;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public int currentLane;

    public float distanceInBetweenLanes = 3.0f;
    public float baseRunSpeed = 5.0f;
    public float baseSideWaySpeed = 10.0f;
    public float gravity = 14.0f;
    public float terminalVelocity = 20.0f;

    public CharacterController controller;

    private BaseState _state;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        _state = GetComponent<RunningState>();
        _state.Construct();
    }

    private void Update()
    {
        UpdateMotor();
    }

    private void UpdateMotor()
    {
        // Check if we're grounded
        isGrounded = controller.isGrounded;
        
        // How should we be moving right? based on state
        moveVector = _state.ProcessMotion();
        
        // are we trying to change state?
        _state.Transition();
        
        // Move the player
        controller.Move(moveVector * Time.deltaTime);
    }

    public float SnapToLane()
    {
        var r = 0.0f;

        if (transform.position.x != (currentLane * distanceInBetweenLanes)) // If we\re not directly on top of a lane
        {
            
        }
        else
        {
            r = 0.0f;
        }
        
        return r;
    }

    public void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
    }

    public void ChangeState(BaseState s)
    {
        _state.Destruct();
        _state = s;
        _state.Construct();
    }
}
