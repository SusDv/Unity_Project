using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions;
using BattleModule.Utility;
using CharacterModule.CharacterType.Base;
using CharacterModule.Stats.Utility.Enums;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace BattleModule.Controllers.Modules.Turn
{
    public class BattleTurnController : ILoadingUnit<List<Character>>
    {
        private readonly BattleEventManager _battleEventManager;
        
        private List<Character> _spawnedCharacters;

        private BattleTurnContext _battleTurnContext;
        
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
            
            _battleTurnContext = new BattleTurnContext();
            
            SetBattleTurnContext();
            
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
                
                character.CharacterStats.StatModifierManager.ApplyInstantModifier(StatType.BATTLE_POINTS, CalculateDeduction(battlePoints));
            }
            
            SetBattleTurnContext();
            
            OnCharactersInTurnChanged?.Invoke(_battleTurnContext);
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
            var characterInTurnStats = _battleTurnContext.CharacterInAction.CharacterStats;

            characterInTurnStats.StatModifierManager.ApplyInstantModifier(StatType.BATTLE_POINTS, -characterInTurnStats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue);

            OnCharactersInTurnChanged?.Invoke(_battleTurnContext);
        }

        private void TriggerTemporaryModifiers() 
        {
            _spawnedCharacters.First().CharacterStats.StatModifierManager.TriggerSealEffects();
        }

        private void OnCharacterDied(Character deadCharacter)
        {
            _spawnedCharacters.Remove(deadCharacter);

            BattleTimer.LocalCycle = _spawnedCharacters.Count;
            
            SetBattleTurnContext();
            
            OnCharactersInTurnChanged?.Invoke(_battleTurnContext);
        }

        private void SetBattleTurnContext()
        {
            var orderedCharacters = _spawnedCharacters.OrderBy((character) =>
                character.CharacterStats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue).ToList();

            _battleTurnContext.CharactersInTurn = orderedCharacters;

            _battleTurnContext.CharacterInAction = orderedCharacters.First();
        }
    }
}