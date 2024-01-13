using InventorySystem.Item;
using InventorySystem.Item.Interfaces;

namespace CharacterModule.Weapon 
{
    public class CharacterWeapon
    {
        private Equipment _weaponItem;

        private Character _characterWithWeapon;

        public bool HaveWeapon { get; set; } = false;

        public CharacterWeapon (Character character)
        {
            _characterWithWeapon = character;
        }

        public void EquipWeapon(Equipment equipmentItem) 
        {
            _weaponItem = equipmentItem;

            (_weaponItem as IItemAction).PerformAction(_characterWithWeapon);
        }

        public void UnequipItem(Equipment equipmentItem) 
        {
            (_weaponItem as IItemAction).PerformAction(_characterWithWeapon);

            _weaponItem = null;
        }

        public float GetWeaponAttackCost() 
        {
            return _weaponItem.BattlePoints;
        }
    }
}