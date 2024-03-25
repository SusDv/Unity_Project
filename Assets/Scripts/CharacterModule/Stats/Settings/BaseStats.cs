using System;
using System.Collections.Generic;
using System.Linq;
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

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (StatList.Count < Enum.GetNames(typeof(StatType)).Length)
            {
                StatList.Clear();

                foreach (StatType statType in Enum.GetValues(typeof(StatType)))
                {
                    StatList.Add(new Stat(statType));
                }
            }

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