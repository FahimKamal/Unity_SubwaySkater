using UnityEngine;

public class RespawnState : BaseState
{
    public override void Construct()
    {
        Debug.Log("Player has respawned");
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
        return base.ProcessMotion();
    }
}
