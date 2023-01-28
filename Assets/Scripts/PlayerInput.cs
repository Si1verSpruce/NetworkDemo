using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions _input;
    private Vector2 _mouseDelta;
    private Vector2 _movementDirection;

    public Vector2 MouseDelta => _mouseDelta;
    public Vector2 MovementDirection => _movementDirection;

    private void Awake()
    {
        _input = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        _mouseDelta = _input.Player.Look.ReadValue<Vector2>();
        _movementDirection = _input.Player.Move.ReadValue<Vector2>();
    }
}
