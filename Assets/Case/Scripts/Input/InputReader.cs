using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-8)]
public class InputReader : Singleton<InputReader>, PlayerInputActions.IPlayerActions
{
    public event Action OnPressInteractButtonEvent = () => { };

    [field: SerializeField] public Vector2 MoveInput { get; private set; }
    [field: SerializeField] public Vector2 LookInput { get; private set; }
    [field: SerializeField] public bool JumpInput { get; set; }


    private PlayerInputActions _playerInputActions;

    protected override void Awake()
    {
        base.Awake();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.SetCallbacks(this);
        _playerInputActions.Player.Enable();
        SetCursorState(true);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        OnPressInteractButtonEvent.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        JumpInput = context.performed;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}