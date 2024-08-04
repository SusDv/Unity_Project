using System.Collections.Generic;
using BattleModule.Controllers.Modules;
using BattleModule.Utility.Interfaces;
using Cysharp.Threading.Tasks;
using Utility;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUIController : ILoadingUnit
    {
        private readonly BattleActionController _battleActionController;

        private readonly List<IUIElement> _uiElements = new ();
        
        [Inject]
        public BattleUIController(BattleActionController battleActionController)
        {
            _battleActionController = battleActionController;
        }

        private void ToggleUIElements()
        {
            _uiElements.ForEach(u => u.ToggleVisibility());
        }


        public UniTask Load()
        {
            _battleActionController.OnBattleActionStarted += ToggleUIElements;

            _battleActionController.OnBattleActionFinished += (_, _) => ToggleUIElements();
            
            return UniTask.CompletedTask;
        }

        public void AddAsUIElement(IUIElement uiElement)
        {
            _uiElements.Add(uiElement);
        }
    }
}