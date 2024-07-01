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
        private List<StatWrapper> StatList { get; set; } = new();
        
        public Dictionary<StatType, Stat> GetStats() 
        {
            return StatList.ToDictionary(stat => stat.StatType, stat => stat.Stat.Clone(stat.StatType is StatType.HEALTH or StatType.MANA, true));
        }
    }
}