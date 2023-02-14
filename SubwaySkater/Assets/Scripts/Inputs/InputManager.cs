using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    // Action Schemes
    private RunnerInputAction _actionScheme;
    
    // Configuration
    /// <summary>
    /// Minimum lenght to swipe to consider it a swipe.
    /// </summary>
    [SerializeField] private float sqrSwipeDeadZone = 50.0f;

    #region Private properties

    private Vector2 _startDrag;

    #endregion

    #region Public Properties

    public bool Tap { get; private set; }
    public bool SwipeLeft { get; private set; }
    public bool SwipeRight { get; private set; }
    public bool SwipeUp { get; private set; }
    public bool SwipeDown { get; private set; }
    public Vector2 TouchPosition { get; private set; }

    #endregion

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SetupControl();
    }

    private void LateUpdate()
    {
        ResetInputs();
    }

    private void ResetInputs()
    {
        Tap = SwipeLeft = SwipeRight = SwipeDown = SwipeUp = false;
    }

    private void SetupControl()
    {
        _actionScheme = new RunnerInputAction();
        
        // Register different actions
        _actionScheme.Gameplay.Tap.performed += OnTap;
        _actionScheme.Gameplay.TouchPosition.performed += OnPosition;
        _actionScheme.Gameplay.StartDrag.performed += OnStartDrag;
        _actionScheme.Gameplay.EndDrag.performed += OnEndDrag;
    }

    private void OnEndDrag(InputAction.CallbackContext ctx)
    {
        var delta = TouchPosition - _startDrag;
        var sqrDistance = delta.sqrMagnitude;

        if (sqrDistance > sqrSwipeDeadZone)
        {
            var x = MathF.Abs(delta.x);
            var y = MathF.Abs(delta.y);

            if (x > y) // Left or Right
            {
                if (delta.x > 0)
                    SwipeRight = true;
                else
                    SwipeLeft = true;
            }
            else // Up or Down
            {
                if (delta.y > 0)
                    SwipeUp = true;
                else
                    SwipeDown = true;
            }
        }

        _startDrag = Vector2.zero;
    }

    private void OnStartDrag(InputAction.CallbackContext ctx)
    {
        _startDrag = TouchPosition;
    }

    private void OnPosition(InputAction.CallbackContext ctx)
    {
        TouchPosition = ctx.ReadValue<Vector2>();
    }

    private void OnTap(InputAction.CallbackContext ctx)
    {
        Tap = true;
    }

    private void OnEnable()
    {
        _actionScheme.Enable();
    }

    private void OnDisable()
    {
        _actionScheme.Disable();
    }
}
