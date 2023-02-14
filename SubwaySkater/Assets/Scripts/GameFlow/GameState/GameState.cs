using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    protected GameManager Brain;
    private void Awake()
    {
        Brain = GetComponent<GameManager>();
    }

    public virtual void Construct() { }     // Will be called once we enter the state.
    public virtual void Destruct() { }      // Will be called once we leave the state.
    public virtual void UpdateState() { }    // Will be called continuously in update loop.
}
