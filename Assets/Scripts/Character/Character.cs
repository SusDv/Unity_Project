using StatModule.Core;
using CharacterModule.Weapon;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterSettings _characterSettings;

    private CharacterWeapon _characterWeapon;

    private Stats _characterStats;

    private void Awake()
    {
        _characterStats = new Stats(_characterSettings.BaseStats);

        _characterWeapon = new CharacterWeapon(this);

        _characterWeapon.EquipWeapon(_characterSettings.BaseWeapon);
    }

    public Stats GetCharacterStats() 
    {
        return _characterStats;
    }

    public CharacterWeapon GetCharacterWeapon() 
    {
        return _characterWeapon;
    }
}