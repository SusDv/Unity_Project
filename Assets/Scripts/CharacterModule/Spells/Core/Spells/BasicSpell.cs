using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Interfaces;
using BattleModule.Actions.BattleActions.Processors;
using CharacterModule.CharacterType.Base;
using CharacterModule.Stats.StatModifier;
using UnityEngine;

namespace CharacterModule.Spells.Core.Spells
{
    [CreateAssetMenu(fileName = "Spell", menuName = "Character/Spells/Basic Spell")]
    public class BasicSpell : SpellBase
    {
        [field: SerializeReference]
        public StatModifiers TargetModifiers { get; private set; } = new DynamicStatModifiers();

        public override IAction GetAction()
        {
            return new ActionProcessor(BattlePoints, TargetModifiers);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            TargetModifiers.GetModifiers().ForEach(statModifier => statModifier.ModifierData.SourceID = GetInstanceID());
        }
#else
        private void Awake()
        {
            TargetModifiers.GetModifiers().ForEach(statModifier => statModifier.ModifierData.SourceID = ID);
        }
#endif
    }
}
