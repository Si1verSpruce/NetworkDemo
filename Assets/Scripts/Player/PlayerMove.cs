using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : NetworkBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private PlayerDash _dash;

    private Rigidbody _rigidbody;
    private float _velocity;
    private bool _isDashActive;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _velocity = MoveSpeedMultiplier.MultiplyMoveSpeedToVelocity(_moveSpeed);
    }

    private void OnEnable()
    {
        _dash.DashActivityChanged += OnDashActivityChanged;
    }

    private void OnDisable()
    {
        _dash.DashActivityChanged -= OnDashActivityChanged;
    }

    private void FixedUpdate()
    {
        if (_isDashActive)
            return;

        if (isLocalPlayer)
        {
            if (_input.MoveDirection != Vector2.zero)
                Move(_input.MoveDirection, _cameraPivot, _velocity);
            else
                _rigidbody.velocity = Vector3.zero;
        }
    }

    private void Move(Vector2 moveDelta, Transform pivot, float velocity)
    {
        Vector3 pivotForward = new Vector3(pivot.forward.x, 0, pivot.forward.z);
        Vector3 pivotRight = new Vector3(pivot.right.x, 0, pivot.right.z);
        _rigidbody.velocity = (pivotForward * moveDelta.y + pivotRight * moveDelta.x) * velocity * Time.deltaTime;
    }

    private void OnDashActivityChanged(bool isActive)
    {
        _isDashActive = isActive;
    }
}
