using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : NetworkBehaviour
{
    private PlayerInputActions _input;
    private Vector2 _lookDelta;
    private Vector2 _moveDirection;

    public event UnityAction DashInputted;

    public Vector2 MoveDelta => _lookDelta;
    public Vector2 MoveDirection => _moveDirection;

    private void Awake()
    {
        _input = new PlayerInputActions();
    }

    public override void OnStartClient()
    {

    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Player.Dash.performed += ctx => OnDashInput();
    }

    private void OnDisable()
    {
        _input.Disable();

        _input.Player.Dash.performed -= ctx => OnDashInput();
    }

    private void Update()
    {
        if (isOwned)
        {
            _lookDelta = _input.Player.Look.ReadValue<Vector2>();
            _moveDirection = _input.Player.Move.ReadValue<Vector2>();
        }
    }

    public void OnDashInput()
    {
        if (isOwned)
            DashInputted?.Invoke();
    }
}
