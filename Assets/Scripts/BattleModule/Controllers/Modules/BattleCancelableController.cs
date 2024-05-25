using System.Collections.Generic;
using System.Linq;
using BattleModule.Input;
using BattleModule.Utility;
using BattleModule.Utility.Interfaces;
using Cysharp.Threading.Tasks;
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
        
        private void HandleCancelAction()
        {
            for (int i = _cancelableList.Count - 1; i >= 0; i--)
            {
                if (!_cancelableList[i].Cancel())
                {
                    break;
                }
            }
        }

        public UniTask Load()
        {
            _battleInput.OnCancelButtonPressed += HandleCancelAction;
            
            return UniTask.CompletedTask;
        }

        public void AppendCancelable(IBattleCancelable battleCancelable)
        {
            _cancelableList.Add(battleCancelable);
        }
        
        public void PrependCancelable(IBattleCancelable battleCancelable)
        {
            _cancelableList = _cancelableList.Prepend(battleCancelable).ToList();
        }
    }
}