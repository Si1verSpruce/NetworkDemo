using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerDash : NetworkBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float _distance;
    [SerializeField] private float _duration;
    [SerializeField] private float _cooldown;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Player _player;

    private Rigidbody _rigidbody;
    private bool _isDashAvailable = true;
    private bool _isDashing;

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
        _isDashing = true;
        _rigidbody.velocity = Vector3.zero;
        float velocity = distance / duration;

        if (direction == Vector3.zero)
            direction = new Vector3(_cameraPivot.forward.x, 0, _cameraPivot.forward.z);

        while (duration > 0)
        {
            _rigidbody.velocity = direction * velocity;

            yield return null;

            duration -= Time.deltaTime;
        }

        _isDashing = false;
        DashActivityChanged?.Invoke(false);
        StartCoroutine(CountCooldown(_cooldown));
    }

    private IEnumerator CountCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);

        _isDashAvailable = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isDashing)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
                player.ApplyColor(_player.ThisColor, _player);
        }
    }
}
