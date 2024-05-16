using System;
using CharacterModule.CharacterType.Base;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICharacterSelect : MonoBehaviour
{
    public Action<UICharacterSelect> OnItemClicked;

    public Character selectionCharacter;
    public TextMeshProUGUI characterName;

    public void OnPointerClick(BaseEventData data)
    {
        PointerEventData pointerEventData = (PointerEventData)data;

        if (pointerEventData.button == PointerEventData.InputButton.Left)
            OnItemClicked?.Invoke(this);
    }

    public void SetData(string text, Character character) 
    {
        characterName.text = text;
        selectionCharacter = character;
    }
}
