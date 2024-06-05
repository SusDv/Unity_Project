using System.Collections.Generic;
using BattleModule.Actions.Outcome;
using BattleModule.Actions.Transformer;
using BattleModule.Controllers.Modules;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Stats.Managers;

namespace BattleModule.Actions.Interfaces
{
    public interface IAction
    {
        public (List<OutcomeTransformer> toAdd, BattleActionOutcome result) ProcessAction(StatManager target, 
            BattleActionOutcome battleActionOutcome,
            BattleDamage battleDamage,
            BattleOutcomeController battleOutcomeController);
    }
}