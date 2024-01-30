using BattleModule.UI.Button;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BattleModule.UI.View 
{
    public class BattleUISpellView 
        : BattleUIButton<BattleUISpellView>
    {
        [SerializeField] private Image _spellImage;

        public void SetData(Sprite spellImage) 
        {
            _spellImage.sprite = spellImage;
        }
    }
}


