using System;
using System.Collections.Generic;
using BattleModule.ActionCore;
using BattleModule.ActionCore.Context;
using BattleModule.ActionCore.Events;
using BattleModule.ActionCore.Interfaces;

namespace BattleModule.Controllers
{
    public class BattleActionController : IBattleAction
    {
        private BattleAction _currentBattleAction;

        private Character _characterInTurn;

        public event Action<BattleActionContext> OnBattleActionChanged;

        public BattleActionController(ref Action<Character> characterInTurnChanged) 
        {
            characterInTurnChanged += OnCharacterInTurnChanged;
        }

        public void SetBattleAction<T>(object actionObject)
            where T : BattleAction
        {
            _currentBattleAction = Activator.CreateInstance<T>().GetInstance(actionObject);

            OnBattleActionChanged?.Invoke(_currentBattleAction.GetBattleActionContext());
        }

        public void ResetBattleAction() 
        {
            SetDefaulBattleAction();
        }

        public void ExecuteBattleAction(List<Character> targets) 
        {
            _currentBattleAction.PerformAction(_characterInTurn.GetCharacterStats(), targets);

            BattleGlobalEventManager.Instance.AdvanceTurn();
        }

        private void SetDefaulBattleAction() 
        {
            SetBattleAction<BattleDefaultAction>(_characterInTurn.
                GetCharacterWeapon().GetWeapon());
        }

        private void OnCharacterInTurnChanged(Character characterInTurn) 
        {
            _characterInTurn = characterInTurn;

            SetDefaulBattleAction();
        }
    }
}
