using UnityEngine;

public class DeathState : BaseState
{
    [SerializeField] private Vector3 knockBackForce = new Vector3(0, 4, -3);
    
    private static readonly int Death = Animator.StringToHash("Death");

    public override void Construct()
    {
        Motor.anim!.SetTrigger(Death);
    }

    public override void Destruct()
    {
        base.Destruct();
    }

    public override void Transition()
    {
        base.Transition();
    }

    public override Vector3 ProcessMotion()
    {
        var m = knockBackForce;

        knockBackForce = new Vector3(
            0, 
            knockBackForce.y -= Motor.gravity * Time.deltaTime,
            knockBackForce.z += 2.0f * Time.deltaTime
            );
        if (knockBackForce.z > 0)
        {
            knockBackForce.z = 0;
            GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStateDeath>());
        }
        
        return knockBackForce;
    }
}
