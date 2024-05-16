using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Interfaces;
using CharacterModule.Stats.StatModifier;
using CharacterModule.Stats.StatModifier.Manager;
using CharacterModule.Stats.Utility.Enums;

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

        public virtual void ApplyModifiers(StatModifierManager source, List<StatModifierManager> targets)
        {
            foreach (var target in targets)
            {
                foreach (var modifier in TargetModifiers.GetModifiers())
                {
                    target.AddModifier(modifier);
                }
            }
            
            source.ApplyInstantModifier(StatType.BATTLE_POINTS, BattlePoints);
        }
    }
}