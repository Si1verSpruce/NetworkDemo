using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControl : NetworkBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Camera _camera;
    [SerializeField, Range(-180, 180)] private float _minVerticalAngle;
    [SerializeField, Range(-180, 180)] private float _maxVerticalAngle;
    [SerializeField] private float _sensivity;

    private void Update()
    {
        if (_input.MoveDelta != Vector2.zero)
        {
            MoveCamera(_input.MoveDelta, _cameraPivot, _sensivity, _minVerticalAngle, _maxVerticalAngle);
            LookAtSameWithPivotDirection(_cameraPivot);
        }
    }

    private void MoveCamera(Vector2 moveDelta, Transform pivot, float moveSpeed, float minVerticalAngle, float maxVerticalAngle)
    {
        pivot.Rotate(new Vector3(-moveDelta.y, moveDelta.x, 0) * Time.deltaTime * moveSpeed);
        float clampedEulerX = Mathf.Clamp(TryGetNegativeAngle(pivot.localEulerAngles.x), minVerticalAngle, maxVerticalAngle);
        pivot.localEulerAngles = new Vector3(clampedEulerX, pivot.localEulerAngles.y, 0);
    }

    private void LookAtSameWithPivotDirection(Transform pivot)
    {
        var pivotRotation = pivot.rotation;
        transform.localEulerAngles = new Vector3(0, pivot.eulerAngles.y, 0);
        pivot.rotation = pivotRotation;
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
