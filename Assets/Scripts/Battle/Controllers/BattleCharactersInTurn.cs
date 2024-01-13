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

                if (battlePoints > 10)
                {
                    if (battlePoints <= 12)
                    {
                        deduction = 2;
                    }
                    else if (battlePoints <= 24)
                    {
                        deduction = 4;
                    }
                    else if (battlePoints <= 36)
                    {
                        deduction = 6;
                    }
                    else
                    {
                        deduction = 8;
                    }
                }
                else
                {
                    deduction = 2 * (int)Mathf.Ceil(battlePoints / 10);
                }

                character.GetCharacterStats().AddStatModifier(StatType.BATTLE_POINTS, -deduction);
            }

            SortCharactersByBattlePoints();
        }

        private void SortCharactersByBattlePoints() 
        {
            _charactersInTurn = _charactersInTurn
                .OrderBy(characterInTurn => characterInTurn.GetCharacterStats().GetStatFinalValue(StatType.BATTLE_POINTS))
                    .ToList();

            OnCharacterInTurnChanged?.Invoke(GetCharactersInTurn().ToList());
        }

        public void ResetCharacterInTurnBattlePoints()
        {
            Stats characterInTurnStats = GetCharacterInTurn().GetCharacterStats();

            characterInTurnStats.AddStatModifier(StatType.BATTLE_POINTS, -characterInTurnStats.GetStatFinalValue(StatType.BATTLE_POINTS));

            OnCharacterInTurnChanged?.Invoke(GetCharactersInTurn().ToList());
        }

        public void TriggerCharacterInTurnTemporaryModifiers() 
        {
            Character characterInTurn = GetCharacterInTurn();

            Stats characterInTurnStats = characterInTurn.GetCharacterStats();
            
            characterInTurnStats.ApplyStatModifiersByCondition((statModifier) =>
                statModifier is TemporaryStatModifier);
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