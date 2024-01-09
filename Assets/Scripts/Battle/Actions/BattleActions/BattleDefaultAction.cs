using StatModule.Utility.Enums;

namespace BattleModule.ActionCore
{
    public class BattleDefaultAction : BattleAction 
    {
        private BattleDefaultAction() 
        {
            SetupAction(null);
        }

        public override void PerformAction(Character source, Character target)
        {
            target.GetStats().ModifyStat(StatType.HEALTH, -source.GetStats().GetStatFinalValue(StatType.ATTACK));
            
            source.GetStats().ModifyStat(StatType.BATTLE_POINTS, 10);
        }

        public static BattleDefaultAction GetBattleDefaultActionInstance() 
        {
            return new BattleDefaultAction();
        }
    }
}
