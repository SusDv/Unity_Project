using BattleModule.Actions.Interfaces;
using BattleModule.Actions.Processors;
using CharacterModule.Stats.StatModifier;
using UnityEngine;

namespace CharacterModule.Spells.Core.Spells
{
    [CreateAssetMenu(fileName = "Spell", menuName = "Character/Spells/Basic Spell")]
    public class BasicSpell : SpellBase
    {
        [field: SerializeField]
        public DynamicStatModifiers TargetModifiers { get; private set; }

        public override IAction GetAction()
        {
            return new DefaultActionProcessor(GetInstanceID(), TargetModifiers, OutcomeTransformers);
        }
    }
}
