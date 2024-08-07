using System.Collections.Generic;
using UnityEngine;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.UI.Presenter.SceneReferences.Turn;
using BattleModule.UI.View;
using BattleModule.Utility.Interfaces;
using CharacterModule.Types.Base;
using CharacterModule.Utility;
using Cysharp.Threading.Tasks;
using Utility;
using Utility.Animation;
using Utility.Constants;
using VContainer;

namespace BattleModule.UI.Presenter 
{
    public class BattleUITurn : MonoBehaviour, ILoadingUnit<List<Character>>, IUIElement
    {
        [SerializeField]
        private BattleTurnSceneReference _battleTurnSceneReference;

        private AssetProvider _assetProvider;
        
        private BattleTurnController _battleTurnController;

        private BattleUIController _battleUIController;

        private BattleUITurnView _battleUITurnView;

        private BattleUITurnView _previewTurn;
        
        private readonly List<BattleUITurnView> _battleUITurnViews = new();
        
        [Inject]
        private void Init(AssetProvider assetProvider,
            BattleTurnController battleTurnController,
            BattleUIController battleUIController)
        {
            _assetProvider = assetProvider;
            
            _battleTurnController = battleTurnController;

            _battleUIController = battleUIController;
        }

        public UniTask Load(List<Character> characters)
        {
            _battleUITurnView = _assetProvider.GetAssetByName<BattleUITurnView>(RuntimeConstants.AssetsName.TurnView);

            _battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;
            
            _battleUIController.AddAsUIElement(this);

            CreateTurnPanels(characters);
            
            return UniTask.CompletedTask;
        }

        private void CreateTurnPanels(IReadOnlyList<Character> characters)
        {
            for (var i = 0; i < characters.Count; i++)
            {
                _battleUITurnViews.Add(Instantiate(_battleUITurnView,
                    _battleTurnSceneReference.BattleTurnParent.transform));
                
                if (i == 0)
                {
                    continue;
                }

                _battleUITurnViews[i].SetPosition(_battleUITurnViews[i - 1], i);
            }

            //_previewTurn = Instantiate(_battleUITurnView, _battleTurnSceneReference.BattleTurnParent.transform);
            
        }

        private void OnCharactersInTurnChanged(BattleTurnContext battleTurnContext)
        {
            CheckCharactersCount(battleTurnContext);
            
            for (var i = 0; i < battleTurnContext.CharactersInTurn.Count; i++)
            {
                _battleUITurnViews[i]
                    .SetData(battleTurnContext.CharactersInTurn[i].BaseInformation, battleTurnContext.CharactersInTurn[i].Stats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue);
            }
        }

        private void CheckCharactersCount(BattleTurnContext battleTurnContext)
        {
            for (var i = battleTurnContext.CharactersInTurn.Count; 
                 i < _battleUITurnViews.Count; i++)
            {
                Destroy(_battleUITurnViews[i].gameObject);
                
                _battleUITurnViews.Remove(_battleUITurnViews[i]);
            }
        }

        public void ToggleVisibility(bool visibility)
        {
            AnimationService.SlideOut(_battleTurnSceneReference.BattleTurnPanel.GetComponent<RectTransform>(),
                Vector2.right, 0.3f, visibility).Forget();
        }
    }
}