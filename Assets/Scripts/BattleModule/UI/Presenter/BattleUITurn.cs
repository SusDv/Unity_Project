using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using BattleModule.Controllers;
using BattleModule.UI.Presenter.SceneSettings.Turn;
using VContainer;

namespace BattleModule.UI.Presenter 
{
    public class BattleUITurn : MonoBehaviour
    {
        private BattleTurnSceneSettings _battleTurnSceneSettings;
        
        [Inject]
        private void Init(BattleTurnSceneSettings battleTargetingSceneSettings,
            BattleTurnController battleTurnController)
        {
            _battleTurnSceneSettings = battleTargetingSceneSettings;

            battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;
        }

        private void ClearTurnPanel()
        {
            for (var i = 0; i < _battleTurnSceneSettings.BattleTurnParent.transform.childCount; i++)
            {
                Destroy(_battleTurnSceneSettings.BattleTurnParent.transform.GetChild(i).gameObject);
            }
        }

        private void OnCharactersInTurnChanged(List<Character> charactersToHaveTurn)
        {
            ClearTurnPanel();

            foreach (var character in charactersToHaveTurn)
            {
                var battleUITurn = Instantiate(_battleTurnSceneSettings.BattleUITurnView,
                    _battleTurnSceneSettings.BattleTurnParent.transform.position, Quaternion.identity,
                    _battleTurnSceneSettings.BattleTurnParent.transform);

                battleUITurn.SetData(character.gameObject.name, character.GetCharacterStats().GetStatInfo(StatModule.Utility.Enums.StatType.BATTLE_POINTS).FinalValue.ToString(CultureInfo.InvariantCulture), charactersToHaveTurn.First().name == character.name);
            }
        }
    }
}