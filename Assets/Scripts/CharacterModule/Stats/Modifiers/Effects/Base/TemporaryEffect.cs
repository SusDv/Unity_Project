using BattleModule.Utility;
using CharacterModule.Utility;
using CharacterModule.Utility.Stats;
using Utility;

namespace CharacterModule.Stats.Modifiers.Effects.Base
{
    public abstract class TemporaryEffect
    {
        protected ModifierData ModifierData;

        protected BattleTimer BattleTimer;

        private Ref<int> _duration;
        
        public virtual void Init(ModifierData modifierData,
            BattleTimer battleTimer, Ref<int> duration)
        {
            ModifierData = modifierData;
            
            BattleTimer = battleTimer;

            _duration = duration;
            
            BattleTimer.OnExpired += Modify;
        }

        protected virtual void Modify()
        {
            --_duration.Value;
            
            if (_duration.Value != 0)
            {
                BattleTimer.ResetTimer();
            }
        }

        public virtual void Remove()
        { }
    }
}