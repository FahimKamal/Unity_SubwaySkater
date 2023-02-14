using UnityEngine;

public class RunningState : BaseState
{
    public override void Construct()
    {
        Motor.verticalVelocity = 0;
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
        if (InputManager.Instance.SwipeUp && Motor.isGrounded)
        {
            // Change to jumping state
            Motor.ChangeState(GetComponent<JumpingState>());
        }

        if (!Motor.isGrounded)
        {
            Motor.ChangeState(GetComponent<FallingState>());
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
