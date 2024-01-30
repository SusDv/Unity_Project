using System;
using UnityEngine;

[Serializable]
public class PlayerSprintData
{
    [field: SerializeField] [field: Range(1.5f, 2f)]
    public float SpeedModifier { get; private set; } = 1.5f;
}
