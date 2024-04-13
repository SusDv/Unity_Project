using UnityEngine;

public class CanvasRotation : MonoBehaviour
{
    Camera _mainCamera;
    private void Start () => _mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    private void Update() => transform.rotation = Quaternion.LookRotation(transform.position - _mainCamera.transform.position);
}
