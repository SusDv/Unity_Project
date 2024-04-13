using CharacterModule.CharacterType.Base;

namespace CharacterModule.Inventory.Interfaces
{
    public interface IConsumable
    {
        public void Consume(Character target);
    }
}
