using UnityEngine;
using UnityEngine.Serialization;

public class RespawnState : BaseState
{
    [SerializeField] private float verticalDistance = 25.0f;
    [SerializeField] private float immunityTime = 1f;

    private float _startTime;
    
    private static readonly int Respawn = Animator.StringToHash("Respawn");

    public override void Construct()
    {
        _startTime = Time.time;
        
        Motor.controller.enabled = false;
        Motor.transform.position = new Vector3(0, verticalDistance, Motor.transform.position.z);
        Motor.controller.enabled = true;

        Motor.verticalVelocity = 0.0f;
        Motor.currentLane = 0;
        Motor.anim.SetTrigger(Respawn);
    }

    public override void Destruct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Game);
    }

    public override void Transition()
    {
        if ((Time.time - _startTime) > immunityTime && Motor.isGrounded)
        {
            Motor.ChangeState(GetComponent<RunningState>());
        }
        
        if (InputManager.Instance.SwipeLeft)
        {
            // Change lane, go left
            Motor.ChangeLane(-1);
        }

        if (InputManager.Instance.SwipeRight)
        {
            // Change lane, go right
            Motor.ChangeLane(1);
        }
    }

    public override Vector3 ProcessMotion()
    {
        // Apply Gravity
        Motor.ApplyGravity();
        
        // Create our return vector
        var m = Vector3.zero;
        m.x = Motor.SnapToLane();
        m.y = Motor.verticalVelocity;
        m.z = Motor.baseRunSpeed;

        return m;
    }
}
