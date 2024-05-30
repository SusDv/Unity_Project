using System.Collections.Generic;
using BattleModule.Actions.BattleActions.Outcome;
using BattleModule.Controllers.Modules;
using BattleModule.UI.View;
using BattleModule.Utility;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;
using Utility.Constants;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUIOutcome : MonoBehaviour, ILoadingUnit
    {
        private AssetLoader _assetLoader;
        
        private BattleUIHelper _battleUIHelper;
        
        private BattleActionController _battleActionController;
        
        private BattleUIOutcomeView _battleUIOutcomeView;
        
        [Inject]
        private void Init(AssetLoader assetLoader,
            BattleUIHelper battleUIHelper,
            BattleActionController battleActionController)
        {
            _assetLoader = assetLoader;

            _battleUIHelper = battleUIHelper;
            
            _battleActionController = battleActionController;
        }

        private void ShowActionOutcomes(Dictionary<Character, BattleActionOutcome> battleActionOutcomes)
        {
            foreach (var outcome in battleActionOutcomes)
            {
                var outcomeView = Instantiate(_battleUIOutcomeView, outcome.Key.transform.position + Vector3.up * 2, Quaternion.identity, _battleUIHelper.WorldCanvas.transform);
                
                outcomeView.SetData(outcome.Value);
            }
        }

        public UniTask Load()
        {
            _battleUIOutcomeView =
                _assetLoader.GetLoadedAsset<BattleUIOutcomeView>(RuntimeConstants.AssetsName.OutcomeView);
            
            _battleActionController.OnBattleActionFinished += ShowActionOutcomes;
            
            return UniTask.CompletedTask;
        }
    }
}