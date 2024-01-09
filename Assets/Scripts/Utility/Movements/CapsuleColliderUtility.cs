using System;
using UnityEngine;

[Serializable]
public class CapsuleColliderUtility
{
    public CapsuleColliderData CapsuleColliderData { get; private set; }

    [field: SerializeField]
    public DefaultColliderData DefaultColliderData { get; private set; }

    public float ColliderLocalHalfHeight { get; private set; }

    public float FloatHeight { get; private set; }

    public Vector3 BottomSphereLocalPosition { get; private set; }

    public Vector3 TopSphereLocalPosition { get; private set; }

    public void Initialize(GameObject gameObject)
    {
        if (CapsuleColliderData != null)
        {
            return;
        }

        CapsuleColliderData = new CapsuleColliderData();

        CapsuleColliderData.Initialize(gameObject);
    }

    public void CalculateCapsuleColliderDimensions() 
    {
        SetSkinWidth();
        
        SetCapsuleColliderHeight(DefaultColliderData.Height * (1 - DefaultColliderData.FloatHeightPercentage));
        
        RecalculateCapsuleColliderCenter();

        ColliderLocalHalfHeight = CapsuleColliderData.Collider.center.y;

        float halfHeight = CapsuleColliderData.Collider.height / 2f;

        FloatHeight = DefaultColliderData.Height - CapsuleColliderData.Collider.height;

        BottomSphereLocalPosition = CapsuleColliderData.ColliderCenterInLocalSpace - Vector3.up * halfHeight;

        TopSphereLocalPosition = BottomSphereLocalPosition + Vector3.up * CapsuleColliderData.Collider.height;

        if (CapsuleColliderData.Collider.height / 2f < halfHeight) 
        {
            SetCapsuleColliderRadius(halfHeight); 
        }


        CapsuleColliderData.UpdateColliderData();
    }

    public void SetCapsuleColliderRadius(float updatedColliderRadius)
    {
        CapsuleColliderData.Collider.radius = updatedColliderRadius;
    }

    public void SetCapsuleColliderHeight(float updatedColliderHeight)
    {
        CapsuleColliderData.Collider.height = updatedColliderHeight;
    }

    public void SetCapsulleColliderCenter(Vector3 updatedColliderCenter)
    {
        CapsuleColliderData.Collider.center = updatedColliderCenter;
    }

    public void SetSkinWidth() 
    {
        DefaultColliderData.SkinWidth = Mathf.Clamp(DefaultColliderData.SkinWidth, 0f, DefaultColliderData.Radius / 2f);
        SetCapsuleColliderRadius(DefaultColliderData.Radius - DefaultColliderData.SkinWidth);
    }

    public void RecalculateCapsuleColliderCenter() 
    {
        float colliderHeightDifference = DefaultColliderData.Height - CapsuleColliderData.Collider.height;

        Vector3 center = new Vector3(0f, DefaultColliderData.CenterY + (colliderHeightDifference / 2f), 0f);
        
        SetCapsulleColliderCenter(center);
    }
}