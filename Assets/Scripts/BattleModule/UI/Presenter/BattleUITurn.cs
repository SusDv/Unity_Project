using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using BattleModule.Controllers;
using BattleModule.Controllers.Turn;
using BattleModule.UI.Presenter.SceneSettings.Turn;
using BattleModule.UI.View;
using CharacterModule;
using CharacterModule.Stats.Utility.Enums;
using VContainer;

namespace BattleModule.UI.Presenter 
{
    public class BattleUITurn : MonoBehaviour
    {
        private BattleTurnSceneSettings _battleTurnSceneSettings;

        private readonly List<BattleUITurnView> _battleUITurnViews = new();
        
        [Inject]
        private void Init(BattleTurnSceneSettings battleTargetingSceneSettings,
            BattleTurnController battleTurnController, BattleSpawner battleSpawner)
        {
            _battleTurnSceneSettings = battleTargetingSceneSettings;

            battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;
            
            battleSpawner.OnCharactersSpawned += OnCharactersSpawned;
        }

        private void OnCharactersSpawned(List<Character> characters)
        {
            CreateTurnPanels(characters);
        }

        private void CreateTurnPanels(List<Character> characters)
        {
            foreach (var character in characters)
            {
                var battleUITurn = Instantiate(_battleTurnSceneSettings.BattleUITurnView,
                    _battleTurnSceneSettings.BattleTurnParent.transform.position, Quaternion.identity,
                    _battleTurnSceneSettings.BattleTurnParent.transform);

                battleUITurn.SetData(character.CharacterInformation.CharacterName, character.CharacterStats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue.ToString(CultureInfo.InvariantCulture), false);
                
                _battleUITurnViews.Add(battleUITurn);
            }
        }

        private void OnCharactersInTurnChanged(BattleTurnContext battleTurnContext)
        {
            CheckCharactersCount(battleTurnContext);
            
            for (var i = 0; i < battleTurnContext.CharactersInTurn.Count; i++)
            {
                _battleUITurnViews[i].SetData(battleTurnContext.CharactersInTurn[i].CharacterInformation.CharacterName,battleTurnContext.CharactersInTurn[i].CharacterStats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue.ToString(CultureInfo.InvariantCulture), battleTurnContext.CharactersInTurn[i] == battleTurnContext.CharacterInAction);
            }
        }

        private void CheckCharactersCount(BattleTurnContext battleTurnContext)
        {
            for (int i = battleTurnContext.CharactersInTurn.Count; i < _battleUITurnViews.Count; i++)
            {
                Destroy(_battleUITurnViews[i].gameObject);
                
                _battleUITurnViews.Remove(_battleUITurnViews[i]);
            }
        }
    }
}