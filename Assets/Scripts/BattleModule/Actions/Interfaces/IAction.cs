using BattleModule.Actions.Outcome;
using BattleModule.Controllers.Modules;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Types.Base;

namespace BattleModule.Actions.Interfaces
{
    public interface IAction
    {
        public BattleActionOutcome ProcessAction(Character target, 
            BattleActionOutcome battleActionOutcome,
            BattleDamage battleDamage,
            BattleOutcomeController battleOutcomeController);
    }
}