using StatModule.Base;
using StatModule.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions;
using CharacterModule.Stats.StatModifier.Modifiers;
using UnityEngine;

namespace BattleModule.Controllers
{
    public class BattleTurnController
    {
        private List<Character> _spawnedCharacters;

        public event Action<List<Character>> OnCharacterToHaveTurnChanged = delegate { };
        
        public BattleTurnController()
        {
            _spawnedCharacters = BattleSpawner.Instance.GetSpawnedCharacters().OrderBy((character) => character.GetCharacterStats().GetStatFinalValue(StatType.BATTLE_POINTS)).ToList();
            
            SetupActions();
        }

        private void SetupActions()
        {
            BattleEventManager.Instance.OnTurnEnded += UpdateCharactersBattlePoints;
        }

        private void UpdateCharactersBattlePoints()
        {
            foreach (Character character in _spawnedCharacters)
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

            SortSpawnedCharacters();
        }

        private float GetTierNumber(float battlePoints) 
        {
            return Mathf.Floor(battlePoints / 10);
        }

        private void SortSpawnedCharacters()
        {
            _spawnedCharacters = _spawnedCharacters.OrderBy((character) => character.GetCharacterStats().GetStatFinalValue(StatType.BATTLE_POINTS)).ToList();

            OnCharacterToHaveTurnChanged?.Invoke(_spawnedCharacters);
        }

        private void ResetBattlePoints()
        {
            Stats characterInTurnStats = _spawnedCharacters.First().GetCharacterStats();

            characterInTurnStats.AddStatModifier(StatType.BATTLE_POINTS, -characterInTurnStats.GetStatFinalValue(StatType.BATTLE_POINTS));
            
            OnCharacterToHaveTurnChanged?.Invoke(_spawnedCharacters);
        }

        private void TriggerTemporaryModifiers() 
        {
            _spawnedCharacters.First().GetCharacterStats().ApplyStatModifiersByCondition(
                (statModifier) => statModifier is TemporaryStatModifier);
        }

        public void TurnEffects() 
        {
            ResetBattlePoints();
            TriggerTemporaryModifiers();
        }
    }
}