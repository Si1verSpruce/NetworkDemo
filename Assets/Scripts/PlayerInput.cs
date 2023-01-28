using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions _input;
    private Vector2 _mouseDelta;
    private Vector2 _movementDirection;

    public event UnityAction DashInputted;

    public Vector2 MouseDelta => _mouseDelta;
    public Vector2 MovementDirection => _movementDirection;

    private void Awake()
    {
        _input = new PlayerInputActions();
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
        _mouseDelta = _input.Player.Look.ReadValue<Vector2>();
        _movementDirection = _input.Player.Move.ReadValue<Vector2>();
    }

    public void OnDashInput()
    {
        Debug.Log("dash!");
        DashInputted?.Invoke();
    }
}
