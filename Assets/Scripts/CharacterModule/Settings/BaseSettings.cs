using UnityEngine;

namespace CharacterModule.Settings
{
    [CreateAssetMenu(fileName = "Character Settings", menuName = "Character/Settings/Character Settings")]
    public class BaseSettings : ScriptableObject
    {
        [field: SerializeField] 
        public BaseStats BaseStats { get; private set; }

        [field: SerializeField]
        public BaseSpells BaseSpells { get; private set; }
        
        [field: SerializeField]
        public BaseInformation BaseInformation { get; private set; }
        
        [field: SerializeField]
        public BaseEquipment BaseEquipment { get; private set; }
    }
}
