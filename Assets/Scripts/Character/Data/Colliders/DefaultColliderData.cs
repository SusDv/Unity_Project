using System;
using UnityEngine;

[Serializable]
public class DefaultColliderData
{
    [field: SerializeField]
    public float Height { get; private set; } = 1.8f;

    [field: SerializeField]
    public float CenterY { get; private set; } = 0.8f;

    [field: SerializeField]
    public float Radius { get; private set; } = 0.2f;

    [field: SerializeField] [field: Range(0f, 1f)]
    public float FloatHeightPercentage { get; set; } = 0.25f;

    [field: SerializeField]
    public float SkinWidth { get; set; } = 0.05f;
}
