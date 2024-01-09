using InventorySystem.Item.Interfaces;
using UnityEngine;

namespace InventorySystem.Item 
{
    [CreateAssetMenu(fileName = "New Equipment", menuName = "Character/Items/Equipment")]
    public class Equipment : BaseItem, IItemAction
    {
        public void PerformAction(Character character)
        {
            
        }
    }
}