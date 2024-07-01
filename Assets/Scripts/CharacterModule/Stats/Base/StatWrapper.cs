using System;
using CharacterModule.Utility;
using UnityEngine;

namespace CharacterModule.Stats.Base
{
    [Serializable]
    public class StatWrapper
    {
        [field: SerializeField]
        public StatType StatType { get; private set; }

        [field: SerializeField]
        public Stat Stat { get; private set; }
    }
}