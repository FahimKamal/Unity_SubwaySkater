
using UnityEngine;

public class FallingState : BaseState
{
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
