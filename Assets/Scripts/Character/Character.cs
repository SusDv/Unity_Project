using StatModule.Core;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterSettings _characterSettings;

    private CharacterWeapon _characterWeapon;

    private Stats _characterStats;

    private void Awake()
    {
        _characterWeapon = GetComponent<CharacterWeapon>();
        _characterStats = new Stats(_characterSettings.BaseStats);
        
        //_characterWeapon?.InitCharacterWeapon(_characterStats, _characterSettings.BaseWeapon);
    }

    public Stats GetStats() 
    {
        return _characterStats;
    }
}
