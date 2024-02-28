using System;
using StatModule.Utility.Enums;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions;
using CharacterModule;
using CharacterModule.Stats.StatModifier.Modifiers;
using UnityEngine;
using VContainer;

namespace BattleModule.Controllers
{
    public class BattleTurnController
    {
        private List<Character> _spawnedCharacters;

        public event Action<List<Character>> OnCharactersInTurnChanged = delegate { };
        
        [Inject]
        public BattleTurnController(BattleSpawner battleSpawner)
        {
            _spawnedCharacters = battleSpawner.GetSpawnedCharacters().OrderBy((character) => character.GetCharacterStats().GetStatInfo(StatType.BATTLE_POINTS).FinalValue).ToList();

            BattleEventManager.Instance.OnTurnEnded += UpdateCharactersBattlePoints;
        }

        private void UpdateCharactersBattlePoints()
        {
            foreach (var character in _spawnedCharacters)
            {
                float battlePoints = character.GetCharacterStats().GetStatInfo(StatType.BATTLE_POINTS).FinalValue;

                int deduction = CalculateDeduction(battlePoints);

                character.GetCharacterStats().AddStatModifier(StatType.BATTLE_POINTS, -deduction);
            }

            SortSpawnedCharacters();
        }

        private int CalculateDeduction(float battlePoints)
        {
            int deduction;
            
            var tierNumber = (int) Mathf.Clamp(battlePoints / 10f, 0f, 4f);

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

            return deduction;
        }

        private void SortSpawnedCharacters()
        {
            _spawnedCharacters = _spawnedCharacters.OrderBy((character) => character.GetCharacterStats().GetStatInfo(StatType.BATTLE_POINTS).FinalValue).ToList();

            OnCharactersInTurnChanged?.Invoke(_spawnedCharacters);
        }

        private void ResetBattlePoints()
        {
            var characterInTurnStats = _spawnedCharacters.First().GetCharacterStats();

            characterInTurnStats.AddStatModifier(StatType.BATTLE_POINTS, -characterInTurnStats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue);
            
            OnCharactersInTurnChanged?.Invoke(_spawnedCharacters);
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