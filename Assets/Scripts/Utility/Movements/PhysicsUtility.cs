using System;
using UnityEngine;
[Serializable]
public class PhysicsUtility
{
    [field: SerializeField]
    public PlayerGravityData GravityData;

    [field: SerializeField]
    public SlopeData SlopeData;

    [field: SerializeField]
    public GroundCheckData GroundCheckData;

    [field: SerializeField]
    public Vector3 RigidBodyVelocity { get; set; }

    [field: SerializeField]
    public float RigidBodyVelocityMagnitude { get; set; }


    public void Initialize() 
    {
        if (GravityData != null && SlopeData != null && GroundCheckData != null) 
        {
            return;
        }

        GravityData = new PlayerGravityData();
        SlopeData = new SlopeData();
        GroundCheckData = new GroundCheckData();
    }
}