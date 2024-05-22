using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions;
using BattleModule.Utility;
using CharacterModule.Stats.Utility.Enums;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace BattleModule.Controllers.Modules.Turn
{
    public class BattleTurnController : ILoadingUnit<List<Character>>
    {
        private readonly BattleEventManager _battleEventManager;
        
        private List<Character> _spawnedCharacters;

        private readonly BattleTurnContext _battleTurnContext = new ();
        
        public event Action<BattleTurnContext> OnCharactersInTurnChanged = delegate { };
        
        [Inject]
        public BattleTurnController(BattleEventManager battleEventManager)
        {
            _battleEventManager = battleEventManager;
        }
        
        public UniTask Load(List<Character> characters)
        {
            BattleTimer.LocalCycle = characters.Count;
            
            _spawnedCharacters = characters;
            
            _battleEventManager.OnTurnEnded += UpdateCharactersBattlePoints;

            _spawnedCharacters.ForEach(character => character.HealthManager.OnCharacterDied += OnCharacterDied);

            return UniTask.CompletedTask;
        }

        public void StartTurn() 
        {
            ResetFirstCharacterBattlePoints();
            
            TriggerTemporaryModifiers();
        }
        
        private void UpdateCharactersBattlePoints()
        {
            foreach (var character in _spawnedCharacters)
            {
                float battlePoints = character.CharacterStats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue;
                
                character.CharacterStats.ApplyInstantModifier(StatType.BATTLE_POINTS, CalculateDeduction(battlePoints));
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

        private void ResetFirstCharacterBattlePoints()
        {
            var stats = _spawnedCharacters.First().CharacterStats;

            stats.ApplyInstantModifier(StatType.BATTLE_POINTS, -stats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue);
            
            OnCharactersInTurnChanged?.Invoke(GetBattleTurnContext());
        }

        private void TriggerTemporaryModifiers() 
        {
            _spawnedCharacters.First().CharacterStats.TriggerSealEffects();
        }

        private void OnCharacterDied(Character deadCharacter)
        {
            _spawnedCharacters.Remove(deadCharacter);

            BattleTimer.LocalCycle = _spawnedCharacters.Count;

            OnCharactersInTurnChanged?.Invoke(GetBattleTurnContext());
        }
        
        private void SortSpawnedCharacters()
        {
            _spawnedCharacters = _spawnedCharacters.OrderBy((character) => character.CharacterStats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue).ToList();

            ResetFirstCharacterBattlePoints();
        }

        private BattleTurnContext GetBattleTurnContext()
        {
            _battleTurnContext.CharactersInTurn = _spawnedCharacters;
            
            _battleTurnContext.CharacterInAction = _spawnedCharacters.First();

            return _battleTurnContext;
        }
    }
}