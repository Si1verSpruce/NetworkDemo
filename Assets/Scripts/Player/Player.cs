using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Player : NetworkBehaviour
{
    [SerializeField] private float _setYourColorDelay;

    private Renderer _renderer;
    private string _name;
    private Color _color;
    private bool _isRecolorInvulnerable;
    private bool _isNotInitialized = true;

    public string Name => _name;
    public Color ThisColor => _color;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Init(string name, Color color)
    {
        if (_isNotInitialized)
        {
            _name = name;
            _renderer.material.color = color;
            _color = _renderer.material.color;
            _isNotInitialized = true;
        }
    }

    public void ApplyColor(Color color)
    {
        if (_isRecolorInvulnerable == false)
        {
            _renderer.material.color = color;
            _isRecolorInvulnerable = true;
            StartCoroutine(SetYourColorAfterDelay(_setYourColorDelay));
        }
    }

    private IEnumerator SetYourColorAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);

        _isRecolorInvulnerable = false;
        _renderer.material.color = _color;
    }
}
