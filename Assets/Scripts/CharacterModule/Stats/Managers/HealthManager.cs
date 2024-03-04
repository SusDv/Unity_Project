using System;
using CharacterModule.Stats.Interfaces;
using StatModule.Utility;
using StatModule.Utility.Enums;

namespace CharacterModule.Stats.Managers
{
    public class HealthManager : IStatObserver
    {
        private readonly Character _observableCharacter;
        
        public StatType StatType { get; set; } = StatType.HEALTH;

        public event Action<Character> OnCharacterDied = delegate { };

        public HealthManager(Character character)
        {
            _observableCharacter = character;
            
            character.GetCharacterStats().AttachStatObserver(this);
        }

        public void UpdateValue(StatInfo statInfo)
        {
            if (!(statInfo.FinalValue <= 0))
            {
                return;
            }

            OnCharacterDied?.Invoke(_observableCharacter);
            
            _observableCharacter.Dispose();
        }
    }
}