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

        public Action OnCharacterInTurnChanged;

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
                float battlePoints = character.GetStats().GetStatFinalValue(StatType.BATTLE_POINTS);

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

                character.GetStats().ApplyStatModifier(StatType.BATTLE_POINTS, -deduction);
            }

            OnCharacterInTurnChanged?.Invoke();
        }

        public void TriggetCharacterInTurnTemporaryModifiers() 
        {
            Stats characterInTurnStats = GetCharacterInTurn().GetStats();

            characterInTurnStats.GetBaseStatModifiers().Where((statModifier) => 
                statModifier is TemporaryStatModifier)
                    .ToList()
                        .ForEach((temporaryStatModifier) => 
                            characterInTurnStats.ApplyStatModifier(temporaryStatModifier));
        }

        public IList<Character> GetCharactersInTurn()
        {
            return _charactersInTurn
                    .OrderBy(character =>
                        character.GetStats().GetStatFinalValue(StatType.BATTLE_POINTS))
                            .ToList()
                                .AsReadOnly();
        }

        public Character GetCharacterInTurn()
        {
            return GetCharactersInTurn().First();
        }
    }
}