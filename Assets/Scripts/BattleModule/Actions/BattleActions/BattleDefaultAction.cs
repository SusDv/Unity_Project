using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Base;
using BattleModule.Actions.BattleActions.Context;
using BattleModule.Utility;
using CharacterModule;
using CharacterModule.Inventory.Items.Base;
using CharacterModule.Inventory.Items.Equipment;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.Utility.Enums;
using JetBrains.Annotations;

namespace BattleModule.Actions.BattleActions
{
    [UsedImplicitly]
    public class BattleDefaultAction : BattleAction 
    {
        public override void PerformAction(StatManager source, List<Character> targets)
        {
            var characterWeapon = BattleActionContext.ActionObject as WeaponItem;

            foreach(var character in targets) 
            {
                var target = character.CharacterStats;

                float damage = -BattleAttackDamageProcessor.CalculateAttackDamage(
                    source.GetStatInfo(StatType.ATTACK).FinalValue,
                    target.GetStatInfo(StatType.DEFENSE).FinalValue);

                target.ApplyStatModifier(StatType.HEALTH,
                    damage);
            }

            source.ApplyStatModifier(StatType.BATTLE_POINTS, characterWeapon.BattlePoints);
        }
    }
}
