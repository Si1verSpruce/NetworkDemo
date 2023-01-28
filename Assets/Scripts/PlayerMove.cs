using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _cameraPivot;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_input.MoveDirection != Vector2.zero)
            Move(_input.MoveDirection, _cameraPivot, _moveSpeed);
        else
            _rigidbody.velocity = Vector2.zero;
    }

    private void Move(Vector2 moveDelta, Transform pivot, float moveSpeed)
    {
        Vector3 pivotForward = new Vector3(pivot.forward.x, 0, pivot.forward.z);
        Vector3 pivotRight = new Vector3(pivot.right.x, 0, pivot.right.z);
        _rigidbody.velocity = (pivotForward * moveDelta.y + pivotRight * moveDelta.x) * moveSpeed * Time.deltaTime;
    }
}
