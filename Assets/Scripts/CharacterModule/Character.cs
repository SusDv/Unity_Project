using CharacterModule.Spells.Core;
using CharacterModule.Stats.Base;
using CharacterModule.Weapon;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterSettings _characterSettings;

    private CharacterWeapon _characterWeapon;

    private Stats _characterStats;

    private SpellContainer _characterSpellContainer;

    private void Awake()
    {
        _characterStats = new Stats(_characterSettings.BaseStats);

        _characterSpellContainer = new SpellContainer(_characterSettings.BaseSpells);

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

    public SpellContainer GetCharacterSpells() 
    {
        return _characterSpellContainer;
    }
}