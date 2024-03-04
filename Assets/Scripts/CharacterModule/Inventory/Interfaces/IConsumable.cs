using CharacterModule.Stats.Base;
using CharacterModule.Stats.Managers;

namespace InventorySystem.Item.Interfaces
{
    public interface IConsumable
    {
        public void Consume(StatManager characterStatManager);
    }
}
