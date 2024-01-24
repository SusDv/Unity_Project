using StatModule.Base;
using CharacterModule.Weapon;
using UnityEngine;
using SpellModule.Base;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterSettings _characterSettings;

    private CharacterWeapon _characterWeapon;

    private Stats _characterStats;

    private Spells _characterSpells;

    private void Awake()
    {
        _characterStats = new Stats(_characterSettings.BaseStats);

        _characterSpells = new Spells(_characterSettings.BaseSpells);

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

    public Spells GetCharacterSpells() 
    {
        return _characterSpells;
    }
}