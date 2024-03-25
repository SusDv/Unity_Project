using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Base;
using BattleModule.Actions.BattleActions.Context;
using CharacterModule;
using CharacterModule.Spells.Core;
using CharacterModule.Spells.Interfaces;
using CharacterModule.Stats.Managers;
using JetBrains.Annotations;

namespace BattleModule.Actions.BattleActions
{
    [UsedImplicitly]
    public class BattleSpellAction : BattleAction
    {
        public override void Init(object actionObject)
        {
            BattleActionContext = new BattleActionContext((actionObject as SpellBase)?.SpellName, actionObject);
        }

        public override void PerformAction(StatManager source, List<Character> targets)
        {
            (BattleActionContext.ActionObject as ISpell)?.UseSpell(source, targets);

            foreach (var statModifier in (BattleActionContext.ActionObject as SpellBase)?.SourceModifiers.GetModifiers()!)
            {
                source.ApplyStatModifier(statModifier);
            }
        }
    }
}
