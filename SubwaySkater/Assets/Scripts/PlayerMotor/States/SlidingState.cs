
using UnityEngine;

public class SlidingState : BaseState
{
    public float slideDuration = 1.0f;
    
    // Collider logic
    private Vector3 _initialCenter;
    private float _initialSize;
    private float _slideStart;
    private static readonly int Slide = Animator.StringToHash("Slide");
    private static readonly int Running = Animator.StringToHash("Running");

    public override void Construct()
    {
        Motor.anim!.SetTrigger(Slide);
        _slideStart = Time.time;

        _initialSize = Motor.controller.height;
        _initialCenter = Motor.controller.center;

        Motor.controller.height = _initialSize * 0.5f;
        Motor.controller.center = _initialCenter * 0.5f;
    }

    public override void Destruct()
    {
        Motor.anim!.SetTrigger(Running);
        Motor.controller.height = _initialSize;
        Motor.controller.center = _initialCenter;
    }

    public override void Transition()
    {
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
        
        if (!Motor.isGrounded)
        {
            Motor.ChangeState(GetComponent<FallingState>());
        }
        
        if (InputManager.Instance.SwipeUp)
        {
            Motor.ChangeState(GetComponent<JumpingState>());
        }
        
        if (Time.time - _slideStart > slideDuration)
        {
            Motor.ChangeState(GetComponent<RunningState>());
        }
    }

    public override Vector3 ProcessMotion()
    {
        var m = Vector3.zero;

        m.x = Motor.SnapToLane();
        m.y = -1.0f;
        m.z = Motor.baseRunSpeed;

        return m;
    }
}
