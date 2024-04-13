using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField, Range(1f, 10f)]
    private float _defaultCameraDistance = 6f;
    [SerializeField, Range(1f, 10f)]
    private float _minimumCameraDistance = 3f;
    [SerializeField, Range(1f, 10f)]
    private float _maximumCameraDistance = 8f;

    [SerializeField, Range(0.5f, 2f)]
    private float _zoomSensitivity = 1f;

    [SerializeField, Range(0f, 1f)]
    private float _zoomSmoothing = 0.1f;

    private CinemachineFramingTransposer _framingTransposer;
    private CinemachineInputProvider _inputProvider;

    private float _currentTargetDistance;

    private void LateUpdate()
    {
        Zoom();
    }

    private void Awake()
    {
        _framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        _inputProvider = GetComponent<CinemachineInputProvider>();

        _currentTargetDistance = _defaultCameraDistance;
    }
    private void Zoom() 
    {
        float zoomValue = _inputProvider.GetAxisValue(2) * _zoomSensitivity;

        _currentTargetDistance = Mathf.Clamp(_currentTargetDistance + zoomValue, _minimumCameraDistance, _maximumCameraDistance);

        float currentDistance = _framingTransposer.m_CameraDistance;

        if (currentDistance == _currentTargetDistance)
            return;

        float lerpedZoomValue = Mathf.Lerp(currentDistance, _currentTargetDistance, _zoomSmoothing);

        _framingTransposer.m_CameraDistance = lerpedZoomValue;
    }
}
