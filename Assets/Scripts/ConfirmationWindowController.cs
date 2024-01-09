using TMPro;
using UnityEngine;

public class ConfirmationWindowController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI confirmationText;
    private string description;

    public ConfirmationWindowController(string text) 
    {
        description = text;
    }
    private void OnEnable()
    {
        confirmationText.text = description;
    }

    private void OnDisable()
    {
        confirmationText.text = string.Empty;
    }

    public bool Confirm(bool IsConfirmed) 
    {
        return IsConfirmed;
    }
}
