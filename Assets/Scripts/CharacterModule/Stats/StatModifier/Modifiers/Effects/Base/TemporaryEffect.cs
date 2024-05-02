using CharacterModule.Stats.Interfaces;
using CharacterModule.Stats.Utility.Enums;

namespace CharacterModule.Stats.StatModifier.Modifiers.Effects.Base
{
    public abstract class TemporaryEffect
    {
        public abstract TemporaryEffectType TemporaryEffectType { get; }

        protected ITemporaryModifier TemporaryModifier;
        
        public virtual TemporaryEffect Init(ITemporaryModifier modifier)
        {
            TemporaryModifier = modifier;

            TemporaryModifier.BattleTimer.OnExpired += Modify;
            
            return this;
        }

        protected virtual void Modify()
        {
            if (--TemporaryModifier.Duration != 0)
            {
                TemporaryModifier.BattleTimer.ResetTimer();
            }
        }

        public virtual void Remove()
        { }
    }
}