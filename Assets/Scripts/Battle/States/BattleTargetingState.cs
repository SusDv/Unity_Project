using BattleModule.ActionCore.Events;
using System;
using UnityEngine;

public class BattleTargetingState : BattleState
{
    public BattleTargetingState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
    {

    }

    public override void OnEnter()
    {
        BattleGlobalActionEvent.OnBattleAction += BattleActionHandler;
        AutoSelectEnemy();
        base.OnEnter();
    }
    public override void OnExit()
    {
        BattleGlobalActionEvent.OnBattleAction -= BattleActionHandler;
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        battleStateMachine.BattleController.Data.SelectedCharacter = SelectEnemyOnScene();
    }
    protected Character SelectEnemyOnScene()
    {
        Character selectedCharacter = battleStateMachine.BattleController.Data.SelectedCharacter;

        selectedCharacter = SelectCharacterUsingKeys();

        if (!battleStateMachine.BattleController.BattleInput.BattleActions.LeftMouseButton.WasPressedThisFrame())
        {
            battleStateMachine.BattleController.OnCharacterTargetChanged?.Invoke(selectedCharacter.gameObject.transform.position);
            return selectedCharacter;
        }

        RaycastHit characterHit;

        Ray characterRay = battleStateMachine.BattleController.GetBattleCamera().ScreenPointToRay(battleStateMachine.BattleController.BattleInput.MousePosition);

        if (Physics.Raycast(characterRay, out characterHit, battleStateMachine.BattleController.GetBattleCamera().farClipPlane, battleStateMachine.BattleController.CharacterLayerMask))
        {
            if (!characterHit.collider.GetComponent<Player>())
            {
                selectedCharacter = characterHit.collider.GetComponent<Character>();
            }
        }

        battleStateMachine.BattleController.OnCharacterTargetChanged?.Invoke(selectedCharacter.gameObject.transform.position);
        
        return selectedCharacter;
    }

    private void AutoSelectEnemy()
    {
        battleStateMachine.BattleController.Data.SelectedCharacter = battleStateMachine.BattleController.BattleCharactersOnScene.
            GetMiddleEnemyOnScene(battleStateMachine.BattleController.BattleCharactersInTurn.GetCharacterInTurn().GetType());
    }
    private Character SelectCharacterUsingKeys()
    {
        if (ArrowKeysInput == 0)
        {
            return battleStateMachine.BattleController.Data.SelectedCharacter;
        }

        return battleStateMachine.BattleController.BattleCharactersOnScene.GetNearbyCharacter(battleStateMachine.BattleController.Data.SelectedCharacter, ArrowKeysInput);
    }
    private void BattleActionHandler() 
    {
        BattleGlobalActionEvent.BattleAction.PerformAction(battleStateMachine.BattleController.BattleCharactersInTurn.GetCharacterInTurn(), battleStateMachine.BattleController.Data.SelectedCharacter);

        BattleGlobalActionEvent.AdvanceTurn();
        
        battleStateMachine.ChangeState(battleStateMachine.BattleIdleState);
    }
}
