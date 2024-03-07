using BattleModule.Controllers.Turn;
using UnityEngine;
using VContainer;

namespace BattleModule.Animation
{
    public class BattleAnimationManager : MonoBehaviour
    {
        private Animator _characterAnimator;

        [Inject]
        private void Init(BattleTurnController battleTurnController)
        {
            battleTurnController.OnCharactersInTurnChanged += OnCharactersInTurnChanged;
        }

        private void OnCharactersInTurnChanged(BattleTurnContext battleTurnContext)
        {
            _characterAnimator = battleTurnContext.CharacterInAction.CharacterAnimator;
        }
    }
}