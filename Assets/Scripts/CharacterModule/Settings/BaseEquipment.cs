using System;
using System.Collections.Generic;
using CharacterModule.Inventory.Items.Equipment;
using UnityEngine;

namespace CharacterModule.Settings
{
    [Serializable]
    public class BaseEquipment
    {
        [field: SerializeField]
        public Weapon BaseWeapon { get; private set; }

        [field: SerializeField]
        public List<Armor> BaseArmor { get; private set; }
    }
}