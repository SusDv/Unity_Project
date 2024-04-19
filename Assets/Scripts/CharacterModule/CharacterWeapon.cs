using BattleModule.Actions.BattleActions.Interfaces;
using CharacterModule.CharacterType.Base;
using CharacterModule.Inventory.Interfaces;
using CharacterModule.Inventory.Items.Equipment;
using CharacterModule.WeaponSpecial.Interfaces;

namespace CharacterModule 
{
    public class CharacterWeapon
    {
        private WeaponItem _weaponItem;

        private ISpecialAttack _specialAttack;

        private readonly Character _characterWithWeapon;

        public CharacterWeapon (Character character)
        {
            _characterWithWeapon = character;
        }

        public void EquipWeapon(WeaponItem equipmentItem) 
        {
            _weaponItem = equipmentItem;

            _specialAttack = equipmentItem.SpecialAttack.GetAttack();
            
            (_weaponItem as IEquipment).Equip(_characterWithWeapon.CharacterStats);
        }

        public bool HaveWeapon()
        {
            return _weaponItem == null;
        }

        public void UnequipItem() 
        {
            (_weaponItem as IEquipment).Unequip(_characterWithWeapon.CharacterStats);

            _weaponItem = null;
        }
        
        public IBattleObject GetWeapon() 
        {
            return _weaponItem;
        }

        public ISpecialAttack GetSpecialAttack()
        {
            return _specialAttack;
        }
    }
}