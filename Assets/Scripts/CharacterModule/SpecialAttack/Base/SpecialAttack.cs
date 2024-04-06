using System.Collections.Generic;
using BattleModule.Utility;
using CharacterModule.SpecialAttack.Interfaces;
using UnityEngine;
using Utility;
using Utility.Constants;
using Utility.Information;

namespace CharacterModule.SpecialAttack.Base
{
    [CreateAssetMenu(fileName = "Special Attack", menuName = "Character/Special")]
    public class SpecialAttack : ScriptableObject, ISpecialAttack, IObjectInformation
    {
        private float _currentEnergyAmount;

        [field: SerializeField] 
        public ObjectInformation ObjectInformation { get; set; }

        [field: SerializeField]
        public float EnergyToAttack { get; set; }
        
        public void Attack(Character source, List<Character> targets)
        {
            if (Mathf.RoundToInt(EnergyToAttack - _currentEnergyAmount) != 0)
            {
                return;
            }
        }

        public void Charge(float amount)
        {
            _currentEnergyAmount = Mathf.Clamp(_currentEnergyAmount + amount, 0, EnergyToAttack);
        }

        [field: SerializeField]
        public TargetType TargetType { get; set; }
        
        [field: SerializeField]
        public TargetSearchType TargetSearchType { get; set; }
        
        [field: SerializeField]
        [field: Range(BattleTargetingConstants.SpellMin, BattleTargetingConstants.SpellMax)]
        public int MaxTargetsCount { get; set; }
    }
}