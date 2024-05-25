using System;
using BattleModule.Utility;
using CharacterModule.Utility;
using Utility;

namespace CharacterModule.Stats.Modifiers.Effects.Base
{
    public abstract class TemporaryEffect
    {
        protected ModifierData ModifierData;

        protected BattleTimer BattleTimer;

        private Action _removeModifier;

        private Ref<int> _duration;
        
        public virtual void Init(ModifierData modifierData,
            BattleTimer battleTimer, Ref<int> duration, Action removeModifier)
        {
            ModifierData = modifierData;
            
            BattleTimer = battleTimer;
                
            BattleTimer.OnExpired += Modify;

            _removeModifier = removeModifier;
            
            _duration = duration;
        }

        protected virtual void Modify()
        {
            if (--_duration.Value != 0)
            {
                BattleTimer.ResetTimer();
            }
        }

        public virtual void Remove()
        {
            _removeModifier();
        }
    }
}