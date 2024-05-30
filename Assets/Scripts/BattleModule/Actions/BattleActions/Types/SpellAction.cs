using BattleModule.Actions.BattleActions.Base;
using BattleModule.Utility.DamageCalculator;
using CharacterModule.Stats.Managers;

namespace BattleModule.Actions.BattleActions.Types
{
    public class SpellAction : BattleAction
    {
        protected override BattleDamage GetDamageCalculator(StatManager statManager)
        {
            return new MagicDamage(statManager);
        }
    }
}
