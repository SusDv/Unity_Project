using System.Collections.Generic;
using System.Linq;
using CharacterModule.Stats.Base;
using CharacterModule.Utility;
using UnityEngine;

namespace CharacterModule.Settings
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