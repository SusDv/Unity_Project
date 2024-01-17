using BattleModule.ActionCore;
using BattleModule.ActionCore.Context;
using BattleModule.Utility.Interfaces;
using System;

namespace BattleModule.Data
{
    public class BattleStatesData
    {
        private BattleAction _battleAction;

        private Character _characterInTurn;

        public Action OnBattleActionChanged;

        public BattleStatesData(Character characterInTurn) 
        {
            SetDefaultBattleAction(characterInTurn);
        }

        public Character CharacterInTurn 
        {
            get { return _characterInTurn; }
            set 
            {
                _characterInTurn = value;
                SetDefaultBattleAction(value); 
            }
        }
        
        public BattleAction BattleAction 
        { 
            get { return _battleAction; }
            set 
            {
                _battleAction = value;
                OnBattleActionChanged?.Invoke();
            } 
        }

        private void SetDefaultBattleAction(Character characterInTurn) 
        {
            ITargetable targetable = characterInTurn.GetCharacterWeapon().GetWeapon() 
                as ITargetable;

            BattleAction = BattleDefaultAction.GetBattleDefaultActionInstance(
                BattleActionContext.
                    GetBattleActionContextInstance(null, targetable));
        }
    }
}