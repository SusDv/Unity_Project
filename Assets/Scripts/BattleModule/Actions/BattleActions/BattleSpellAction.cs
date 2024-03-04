using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Base;
using CharacterModule;
using CharacterModule.Spells.Interfaces;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.Managers;
using JetBrains.Annotations;

namespace BattleModule.Actions.BattleActions
{
    [UsedImplicitly]
    public class BattleSpellAction : BattleAction
    {
        protected override string ActionName => "Spell use";

        public override void PerformAction(StatManager source, List<Character> targets)
        {
            (BattleActionContext.ActionObject as ISpell)?.UseSpell(source, targets);
        }
    }
}
