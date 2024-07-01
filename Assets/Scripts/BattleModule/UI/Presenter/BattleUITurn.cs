using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using BattleModule.Controllers.Modules.Turn;
using BattleModule.UI.Presenter.SceneReferences.Turn;
using BattleModule.UI.View;
using CharacterModule.Types.Base;
using CharacterModule.Utility;
using Cysharp.Threading.Tasks;
using Utility;
using Utility.Constants;
using VContainer;

namespace BattleModule.UI.Presenter 
{
    public class BattleUITurn : MonoBehaviour, ILoadingUnit<List<Character>>
    {
        [SerializeField]
        private BattleTurnSceneReference _battleTurnSceneReference;

        private AssetProvider _assetProvider;
        
        private BattleTurnController _battleTurnController;

        private BattleUITurnView _battleUITurnView;
        
        private readonly List<BattleUITurnView> _battleUITurnViews = new();
        
        [Inject]
        private void Init(AssetProvider assetProvider,
            BattleTurnController battleTurnController)
        {
            _assetProvider = assetProvider;
            
            _battleTurnController = battleTurnController;
        }

        public UniTask Load(List<Character> characters)
        {
            _battleUITurnView = _assetProvider.GetAssetByName<BattleUITurnView>(RuntimeConstants.AssetsName.TurnView);

            _battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;

            CreateTurnPanels(characters);
            
            return UniTask.CompletedTask;
        }

        private void CreateTurnPanels(IEnumerable<Character> characters)
        {
            foreach (var battleUITurn in characters.Select(_ => Instantiate(_battleUITurnView,
                         _battleTurnSceneReference.BattleTurnParent.transform.position, Quaternion.identity,
                         _battleTurnSceneReference.BattleTurnParent.transform)))
            {
                _battleUITurnViews.Add(battleUITurn);
            }
        }

        private void OnCharactersInTurnChanged(BattleTurnContext battleTurnContext)
        {
            CheckCharactersCount(battleTurnContext);
            
            for (var i = 0; i < battleTurnContext.CharactersInTurn.Count; i++)
            {
                _battleUITurnViews[i].SetData(battleTurnContext.CharactersInTurn[i].BaseInformation.CharacterName,battleTurnContext.CharactersInTurn[i].Stats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue.ToString(CultureInfo.InvariantCulture), battleTurnContext.CharactersInTurn[i] == battleTurnContext.CharactersInTurn.First());
            }
        }

        private void CheckCharactersCount(BattleTurnContext battleTurnContext)
        {
            for (int i = battleTurnContext.CharactersInTurn.Count; 
                 i < _battleUITurnViews.Count; i++)
            {
                Destroy(_battleUITurnViews[i].gameObject);
                
                _battleUITurnViews.Remove(_battleUITurnViews[i]);
            }
        }
    }
}