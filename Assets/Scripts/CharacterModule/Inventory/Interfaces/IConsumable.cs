using CharacterModule.Stats.Managers;

namespace CharacterModule.Inventory.Interfaces
{
    public interface IConsumable
    {
        public void Consume(StatManager characterStatManager);
    }
}
