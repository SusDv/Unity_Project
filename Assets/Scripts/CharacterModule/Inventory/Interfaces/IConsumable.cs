using CharacterModule.Stats.Base;
using StatModule.Interfaces;

namespace InventorySystem.Item.Interfaces
{
    public interface IConsumable
    {
        public void Consume(Stats characterStats);
    }
}
