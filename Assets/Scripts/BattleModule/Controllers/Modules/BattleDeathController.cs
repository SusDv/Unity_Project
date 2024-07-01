using System;
using System.Collections.Generic;
using CharacterModule.Animation;
using CharacterModule.Types.Base;
using CharacterModule.Utility;
using CharacterModule.Utility.Stats;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;
using Utility.ObserverPattern;

namespace BattleModule.Controllers.Modules
{
    public class BattleDeathController : ILoadingUnit<List<Character>>
    {
        public event Action<GameObject> OnCharacterDied = delegate { };

        private void CharacterDied(GameObject gameObject)
        {
            OnCharacterDied?.Invoke(gameObject);
        }

        public UniTask Load(List<Character> characters)
        {
            foreach (var character in characters)
            {
                character.Stats.AttachObserver(
                    HealthObserver.CreateInstance(character, CharacterDied));
            }
            
            return UniTask.CompletedTask;
        }
    }
    
    public sealed class HealthObserver : IStatObserver
    {
        private readonly GameObject _observableCharacter;

        private readonly AnimationManager _animationManager;

        private readonly Action<GameObject> _characterDiedAction;
        
        private HealthObserver(Character character,
            Action<GameObject> characterDiedAction)
        {
            _observableCharacter = character.gameObject;

            _animationManager = character.AnimationManager;

            _characterDiedAction = characterDiedAction;
        }
        
        public StatType StatType => StatType.HEALTH;
        
        public void UpdateValue(StatInfo info, bool negativeChange = false)
        {
            if (negativeChange && info.FinalValue > 0)
            {
                _animationManager.PlayAnimation("Damaged").Forget();
                
                return;
            }

            if (!(info.FinalValue <= 0))
            {
                return;
            }

            _characterDiedAction?.Invoke(_observableCharacter);
        }

        public static HealthObserver CreateInstance(Character character,
            Action<GameObject> characterDiedAction)
        {
            return new HealthObserver(character, characterDiedAction);
        }
    }
}