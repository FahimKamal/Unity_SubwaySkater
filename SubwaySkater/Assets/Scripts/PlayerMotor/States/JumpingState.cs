
using UnityEngine;

public class JumpingState : BaseState
{
    public float jumpForce = 7.0f;
    public override void Construct()
    {
        Motor.verticalVelocity = jumpForce;
    }

    public override void Destruct()
    {
        
    }

    public override void Transition()
    {
        if (Motor.verticalVelocity < 0)
        {
            Motor.ChangeState(GetComponent<FallingState>());
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
