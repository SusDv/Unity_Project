using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Utility
{
    public class AssetLoader
    {
        private readonly Dictionary<string, GameObject> 
            _loadedAssets = new ();

        public async UniTask LoadBattleAssets()
        {
            var handle = Addressables.LoadAssetsAsync<GameObject>
                (RuntimeConstants.AssetsLabels.BattleAssetsLabel, null);

            await handle.Task;
            
            foreach (var asset in handle.Result)
            {
                _loadedAssets.Add(asset.name, asset);
            }
        }
        
        public T GetLoadedAsset<T>(string name)
        {
            var asset = _loadedAssets[name];
            
            return asset.GetComponent<T>();
        }
    }
}