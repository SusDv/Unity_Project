using CharacterModule.Stats.Base;

namespace InventorySystem.Item.Interfaces
{
    public interface IConsumable
    {
        public void Consume(Stats characterStats);
    }
}
