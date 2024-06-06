using System;
using System.Collections.Generic;
using System.Linq;
using BattleModule.Accuracy;
using BattleModule.Actions.Outcome;
using BattleModule.Actions.Outcome.Outcomes;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.Utility;
using CharacterModule.Types.Base;
using CharacterModule.Utility;
using Cysharp.Threading.Tasks;
using Utility;
using VContainer;

namespace BattleModule.Controllers.Modules
{
    public class BattleAccuracyController : ILoadingUnit
    {
        private readonly Dictionary<Character, BattleAccuracy> _battleAccuracies = new ();

        private readonly BattleTurnController _battleTurnController;
        
        public event Action<Dictionary<Character, BattleAccuracy>> OnAccuraciesChanged = delegate { };
        
        [Inject]
        private BattleAccuracyController(BattleTurnController battleTurnController)
        {
            _battleTurnController = battleTurnController;
        }
        
        private void OnCharactersInTurnChanged(BattleTurnContext battleTurnContext)
        {
            _battleAccuracies.Clear();
            
            float accuracy = battleTurnContext.CharactersInTurn.First().Stats.GetStatInfo(StatType.ACCURACY).FinalValue;
 
            foreach (var character in battleTurnContext.CharactersInTurn)
            {
                float evasion = character.Stats.GetStatInfo(StatType.EVASION).FinalValue;
                
                _battleAccuracies.Add(character, 
                    new BattleAccuracy().Init(
                        accuracy, 
                        evasion, 
                        character.GetType() == battleTurnContext.CharactersInTurn.First().GetType()));
            }
            
            OnAccuraciesChanged?.Invoke(_battleAccuracies);
        }
        
        public UniTask Load()
        {
            _battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;
                
            return UniTask.CompletedTask;
        }
        
        public Dictionary<Character, BattleAccuracy> GetAccuracies()
        {
            return _battleAccuracies;
        }
    }
}