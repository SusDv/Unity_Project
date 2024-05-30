using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Stats.Managers;

namespace BattleModule.Actions.BattleActions.Interfaces
{
    public interface IAction
    {
        public void ApplyModifiers(StatManager target, BattleActionOutcome battleActionOutcome,
            BattleDamage battleDamage);
    }
}