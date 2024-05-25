using CharacterModule.Inventory.Items.Equipment;
using UnityEngine;

namespace CharacterModule.Settings
{
    [CreateAssetMenu(fileName = "Character Settings", menuName = "Character/Settings/Character Settings")]
    public class BaseSettings : ScriptableObject
    {
        public BaseStats BaseStats;

        public BaseSpells BaseSpells;

        public BaseInformation BaseInformation;
        
        public Weapon BaseWeapon;
    }
}
