using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace BattleModule.Utility
{
    public interface ILoadingUnit
    {
        public UniTask Load();
    }

    public interface ILoadingUnit<in T>
    {
        public UniTask Load(T param);
    }

    public sealed class LoadingService
    {
        private async UniTask OnFinishedLoading()
        {
            int currentThreadId = Thread.CurrentThread.ManagedThreadId;
            
            int mainThreadId = PlayerLoopHelper.MainThreadId;

            if (mainThreadId != currentThreadId) 
            {
               await UniTask.SwitchToMainThread();
            }
        }

        public async UniTask BeginLoading(ILoadingUnit loadingUnit)
        {
            try 
            {
                await loadingUnit.Load();
            }
            catch
            {
                throw new Exception();
            }
            finally
            {
                await OnFinishedLoading();
            }
        }
        
        public async UniTask BeginLoading<T>(ILoadingUnit<T> loadingUnit, T param)
        {
            try 
            {
                await loadingUnit.Load(param);
            }
            catch
            {
                throw new Exception();
            }
            finally
            {
                await OnFinishedLoading();
            }
        }
    }
}