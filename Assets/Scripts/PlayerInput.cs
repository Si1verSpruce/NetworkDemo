using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions _input;


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
        Debug.Log(_input.Player.Look.ReadValue<Vector2>());
    }
}
