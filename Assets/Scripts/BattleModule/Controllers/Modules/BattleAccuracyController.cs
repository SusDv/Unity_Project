using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Accuracy;
using BattleModule.Controllers.Modules.Turn;
using CharacterModule.CharacterType.Base;
using CharacterModule.Stats.Utility.Enums;
using VContainer;

namespace BattleModule.Controllers.Modules
{
    public class BattleAccuracyController
    {
        private readonly Dictionary<Character, BattleAccuracy> _battleAccuracies = new ();

        public event Action<Dictionary<Character, BattleAccuracy>> OnAccuraciesChanged = delegate { };

        [Inject]
        private BattleAccuracyController(BattleTurnController battleTurnController)
        {
            battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;
        }

        private void OnCharactersInTurnChanged(BattleTurnContext battleTurnContext)
        {
            GenerateAccuracies(battleTurnContext.CharacterInAction, battleTurnContext.CharactersInTurn);
        }

        private void GenerateAccuracies(Character characterInTurn, List<Character> characters)
        {
            float accuracy = characterInTurn.CharacterStats.GetStatInfo(StatType.ACCURACY).FinalValue;
 
            foreach (var character in characters)
            {
                float evasion = character.CharacterStats.GetStatInfo(StatType.EVASION).FinalValue;
                
                _battleAccuracies.Add(character, new BattleAccuracy().Init(accuracy, evasion, character.GetType() == characterInTurn.GetType()));
            }
            
            OnAccuraciesChanged?.Invoke(_battleAccuracies);
        }

        public Dictionary<Character, BattleAccuracy> GetAccuracies()
        {
            return _battleAccuracies;
        }
    }
}