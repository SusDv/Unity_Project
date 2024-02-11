using StatModule.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Actions;
using BattleModule.Utility.Interfaces;
using CharacterModule.Stats.Base;
using CharacterModule.Stats.StatModifier.Modifiers;
using UnityEngine;
using VContainer;

namespace BattleModule.Controllers
{
    public class BattleTurnController
    {
        private List<Character> _spawnedCharacters;

        private List<ICharacterInTurnObserver> _characterInTurnObservers;
        
        [Inject]
        public BattleTurnController(BattleSpawner battleSpawner)
        {
            _characterInTurnObservers = new List<ICharacterInTurnObserver>();
            
            _spawnedCharacters = battleSpawner.GetSpawnedCharacters().OrderBy((character) => character.GetCharacterStats().GetStatFinalValue(StatType.BATTLE_POINTS)).ToList();
            
            SetupActions();
        }

        public void AddCharacterInTurnObserver(ICharacterInTurnObserver observer)
        {
            _characterInTurnObservers.Add(observer);
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

            _characterInTurnObservers.ForEach(o => o.Notify(_spawnedCharacters));
        }

        private void ResetBattlePoints()
        {
            Stats characterInTurnStats = _spawnedCharacters.First().GetCharacterStats();

            characterInTurnStats.AddStatModifier(StatType.BATTLE_POINTS, -characterInTurnStats.GetStatFinalValue(StatType.BATTLE_POINTS));
            
            _characterInTurnObservers.ForEach(o => o.Notify(_spawnedCharacters));
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