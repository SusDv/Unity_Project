using StatModule.Interfaces;

namespace InventorySystem.Item.Interfaces
{
    public interface IConsumable
    {
        public void Consume(IHaveStats character);
    }
}
