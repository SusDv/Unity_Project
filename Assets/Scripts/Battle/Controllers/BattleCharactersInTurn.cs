using BattleModule.ActionCore.Events;
using StatModule.Core;
using StatModule.Modifier;
using StatModule.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleModule.Controllers
{
    public class BattleCharactersInTurn
    {
        private List<Character> _charactersInTurn;

        public Action<List<Character>> OnCharacterInTurnChanged;

        public BattleCharactersInTurn(List<Character> characters)
        {
            _charactersInTurn = new List<Character>(characters);

            SetupBattleActions();
        }

        private void SetupBattleActions()
        {
            BattleGlobalActionEventProcessor.OnTurnEnded += UpdateCharactersInTurn;
        }

        private void UpdateCharactersInTurn()
        {
            foreach (Character character in _charactersInTurn)
            {
                float battlePoints = character.GetCharacterStats().GetStatFinalValue(StatType.BATTLE_POINTS);

                int deduction;

                int tierNumber = (int) Mathf.Clamp(GetTierNumber(battlePoints), 0f, 4f);

                if (battlePoints <= 10 * tierNumber)
                {
                    deduction = 2 * tierNumber;
                }
                else if (battlePoints > 10 * tierNumber &&
                        battlePoints < 10 * tierNumber + 2 * tierNumber)
                {
                    deduction = 3 + (2 * (tierNumber - 1));
                }
                else 
                {
                    deduction = 4 + (2 * (tierNumber - 1));
                }

                character.GetCharacterStats().AddStatModifier(StatType.BATTLE_POINTS, -deduction);
            }

            SortCharactersByBattlePoints();
        }

        private float GetTierNumber(float battlePoints) 
        {
            return Mathf.Floor(battlePoints / 10);
        }

        private void SortCharactersByBattlePoints() 
        {
            _charactersInTurn = _charactersInTurn
                .OrderBy(characterInTurn => characterInTurn.GetCharacterStats().GetStatFinalValue(StatType.BATTLE_POINTS))
                    .ToList();

            OnCharacterInTurnChanged?.Invoke(GetCharactersInTurn().ToList());
        }

        private void ResetCharacterInTurnBattlePoints()
        {
            Stats characterInTurnStats = GetCharacterInTurn().GetCharacterStats();

            characterInTurnStats.AddStatModifier(StatType.BATTLE_POINTS, -characterInTurnStats.GetStatFinalValue(StatType.BATTLE_POINTS));

            OnCharacterInTurnChanged?.Invoke(GetCharactersInTurn().ToList());
        }

        private void TriggerCharacterInTurnTemporaryModifiers() 
        {
            Character characterInTurn = GetCharacterInTurn();

            Stats characterInTurnStats = characterInTurn.GetCharacterStats();
            
            characterInTurnStats.ApplyStatModifiersByCondition(
                (statModifier) =>
                    (statModifier is TemporaryStatModifier),

                (statModifier) => 
                    !(statModifier as TemporaryStatModifier)
                        .TemporaryStatModifierType
                        .Equals(TemporaryStatModifierType.APPLIED_EVERY_CYCLE)
                );
        }

        public void OnTurnStarted() 
        {
            ResetCharacterInTurnBattlePoints();
            TriggerCharacterInTurnTemporaryModifiers();
        }

        public IList<Character> GetCharactersInTurn()
        {
            return _charactersInTurn.AsReadOnly();
        }

        public Character GetCharacterInTurn()
        {
            return GetCharactersInTurn().First();
        }
    }
}