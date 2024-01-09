using System;
using UnityEngine;

[Serializable]
public class SlopeData
{
    [field: SerializeField] [field: Range(0f, 80f)]
    public float MaximumSlopeAngle { get; private set; } = 45f;

    [field: SerializeField]
    public bool OnSlope { get; set; }

    [field: SerializeField]
    public bool SlidingOffSlope { get; set; }

}
