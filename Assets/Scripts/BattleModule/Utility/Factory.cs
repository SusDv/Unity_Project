using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleModule.Utility
{
    public class Factory
    {
        private readonly IObjectResolver _container;

        public Factory(IObjectResolver container)
        {
            _container = container;
        }

        public T CreateInstance<T>(T prefab,
            Vector3 position, Quaternion rotation, Transform parent)
            where T : MonoBehaviour
        {
            return _container.Instantiate(prefab, position, rotation, parent);
        }
    }
}