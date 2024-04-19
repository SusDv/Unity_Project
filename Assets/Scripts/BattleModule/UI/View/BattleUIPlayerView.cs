using System.Collections.Generic;
using CharacterModule.Data.Info;
using CharacterModule.Stats.Interfaces;
using CharacterModule.WeaponSpecial.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Utility.UI;

namespace BattleModule.UI.View 
{
    public class BattleUIPlayerView : MonoBehaviour
    {
        [SerializeField] private Image _characterImage;
        
        [SerializeField] private List<SliderStatObserver> _statObservers;

        [SerializeField] private BattleUISpecialAttackView _specialAttack;

        public void SetData(ISpecialAttack specialAttack,
            CharacterInformation characterInformation, 
            IStatSubject statSubject) 
        {
            _characterImage.sprite = characterInformation.CharacterImage;
            
            _specialAttack.SetData(specialAttack);

            _statObservers.ForEach(statSubject.AttachStatObserver);
        }
    }
}