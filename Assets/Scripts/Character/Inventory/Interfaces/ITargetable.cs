using BattleModule.Utility.Enums;

namespace InventorySystem.Inventory.Interfaces
{
    public interface ITargetable
    {
        public TargetType TargetType { get; set; }
    }
}
