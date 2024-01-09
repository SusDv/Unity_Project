using UnityEngine;

public class CanvasRotation : MonoBehaviour
{
    Camera mainCamera;
    private void Start () => mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    private void Update() => transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
}
