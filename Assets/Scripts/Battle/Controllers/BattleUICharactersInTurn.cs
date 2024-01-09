using BattleModule.ActionCore.Events;
using StatModule.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleModule.Controllers
{
    public class BattleUICharactersInTurn
    {
        private List<Character> _charactersInTurn;

        public Action OnCharacterInTurnChanged;

        public BattleUICharactersInTurn(List<Character> characters)
        {
            _charactersInTurn = new List<Character>(characters);

            SetupBattleActions();
        }

        private void SetupBattleActions()
        {
            BattleGlobalActionEvent.OnTurnEnded += UpdateCharactersBattlePoints;
        }

        private void UpdateCharactersBattlePoints()
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

                character.GetStats().ModifyStat(StatType.BATTLE_POINTS, -deduction);
            }

            OnCharacterInTurnChanged?.Invoke();
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