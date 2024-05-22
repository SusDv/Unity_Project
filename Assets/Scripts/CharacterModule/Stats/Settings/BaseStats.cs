using System.Linq;
using System.Collections.Generic;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.Utility.Enums;
using UnityEngine;

namespace CharacterModule.Stats.Settings
{
    [CreateAssetMenu(fileName = "Base Stats", menuName = "Character/Stats/Base Stats")]
    public class BaseStats : ScriptableObject 
    {
        [field: SerializeField]
        private List<Stat> StatList { get; set; } = new();
        
        public Dictionary<StatType, Stat> GetStats() 
        {
            foreach (var stat in StatList)
            {
                stat.MaxValue = stat.BaseValue;
                
                stat.FinalValue = stat.BaseValue;
            }
            
            return StatList.ToDictionary(stat => stat.StatType, stat => stat.Clone() as Stat);
        }
    }
}