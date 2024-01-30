using System;
using UnityEngine;

[Serializable]
public class PlayerWalkData
{
    [field: SerializeField] [field: Range(0.1f, 0.75f)]
    public float SpeedModifier { get; private set; } = 0.25f;
}
