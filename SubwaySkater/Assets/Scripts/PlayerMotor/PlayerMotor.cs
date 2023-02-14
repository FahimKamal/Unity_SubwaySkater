using System;
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
    public Animator anim;

    private BaseState _state;
    private bool _isPaused = true;
    
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        _state = GetComponent<RunningState>();
        _state.Construct();
    }

    private void Update()
    {
        if (!_isPaused)
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
        
        // Feed animator necessary values
        anim.SetBool(IsGrounded, isGrounded);
        anim.SetFloat(Speed, Mathf.Abs(moveVector.z));
        
        // Move the player
        controller.Move(moveVector * Time.deltaTime);
    }

    public float SnapToLane()
    {
        var r = 0.0f;
        
        if (transform.position.x != (currentLane * distanceInBetweenLanes)) // If we\re not directly on top of a lane
        {
            var deltaToDesiredPosition = (currentLane * distanceInBetweenLanes) - transform.position.x;
            r = (deltaToDesiredPosition > 0) ? 1 : -1;
            r *= baseSideWaySpeed;

            var actualDistance = r * Time.deltaTime;
            if (Mathf.Abs(actualDistance) > Mathf.Abs(deltaToDesiredPosition))
            {
                r = deltaToDesiredPosition * (1 / Time.deltaTime);
            }
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

    public void ApplyGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;
        if (verticalVelocity < -terminalVelocity)
        {
            verticalVelocity = -terminalVelocity;
        }
    }

    public void PausePlayer()
    {
        _isPaused = true;
    }

    public void ResumePlayer()
    {
        _isPaused = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var hitLayerName = LayerMask.LayerToName(hit.gameObject.layer);

        if (hitLayerName == "Death")
        {
                ChangeState(GetComponent<DeathState>());
        }
    }

    public void RespawnPlayer()
    { 
        ChangeState(GetComponent<RespawnState>());
    }
}
