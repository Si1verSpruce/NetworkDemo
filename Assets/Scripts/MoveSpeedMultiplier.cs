using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoveSpeedMultiplier
{
    public static readonly float MoveSpeedToVelocityMultiplier = 50f;

    public static float MultiplyMoveSpeedToVelocity(float moveSpeed)
    {
        return MoveSpeedToVelocityMultiplier * moveSpeed;
    }
}
