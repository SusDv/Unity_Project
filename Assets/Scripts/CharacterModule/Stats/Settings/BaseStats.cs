using System.Collections.Generic;
using System.Linq;
using CharacterModule.Stats.Base;
using StatModule.Utility.Enums;
using UnityEngine;

namespace CharacterModule.Stats.Settings
{
    [CreateAssetMenu(menuName = "Base Stats", fileName = "Character/Stats/Base Stats")]
    public class BaseStats : ScriptableObject 
    {
        [field: SerializeField]
        private List<Stat> StatList { get; set; } = new();

#if UNITY_EDITOR
        private void OnValidate()
        {
            foreach (var stat in StatList)
            {
                stat.MaxValue = stat.BaseValue;
                
                stat.FinalValue = stat.BaseValue;
            }
        }
#else
        private void Awake()
        {
            foreach (var stat in StatList)
            {
                stat.MaxValue = stat.BaseValue;
                
                stat.FinalValue = stat.BaseValue;
            }
        }
#endif
        

        public Dictionary<StatType, Stat> GetStats() 
        {
            return StatList.ToDictionary(stat => stat.StatType, stat => stat.Clone() as Stat);
        }
    }
}