using CharacterModule.Inventory.Interfaces;
using CharacterModule.Inventory.Items;

namespace CharacterModule 
{
    public class CharacterWeapon
    {
        private WeaponItem _weaponItem;

        private readonly Character _characterWithWeapon;

        public CharacterWeapon (Character character)
        {
            _characterWithWeapon = character;
        }

        public void EquipWeapon(WeaponItem equipmentItem) 
        {
            _weaponItem = equipmentItem;

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

        public WeaponItem GetWeapon() 
        {
            return _weaponItem;
        }
    }
}