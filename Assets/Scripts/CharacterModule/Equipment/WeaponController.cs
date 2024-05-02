using System.Collections.Generic;
using System.Linq;
using BattleModule.AccuracyModule.Transformer;
using BattleModule.Actions.BattleActions.Interfaces;
using CharacterModule.CharacterType.Base;
using CharacterModule.Inventory.Interfaces;
using CharacterModule.Inventory.Items.Equipment;
using CharacterModule.Stats.StatModifier.Manager;
using CharacterModule.WeaponSpecial.Interfaces;

namespace CharacterModule.Equipment 
{
    public class WeaponController
    {
        private Weapon _weapon;

        private ISpecialAttack _specialAttack;

        private readonly StatModifierManager _statModifierManager;

        public WeaponController (Character character)
        {
            _statModifierManager = character.CharacterStats.StatModifierManager;
        }

        public void Equip(Weapon equipment) 
        {
            _weapon = equipment;

            _specialAttack = equipment.SpecialAttack.GetAttack();
            
            (_weapon as IEquipment).Equip(_statModifierManager);
        }

        public bool HaveWeapon()
        {
            return _weapon == null;
        }

        public void Unequip() 
        {
            (_weapon as IEquipment).Unequip(_statModifierManager);

            _weapon = null;
        }
        
        public IBattleObject GetWeapon() 
        {
            return _weapon;
        }

        public ISpecialAttack GetSpecialAttack()
        {
            return _specialAttack;
        }

        public List<OutcomeTransformer> GetTransformers()
        {
            return _weapon.OutcomeTransformers.Where(o => o.IsAvailable).ToList();
        }
    }
}