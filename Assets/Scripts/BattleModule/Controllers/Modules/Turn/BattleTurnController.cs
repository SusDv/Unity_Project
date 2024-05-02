using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions;
using BattleModule.Utility;
using CharacterModule.CharacterType.Base;
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

            BattleTimer.LocalCycle = _spawnedCharacters.Count;
        }

        private void UpdateCharactersBattlePoints()
        {
            foreach (var character in _spawnedCharacters)
            {
                float battlePoints = character.CharacterStats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue;
                
                character.CharacterStats.StatModifierManager.ApplyInstantModifier(StatType.BATTLE_POINTS, CalculateDeduction(battlePoints));
            }
            
            SortSpawnedCharacters();
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

            characterInTurnStats.StatModifierManager.ApplyInstantModifier(StatType.BATTLE_POINTS, -characterInTurnStats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue);
            
            OnCharactersInTurnChanged?.Invoke(GetCurrentBattleTurnContext());
        }

        private void TriggerTemporaryModifiers() 
        {
            _spawnedCharacters.First().CharacterStats.StatModifierManager.TriggerSealEffects();
        }

        private void OnCharacterDied(Character deadCharacter)
        {
            _spawnedCharacters.Remove(deadCharacter);

            BattleTimer.LocalCycle = _spawnedCharacters.Count;
            
            OnCharactersInTurnChanged.Invoke(GetCurrentBattleTurnContext());
        }

        private BattleTurnContext GetCurrentBattleTurnContext()
        {
            return new BattleTurnContext(_spawnedCharacters.First(), _spawnedCharacters);
        }

        public void StartTurn() 
        {
            ResetFirstCharacterBattlePoints();
            
            TriggerTemporaryModifiers();
        }
    }
}