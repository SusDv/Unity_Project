using System;
using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.Utility;
using CharacterModule.Stats.Utility.Enums;
using CharacterModule.Types.Base;

namespace CharacterModule.Stats.Managers.SingleStat
{
    public class HealthManager : IStatObserver
    {
        private readonly Character _observableCharacter;
        
        public StatType StatType { get; set; } = StatType.HEALTH;

        public event Action<Character> OnCharacterDied = delegate { };

        public HealthManager(Character character)
        {
            _observableCharacter = character;
            
            character.CharacterStats.AttachStatObserver(this);
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