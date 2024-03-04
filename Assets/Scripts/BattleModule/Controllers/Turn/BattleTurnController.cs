using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions;
using CharacterModule;
using CharacterModule.Stats.StatModifier.Modifiers;
using StatModule.Utility.Enums;
using UnityEngine;
using VContainer;

namespace BattleModule.Controllers.Turn
{
    public class BattleTurnController
    {
        private List<Character> _spawnedCharacters;

        public event Action<BattleTurnContext> OnCharactersInTurnChanged = delegate { };
        
        [Inject]
        public BattleTurnController(BattleSpawner battleSpawner)
        {
            _spawnedCharacters = battleSpawner.GetSpawnedCharacters().OrderBy((character) => character.GetCharacterStats().GetStatInfo(StatType.BATTLE_POINTS).FinalValue).ToList();

            _spawnedCharacters.ForEach(character => character.GetHealthManager().OnCharacterDied += OnCharacterDied);
            
            BattleEventManager.Instance.OnTurnEnded += UpdateCharactersBattlePoints;
        }

        private void UpdateCharactersBattlePoints()
        {
            foreach (var character in _spawnedCharacters)
            {
                float battlePoints = character.GetCharacterStats().GetStatInfo(StatType.BATTLE_POINTS).FinalValue;

                int deduction = CalculateDeduction(battlePoints);

                character.GetCharacterStats().ApplyStatModifier(StatType.BATTLE_POINTS, -deduction);
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

            OnCharactersInTurnChanged?.Invoke(GetCurrentBattleTurnContext());
        }

        private void ResetBattlePoints()
        {
            var characterInTurnStats = _spawnedCharacters.First().GetCharacterStats();

            characterInTurnStats.ApplyStatModifier(StatType.BATTLE_POINTS, -characterInTurnStats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue);
            
            OnCharactersInTurnChanged?.Invoke(GetCurrentBattleTurnContext());
        }

        private void TriggerTemporaryModifiers() 
        {
            _spawnedCharacters.First().GetCharacterStats().ApplyStatModifiersByCondition(
                (statModifier) => statModifier is TemporaryStatModifier);
        }

        private void OnCharacterDied(Character deadCharacter)
        {
            _spawnedCharacters.Remove(deadCharacter);
            
            OnCharactersInTurnChanged.Invoke(GetCurrentBattleTurnContext());
        }

        private BattleTurnContext GetCurrentBattleTurnContext()
        {
            return new BattleTurnContext(_spawnedCharacters.First(), _spawnedCharacters);
        }

        public void TurnEffects() 
        {
            ResetBattlePoints();
            TriggerTemporaryModifiers();
        }
    }
}