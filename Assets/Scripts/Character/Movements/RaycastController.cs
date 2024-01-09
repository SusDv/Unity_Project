using UnityEngine;

public class RaycastController : MonoBehaviour
{
    protected CapsuleCollider capsuleCollider;

    protected RaycastOrigins raycastOrigins;


    protected int horizontalRayNumber = 7;
    protected int verticalRayNumber = 6;

    protected float horizontalRaySpacing { get; private set; }
    protected float verticalRaySpacing { get; private set; }
    protected float forwardRaySpacing { get; private set; }

    protected float skinWidth = .02f;
    protected void UpdateRaycastOrigins()
    {
        Bounds bounds = capsuleCollider.bounds;
        bounds.Expand(-2 * skinWidth);
        raycastOrigins.forwardPosition = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
        raycastOrigins.backwardPosition = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);

        raycastOrigins.rightPosition = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
        raycastOrigins.leftPosition = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);

        raycastOrigins.upPosition = new Vector3(bounds.max.x, bounds.max.y, bounds.max.z);
        raycastOrigins.downPosition = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
    }
    protected void CalculateRaySpacing()
    {
        Bounds bounds = capsuleCollider.bounds;
        horizontalRayNumber = Mathf.Clamp(horizontalRayNumber, 2, int.MaxValue);
        verticalRayNumber = Mathf.Clamp(verticalRayNumber, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.x / horizontalRayNumber;
        verticalRaySpacing = bounds.size.y / verticalRayNumber;
        forwardRaySpacing = bounds.size.z / verticalRayNumber;
    }
    protected struct RaycastOrigins
    {
        public Vector3 forwardPosition, backwardPosition;
        public Vector3 leftPosition, rightPosition;
        public Vector3 upPosition, downPosition;
    }
}
