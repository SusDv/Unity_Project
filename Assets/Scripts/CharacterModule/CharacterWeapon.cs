using InventorySystem.Item;
using InventorySystem.Item.Interfaces;

namespace CharacterModule.Weapon 
{
    public class CharacterWeapon
    {
        private WeaponItem _weaponItem;

        private Character _characterWithWeapon;

        public bool HaveWeapon { get; set; } = false;

        public CharacterWeapon (Character character)
        {
            _characterWithWeapon = character;
        }

        public void EquipWeapon(WeaponItem equipmentItem) 
        {
            _weaponItem = equipmentItem;

            (_weaponItem as IEquipable).Equip(_characterWithWeapon);
        }

        public void UnequipItem() 
        {
            (_weaponItem as IEquipable).Unequip(_characterWithWeapon);

            _weaponItem = null;
        }

        public WeaponItem GetWeapon() 
        {
            return _weaponItem;
        }
    }
}