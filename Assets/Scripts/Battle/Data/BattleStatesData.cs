using BattleModule.ActionCore;
using BattleModule.ActionCore.Context;
using BattleModule.ActionCore.Interfaces;
using BattleModule.Utility.Interfaces;
using InventorySystem.Item;
using System;

namespace BattleModule.Data
{
    public class BattleStatesData : IBattleAction
    {
        private Character _characterInTurn;

        private BattleAction _battleAction;

        public Action OnBattleActionChanged { get; set; } = delegate { };

        public BattleStatesData(Character characterInTurn) 
        {
            SetDefaultBattleAction(characterInTurn);
        }

        public Character CharacterInTurn 
        {
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
            WeaponItem characterWeapon = characterInTurn.GetCharacterWeapon().GetWeapon();

            BattleAction = BattleDefaultAction.GetBattleDefaultActionInstance(
                BattleActionContext.
                    GetBattleActionContextInstance(characterWeapon));
        }
    }
}