using BattleModule.Actions.BattleActions.Interfaces;
using BattleModule.Actions.BattleActions.Outcome;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.StatModifier;
using CharacterModule.Utility;

namespace BattleModule.Actions.BattleActions.Processors
{
    public class ActionProcessor : IAction
    {
        private float BattlePoints { get; }
        
        private StatModifiers TargetModifiers { get; }

        public ActionProcessor(float battlePoints, 
            StatModifiers statModifiers)
        {
            BattlePoints = battlePoints;

            TargetModifiers = statModifiers;
        }

        public virtual void ApplyModifiers(StatManager source, 
            StatManager target, BattleActionOutcome battleActionOutcome)
        {
            foreach (var modifier in TargetModifiers.GetModifiers().modifiers)
            {
                target.AddModifier(modifier);
            }
            
            foreach (var modifier in TargetModifiers.GetModifiers().temporaryModifiers)
            {
                target.AddModifier(modifier);
            }
            
            source.ApplyInstantModifier(StatType.BATTLE_POINTS, BattlePoints);
        }
    }
}