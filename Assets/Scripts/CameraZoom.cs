using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField, Range(1f, 10f)]
    private float defaultCameraDistance = 6f;
    [SerializeField, Range(1f, 10f)]
    private float minimumCameraDistance = 3f;
    [SerializeField, Range(1f, 10f)]
    private float maximumCameraDistance = 8f;

    [SerializeField, Range(0.5f, 2f)]
    private float zoomSensitivity = 1f;

    [SerializeField, Range(0f, 1f)]
    private float zoomSmoothing = 0.1f;

    CinemachineFramingTransposer framingTransposer;
    CinemachineInputProvider inputProvider;

    float currentTargetDistance;

    private void LateUpdate()
    {
        Zoom();
    }

    private void Awake()
    {
        framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        inputProvider = GetComponent<CinemachineInputProvider>();

        currentTargetDistance = defaultCameraDistance;
    }
    private void Zoom() 
    {
        float zoomValue = inputProvider.GetAxisValue(2) * zoomSensitivity;

        currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minimumCameraDistance, maximumCameraDistance);

        float currentDistance = framingTransposer.m_CameraDistance;

        if (currentDistance == currentTargetDistance)
            return;

        float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, zoomSmoothing);

        framingTransposer.m_CameraDistance = lerpedZoomValue;
    }
}
