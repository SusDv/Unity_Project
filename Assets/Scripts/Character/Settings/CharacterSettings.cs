using InventorySystem.Item;
using StatModule.Settings;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Settings", menuName = "Character/Settings/Character Settings")]
public class CharacterSettings : ScriptableObject
{
    public BaseStats BaseStats;

    public Equipment BaseWeapon;
}
