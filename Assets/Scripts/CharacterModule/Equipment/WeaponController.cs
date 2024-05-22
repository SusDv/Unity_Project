using BattleModule.Actions.BattleActions.Interfaces;
using CharacterModule.Inventory.Interfaces;
using CharacterModule.Inventory.Items.Equipment;
using CharacterModule.Stats.Managers;
using CharacterModule.Types.Base;
using CharacterModule.WeaponSpecial.Interfaces;

namespace CharacterModule.Equipment 
{
    public class WeaponController
    {
        private IEquipment _weapon;

        private IBattleObject _battleObject;

        private ISpecialAttack _specialAttack;

        private readonly StatManager _statModifierManager;

        public WeaponController (Character character)
        {
            _statModifierManager = character.CharacterStats;
        }

        public void Equip(Weapon equipment) 
        {
            _weapon = equipment.GetEquipment();

            _battleObject = equipment;

            _specialAttack = equipment.SpecialAttack.GetAttack();
            
            _weapon.Equip(_statModifierManager);
        }
        
        public void Unequip() 
        {
            _weapon.Unequip(_statModifierManager);

            _specialAttack = null;
            
            _weapon = null;
        }
        
        public IBattleObject GetWeapon() 
        {
            return _battleObject;
        }

        public ISpecialAttack GetSpecialAttack()
        {
            return _specialAttack;
        }
    }
}