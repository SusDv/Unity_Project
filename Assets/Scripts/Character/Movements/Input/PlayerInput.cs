using UnityEngine;
public class PlayerInput : MonoBehaviour
{
    public PlayerInputActions InputActions { get; private set; }

    public PlayerInputActions.PlayerActions PlayerActions { get; private set; } 

    private void Awake()
    {
        InputActions = new PlayerInputActions();

        PlayerActions = InputActions.Player;
    }

    private void OnEnable()
    {
        PlayerActions.Enable();
    }
    private void OnDisable()
    {
        PlayerActions.Disable();
    }
}
