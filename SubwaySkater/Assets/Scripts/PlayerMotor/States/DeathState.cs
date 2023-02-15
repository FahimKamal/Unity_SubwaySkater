using UnityEngine;

public class DeathState : BaseState
{
    [SerializeField] private Vector3 knockBackForce = new Vector3(0, 4, -3);
    private Vector3 _currentKnockBack;
    
    private static readonly int Death = Animator.StringToHash("Death");

    public override void Construct()
    {
        Motor.anim!.SetTrigger(Death);
        _currentKnockBack = knockBackForce;
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
        var m = _currentKnockBack;

        _currentKnockBack = new Vector3(
            0, 
            _currentKnockBack.y -= Motor.gravity * Time.deltaTime,
            _currentKnockBack.z += 2.0f * Time.deltaTime
            );
        if (_currentKnockBack.z > 0)
        {
            _currentKnockBack.z = 0;
            GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStateDeath>());
        }
        
        return _currentKnockBack;
    }
}
