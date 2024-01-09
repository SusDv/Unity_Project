using System;
using UnityEngine;

[Serializable]
public class GroundCheckData
{
    [field: SerializeField] [field: Range(0f, 1f)]
    public float GroundCheckBuffer { get; private set; } = 0.02f;

    [field: SerializeField] [field: Range(0f, 100f)]
    public float FloatForceMultiplier { get; private set; } = 5f;
    [field: SerializeField] [field: Range(0f, 100f)]
    public float FloatVelocityMultiplier { get; private set; } = 5f;

    [field: SerializeField] [field: Range(0f, 0.8f)]
    public float GroundCheckTolerance { get; private set; } = 0.05f;

    [field: SerializeField]
    public bool OnGround { get; set; }

}