using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField, Range(-180, 180)] private float _minVerticalAngle;
    [SerializeField, Range(-180, 180)] private float _maxVerticalAngle;
    [SerializeField] private float _sensivity;

    private void Update()
    {
        if (_input.MoveDelta != Vector2.zero)
            MoveCamera(_input.MoveDelta);
    }

    private void MoveCamera(Vector2 moveDelta)
    {
        _cameraPivot.Rotate(new Vector3(-moveDelta.y, moveDelta.x, 0) * Time.deltaTime * _sensivity);
        float clampedEulerX = Mathf.Clamp(TryGetNegativeAngle(_cameraPivot.localEulerAngles.x), _minVerticalAngle, _maxVerticalAngle);
        _cameraPivot.localEulerAngles = new Vector3(clampedEulerX, _cameraPivot.localEulerAngles.y, 0);
    }

    private float TryGetNegativeAngle(float angle)
    {
        float circleAngle = 360;
        float halfCircleAngle = circleAngle / 2;

        if (angle > halfCircleAngle)
            return angle - circleAngle;
        else
            return angle;
    }
}
