using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions;
using CharacterModule;
using CharacterModule.Stats.StatModifier.Modifiers;
using CharacterModule.Stats.StatModifier.Modifiers.Base;
using CharacterModule.Stats.Utility.Enums;
using UnityEngine;
using VContainer;

namespace BattleModule.Controllers.Modules.Turn
{
    public class BattleTurnController
    {
        private List<Character> _spawnedCharacters;

        public event Action<BattleTurnContext> OnCharactersInTurnChanged = delegate { };
        
        [Inject]
        public BattleTurnController(BattleSpawner battleSpawner,
            BattleEventManager battleEventManager)
        {
            battleSpawner.OnCharactersSpawned += OnCharactersSpawned;
            
            battleEventManager.OnTurnEnded += UpdateCharactersBattlePoints;
        }

        private void OnCharactersSpawned(List<Character> characters)
        {
            _spawnedCharacters = characters;
            
            _spawnedCharacters.ForEach(character => character.HealthManager.OnCharacterDied += OnCharacterDied);

            BaseStatModifier.SetLocalCycle(characters.Count);
        }

        private void UpdateCharactersBattlePoints()
        {
            foreach (var character in _spawnedCharacters)
            {
                float battlePoints = character.CharacterStats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue;
                
                character.CharacterStats.ApplyStatModifier(StatType.BATTLE_POINTS, CalculateDeduction(battlePoints));
            }
        }

        private static int CalculateDeduction(float battlePoints)
        {
            var tierNumber = (int) Mathf.Clamp(battlePoints / 10f, 0f, 4f);

            return -(battlePoints <= 10 * tierNumber ? 2 * tierNumber
                : battlePoints < 10 * tierNumber + 2 * tierNumber
                    ? 3 + 2 * (tierNumber - 1)
                    : 4 + 2 * (tierNumber - 1));
        }

        private void SortSpawnedCharacters()
        {
            _spawnedCharacters = _spawnedCharacters.OrderBy((character) => character.CharacterStats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue).ToList();

            ResetFirstCharacterBattlePoints();
            
            OnCharactersInTurnChanged?.Invoke(GetCurrentBattleTurnContext());
        }

        private void ResetFirstCharacterBattlePoints()
        {
            var characterInTurnStats = _spawnedCharacters.First().CharacterStats;

            characterInTurnStats.ApplyStatModifier(StatType.BATTLE_POINTS, -characterInTurnStats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue);
        }

        private void TriggerTemporaryModifiers() 
        {
            _spawnedCharacters.First().CharacterStats.ApplyStatModifiersByCondition(
                (statModifier) => statModifier is TemporaryStatModifier);
        }

        private void OnCharacterDied(Character deadCharacter)
        {
            _spawnedCharacters.Remove(deadCharacter);

            BaseStatModifier.SetLocalCycle(_spawnedCharacters.Count);
            
            OnCharactersInTurnChanged.Invoke(GetCurrentBattleTurnContext());
        }

        private BattleTurnContext GetCurrentBattleTurnContext()
        {
            return new BattleTurnContext(_spawnedCharacters.First(), _spawnedCharacters);
        }

        public void StartTurn() 
        {
            SortSpawnedCharacters();
            
            TriggerTemporaryModifiers();
        }
    }
}