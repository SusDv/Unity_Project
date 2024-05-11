using System.Threading.Tasks;

namespace BattleModule.Utility
{
    public interface ILoadingUnit
    {
        public Task Load();
    }

    public interface ILoadingUnit<in T>
    {
        public Task Load(T param);
    }

    public sealed class LoadingService
    {
        public async Task BeginLoading(ILoadingUnit loadingUnit)
        {
            await loadingUnit.Load();
        }

        public async Task BeginLoading<T>(ILoadingUnit<T> loadingUnit, T param)
        {
            await loadingUnit.Load(param);
        }
    }
}