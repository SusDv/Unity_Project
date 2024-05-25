using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Utility;
using CharacterModule.Types.Base;
using CharacterModule.Utility;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace BattleModule.Controllers.Modules.Turn
{
    public class BattleTurnController : ILoadingUnit<List<Character>>
    {
        private readonly BattleTimerController _battleTimerController;

        private readonly BattleTurnEvents _battleTurnEvents;
        
        private List<Character> _spawnedCharacters;
        
        public event Action<BattleTurnContext> OnCharactersInTurnChanged = delegate { };
        
        [Inject]
        public BattleTurnController(BattleTimerController battleTimerController,
            BattleTurnEvents battleTurnEvents)
        {
            _battleTimerController = battleTimerController;

            _battleTurnEvents = battleTurnEvents;
        }
        
        private void UpdateCharactersBattlePoints()
        {
            foreach (var character in _spawnedCharacters)
            {
                float battlePoints = character.Stats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue;
                
                character.Stats.ApplyInstantModifier(StatType.BATTLE_POINTS, CalculateDeduction(battlePoints));
            }

            SortCharacters();
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
            _spawnedCharacters.First().Stats.ResetStatValue(StatType.BATTLE_POINTS);
            
            OnCharactersInTurnChanged?.Invoke(GetBattleTurnContext());
        }

        private void OnCharacterDied(Character deadCharacter)
        {
            _spawnedCharacters.Remove(deadCharacter);

            _battleTimerController.SetLocalCycle(_spawnedCharacters.Count);

            OnCharactersInTurnChanged?.Invoke(GetBattleTurnContext());
        }
        
        private void SortCharacters()
        {
            _spawnedCharacters = _spawnedCharacters.OrderBy((character) => character.Stats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue).ToList();
        }

        private BattleTurnContext GetBattleTurnContext()
        {
            return new BattleTurnContext
            {
                CharactersInTurn = _spawnedCharacters
            };
        }

        public void TriggerTurnEffects()
        {
            ResetFirstCharacterBattlePoints();
            
            _spawnedCharacters.ForEach(c => c.Stats.TriggerSealEffects());
        }

        public UniTask Load(List<Character> characters)
        {
            _spawnedCharacters = characters;
            
            _spawnedCharacters.ForEach(character =>
            {
                character.Stats.SetBattleTimerFactory(_battleTimerController.CreateTimer);
                
                character.HealthManager.OnCharacterDied += OnCharacterDied;
            });
            
            _battleTimerController.SetLocalCycle(characters.Count);

            _battleTurnEvents.OnTurnEnd += UpdateCharactersBattlePoints;

            return UniTask.CompletedTask;
        }
    }
}