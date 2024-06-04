using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using CharacterModule.Settings;
using Utility.UI;
using CharacterModule.WeaponSpecial.Interfaces;
using Utility.ObserverPattern;

namespace BattleModule.UI.View 
{
    public class BattleUIPlayerView : MonoBehaviour
    {
        [SerializeField] private Image _characterImage;
        
        [SerializeField] private List<SliderStatObserver> _statObservers;

        [SerializeField] private BattleUISpecialAttackView _specialAttack;

        public void SetData(ISpecialAttack specialAttack,
            BaseInformation baseInformation, 
            IStatSubject statSubject) 
        {
            _characterImage.sprite = baseInformation.CharacterImage;
            
            _specialAttack.SetData(specialAttack);

            _statObservers.ForEach(statSubject.AttachObserver);
        }
    }
}