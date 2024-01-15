using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    PlayerInput PlayerInput;

    public Vector2 MoveInput { get; private set; } 
    public bool isThrowPressed { get; private set; }

    InputAction _moveAction;
    InputAction _throwAction;

    private void Start() 
    {
        PlayerInput = GetComponent<PlayerInput>();
        _moveAction = PlayerInput.actions["Move"];
        _throwAction = PlayerInput.actions["Throw"];
    }

    private void Update() 
    {
        MoveInput = _moveAction.ReadValue<Vector2>();
        isThrowPressed = _throwAction.WasPressedThisFrame();
    }
}
