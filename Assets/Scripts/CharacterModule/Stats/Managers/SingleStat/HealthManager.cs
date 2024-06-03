using System;
using CharacterModule.Types.Base;
using CharacterModule.Utility;
using CharacterModule.Utility.Stats;
using Utility.ObserverPattern;

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
            
            character.Stats.AttachStatObserver(this);
        }

        public async void UpdateValue(StatInfo statInfo, bool negativeChange)
        {
            if (negativeChange)
            {
                await _observableCharacter.AnimationManager.PlayAnimation("Damaged");
            }

            if (!(statInfo.FinalValue <= 0))
            {
                return;
            }
            
            OnCharacterDied?.Invoke(_observableCharacter);
            
            _observableCharacter.Dispose();
        }
    }
}