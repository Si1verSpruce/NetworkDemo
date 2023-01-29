using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _setYourColorDelay;

    private Renderer _renderer;
    private Color _color;
    private bool _isRecolorInvulnerable;
    private bool _isColorSet;

    public Color ThisColor => _color;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Init(Color color)
    {
        if (_isColorSet == false)
        {
            _renderer.material.color = color;
            _color = _renderer.material.color;
            _isColorSet = true;
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
