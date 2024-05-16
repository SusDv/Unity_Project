using CharacterModule.Data.Info;
using CharacterModule.Inventory.Items.Equipment;
using CharacterModule.Spells.Settings;
using CharacterModule.Stats.Settings;
using UnityEngine;

namespace CharacterModule.Settings
{
    [CreateAssetMenu(fileName = "Character Settings", menuName = "Character/Settings/Character Settings")]
    public class CharacterSettings : ScriptableObject
    {
        public BaseStats BaseStats;

        public BaseSpells BaseSpells;

        public Weapon BaseWeapon;

        public CharacterInformation CharacterInformation;
    }
}
