using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerDash : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float _distance;
    [SerializeField] private float _duration;
    [SerializeField] private Transform _cameraPivot;

    private Rigidbody _rigidbody;
    private bool _isDashAvailable = true;

    public event UnityAction<bool> DashActivityChanged;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
            StartCoroutine(Dash(_rigidbody.velocity.normalized, _distance, _duration));
    }

    private IEnumerator Dash(Vector3 direction, float distance, float duration)
    {
        DashActivityChanged?.Invoke(true);
        _isDashAvailable = false;
        float velocity = distance / duration;

        if (direction == Vector3.zero)
            direction = new Vector3(_cameraPivot.forward.x, 0, _cameraPivot.forward.z);

        while (duration > 0)
        {
            _rigidbody.velocity = direction * velocity;

            yield return null;

            duration -= Time.deltaTime;
        }

        DashActivityChanged?.Invoke(false);
        _isDashAvailable = true;
    }
}
