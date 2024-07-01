using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utility.Constants;

namespace Utility
{
    public class AssetProvider
    {
        private readonly Dictionary<string, GameObject> 
            _loadedAssets = new ();

        private void PopulateAssetDictionary(GameObject loadedAsset)
        {
            _loadedAssets.Add(loadedAsset.name, loadedAsset);
        }

        public async UniTask Load()
        { 
            await Addressables.LoadAssetsAsync<GameObject>(RuntimeConstants.AssetsLabels.BattleAssetsLabel, PopulateAssetDictionary);
        }
        
        public T GetAssetByName<T>(string name)
        {
            return _loadedAssets[name].GetComponent<T>();
        }
    }
}