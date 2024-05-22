using BattleModule.Actions.BattleActions.Outcome;
using CharacterModule.Stats.Managers;

namespace BattleModule.Actions.BattleActions.Interfaces
{
    public interface IAction
    {
        public void ApplyModifiers(StatManager source, StatManager target, BattleActionOutcome battleActionOutcome);
    }
}