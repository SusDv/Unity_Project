using BattleModule.UI.BattleButton;
using UnityEngine;
using UnityEngine.UI;

namespace BattleModule.UI.View 
{
    public class BattleUISpellView 
        : BattleUIInteractable<BattleUISpellView>
    {
        [SerializeField] private Image _spellImage;

        public void SetData(Sprite spellImage) 
        {
            _spellImage.sprite = spellImage;
        }
    }
}


