using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Actions.BattleActions.Transformer;
using BattleModule.Controllers.Modules;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Stats.Managers;

namespace BattleModule.Actions.BattleActions.Interfaces
{
    public interface IAction
    {
        public (List<OutcomeTransformer> toAdd, BattleActionOutcome result) ApplyModifiers(StatManager target, 
            BattleActionOutcome battleActionOutcome,
            BattleDamage battleDamage,
            BattleOutcomeController battleOutcomeController);
    }
}