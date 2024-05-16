using System.Collections.Generic;
using CharacterModule.Stats.StatModifier.Manager;

namespace BattleModule.Actions.BattleActions.Interfaces
{
    public interface IAction
    {
        public void ApplyModifiers(StatModifierManager source, List<StatModifierManager> targets);
    }
}