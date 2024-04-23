using UnityEngine.AddressableAssets;

namespace Utility
{
    public static class AssetLoader
    {
        public static T Load<T>(string assetName)
            where T : class
        {
            return Addressables.LoadAssetAsync<T>(assetName) as T;
        }

        public static void ClearCache()
        {
            Addressables.ClearResourceLocators();
        }
    }
}