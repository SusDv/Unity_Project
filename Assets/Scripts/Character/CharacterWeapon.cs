using InventorySystem.Item;
using StatModule.Core;
using StatModule.Modifier;
using UnityEngine;

public class CharacterWeapon : MonoBehaviour
{
    public Equipment WeaponItem;

    private Stats _characterStats;

    public void InitCharacterWeapon(Stats stats, Equipment baseWeapon) 
    {
        _characterStats = stats;
        EquipWeapon(baseWeapon);
    }

    public void EquipWeapon(Equipment item) 
    {
        WeaponItem = item;
    }
}
