using System.Collections.Generic;
using System.Linq;
using BattleModule.Input;
using BattleModule.Utility.Interfaces;
using Cysharp.Threading.Tasks;
using Utility;
using VContainer;

namespace BattleModule.Controllers.Modules
{
    public class BattleCancelableController : ILoadingUnit
    {
        private readonly BattleInput _battleInput;
        
        [Inject]
        private BattleCancelableController(BattleInput battleInput)
        {
            _battleInput = battleInput;
        }

        private List<IBattleCancelable> _cancelableList = new();
        

        private void CancelAction()
        {
            for (var i = _cancelableList.Count - 1; i >= 0; i--)
            {
                if (!_cancelableList[i].TryCancel())
                {
                    continue;
                }

                TryRemoveCancelable(_cancelableList[i]);

                break;
            }
        }

        private bool TryRemoveCancelable(IBattleCancelable battleCancelable)
        {
            if (_cancelableList.IndexOf(battleCancelable) == 0
                || !_cancelableList.Contains(battleCancelable))
            {
                return false;
            }
            
            _cancelableList.Remove(battleCancelable);

            return true;
        }

        public UniTask Load()
        {
            _battleInput.OnCancelButtonPressed += CancelAction;
            
            return UniTask.CompletedTask;
        }

        public void TryAppendCancelable(IBattleCancelable battleCancelable)
        {
            if (TryRemoveCancelable(battleCancelable))
            {
                return;
            }

            _cancelableList.Add(battleCancelable);
        }
        
        public void PrependCancelable(IBattleCancelable battleCancelable)
        {
            _cancelableList = _cancelableList.Prepend(battleCancelable).ToList();
        }
    }
}