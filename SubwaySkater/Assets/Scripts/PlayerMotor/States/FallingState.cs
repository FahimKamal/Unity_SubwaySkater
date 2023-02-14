
using UnityEngine;

public class FallingState : BaseState
{
    private static readonly int Fall = Animator.StringToHash("Fall");

    public override void Construct()
    {
        Motor.anim!.SetTrigger(Fall);
    }

    public override void Transition()
    {
        if (Motor.isGrounded)
        {
            Motor.ChangeState(gameObject.GetComponent<RunningState>());
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
