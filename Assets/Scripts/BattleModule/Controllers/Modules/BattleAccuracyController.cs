using System;
using System.Collections.Generic;
using BattleModule.Accuracy;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.Utility;
using CharacterModule.Stats.Utility.Enums;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using VContainer;

namespace BattleModule.Controllers.Modules
{
    public class BattleAccuracyController : ILoadingUnit
    {
        private readonly Dictionary<Character, BattleAccuracy> _battleAccuracies = new ();

        private readonly BattleTurnController _battleTurnController;
        
        public event Action<Dictionary<Character, BattleAccuracy>> OnAccuraciesChanged = delegate { };
        
        public UniTask Load()
        {
            _battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;
                
            return UniTask.CompletedTask;
        }
        
        public Dictionary<Character, BattleAccuracy> GetAccuracies()
        {
            return _battleAccuracies;
        }
        
        [Inject]
        private BattleAccuracyController(BattleTurnController battleTurnController)
        {
            _battleTurnController = battleTurnController;
        }

        private void OnCharactersInTurnChanged(BattleTurnContext battleTurnContext)
        {
            _battleAccuracies.Clear();
            
            float accuracy = battleTurnContext.CharacterInAction.CharacterStats.GetStatInfo(StatType.ACCURACY).FinalValue;
 
            foreach (var character in battleTurnContext.CharactersInTurn)
            {
                float evasion = character.CharacterStats.GetStatInfo(StatType.EVASION).FinalValue;
                
                _battleAccuracies.Add(character, 
                    new BattleAccuracy().Init(
                    accuracy, 
                    evasion, 
                    character.GetType() == battleTurnContext.CharacterInAction.GetType()));
            }
            
            OnAccuraciesChanged?.Invoke(_battleAccuracies);
        }
    }
}