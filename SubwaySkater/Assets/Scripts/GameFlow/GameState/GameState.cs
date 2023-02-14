using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    protected GameManager Brain;
    private void Awake()
    {
        Brain = GetComponent<GameManager>();
    }

    /// <summary>
    /// // Will be called once we enter the state.
    /// </summary>
    public virtual void Construct()
    {
        Debug.Log("Constructing : " + this);
    }    
    
    /// <summary>
    /// Will be called once we leave the state.
    /// </summary>
    public virtual void Destruct() { }    
    
    /// <summary>
    /// Will be called continuously in update loop.
    /// </summary>
    public virtual void UpdateState() { }   
}
