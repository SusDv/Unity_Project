using System;
using System.Collections.Generic;
using BattleModule.Targeting.Base;
using BattleModule.Utility;
using CharacterModule.Types.Base;
using Utility;

namespace BattleModule.Targeting.Processor
{
    public class BattleTargetingProcessor
    {
        private readonly Dictionary<TargetSearchType, BattleTargeting> _targeting = new();
        
        private BattleTargeting _currentTargetingClass;

        private readonly Action<List<Character>> _targetsChangedCallback;

        public BattleTargetingProcessor(
            Action<List<Character>> targetsChangedCallback)
        {
            Init();

            _targetsChangedCallback = targetsChangedCallback;
        }

        private void Init() 
        {
            _targeting.Clear();
            
            foreach(var targeting in ReflectionUtils.GetConcreteInstances<BattleTargeting>()) 
            {
                _targeting.Add(targeting.TargetSearchType, targeting);
            }
        }

        public void SetTargetingData(TargetSearchType targetSearchType,
            List<Character> targetPool,
            int maxTargetsToSelect) 
        {
            _currentTargetingClass = _targeting[targetSearchType];
            
            _currentTargetingClass.Init(targetPool, maxTargetsToSelect);
        }

        public void PrepareTargets(int mainTargetIndex) 
        {
            _currentTargetingClass.PrepareTargets(mainTargetIndex, _targetsChangedCallback);
        }

        public List<Character> GetSelectedTargets()
        {
            return _currentTargetingClass.GetSelectedTargets(_targetsChangedCallback);
        }

        public bool TargetingComplete()
        {
            return _currentTargetingClass.TargetingComplete();
        }

        public bool CancelAction()
        {
            return _currentTargetingClass.OnCancelAction(_targetsChangedCallback);
        }
    }
}
