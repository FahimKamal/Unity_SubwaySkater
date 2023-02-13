using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected PlayerMotor Motor;

    private void Awake()
    {
        Motor = GetComponent<PlayerMotor>();
    }

    public virtual void Construct() { }     // Will be called once we enter the state.
    public virtual void Destruct() { }      // Will be called once we leave the state.
    public virtual void Transition() { }    // Will be called continuously in update loop.

    public virtual Vector3 ProcessMotion()
    {
        Debug.Log("Process motion is not implemented in " + this.ToString());
        return Vector3.zero;
    }
}
