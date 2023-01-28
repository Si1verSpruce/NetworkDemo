using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerDash : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float _distance;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _cameraPivot;

    private Rigidbody _rigidbody;
    private bool _isDashAvailable = true;
    private bool _isNotCollided;
    private float _velocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _velocity = MoveSpeedMultiplier.MultiplyMoveSpeedToVelocity(_moveSpeed);
    }

    private void OnEnable()
    {
        _input.DashInputted += TryToDash;
    }

    private void OnDisable()
    {
        _input.DashInputted -= TryToDash;
    }

    private void TryToDash()
    {
        if (_isDashAvailable)
            StartCoroutine(Dash(_rigidbody.velocity.normalized, _distance, _velocity));
    }

    private IEnumerator Dash(Vector3 direction, float distance, float velocity)
    {
        _isDashAvailable = false;
        _isNotCollided = true;
        Vector3 targetPosition;

        if (direction == Vector3.zero)
            direction = new Vector3(_cameraPivot.forward.x, 0, _cameraPivot.forward.z);

        targetPosition = transform.position + direction * distance;

        Debug.Log(direction);
        Debug.Log(velocity);

        while (transform.position != targetPosition && _isNotCollided)
        {
            //transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            _rigidbody.velocity = direction * velocity * Time.deltaTime;

            yield return null;
        }

        _isDashAvailable = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isNotCollided = false;
    }
}
