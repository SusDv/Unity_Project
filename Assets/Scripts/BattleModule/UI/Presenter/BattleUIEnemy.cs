using System.Collections.Generic;
using System.Linq;
using BattleModule.UI.Presenter.SceneReferences.Enemy;
using BattleModule.UI.View;
using CharacterModule.Types;
using CharacterModule.Types.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;
using Utility.Constants;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUIEnemy : MonoBehaviour, ILoadingUnit<List<Character>>
    {
        [SerializeField]
        private BattleEnemySceneReference _battleEnemySceneReference;
        
        private AssetLoader _assetLoader;
        
        private BattleUIEnemyView _battleUIEnemyView;
        
        private List<Character> _enemyCharacters;

        private List<BattleUIEnemyView> _battleUIEnemies = new();
        
        [Inject]
        private void Init(AssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }

        public UniTask Load(List<Character> characters)
        {
            _battleUIEnemyView = _assetLoader.GetLoadedAsset<BattleUIEnemyView>(RuntimeConstants.AssetsName.EnemyView);
            
            CreateBattleUIEnemies(characters.Where(c => c is Enemy));
            
            return UniTask.CompletedTask;
        }

        private void CreateBattleUIEnemies(IEnumerable<Character> enemies)
        {
            foreach (var enemy in enemies)
            {
                var battleUICharacterView = Instantiate(_battleUIEnemyView, enemy.gameObject.transform);
                
                _battleUIEnemies.Add(battleUICharacterView);

                battleUICharacterView.SetData(enemy.Stats);
            }
        }
    }
}
