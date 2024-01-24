using InventorySystem.Item;
using SpellModule.Settings;
using StatModule.Settings;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Settings", menuName = "Character/Settings/Character Settings")]
public class CharacterSettings : ScriptableObject
{
    public BaseStats BaseStats;

    public BaseSpells BaseSpells;

    public WeaponItem BaseWeapon;
}
