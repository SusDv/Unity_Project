using System;
using UnityEngine;

[Serializable]
public class PlayerGravityData
{
    [field: SerializeField] [field: Range(10f, 50f)]
    public float MinimumFallGravity { get; private set; } = 20f;

    [field: SerializeField] [field: Range(20f, 100f)]
    public float MaximumFallGravity { get; private set; } = 30f;

    [field: SerializeField] [field: Range(0.01f, 0.5f)]
    public float GravityFallIncrementTime { get; set; } = 0.05f;

    [field: SerializeField] [field: Range(5f, 15f)]
    public float GravityFallIncrementAmount { get; private set; } = 5f;

    [field: SerializeField]
    public float GravityFallIncerementTimer { get; set; }

    [field: SerializeField]
    public float CurrentFallGravity { get; set; } = 0f;

    [field: SerializeField]
    public bool IsFalling { get; set; } = false;

}
