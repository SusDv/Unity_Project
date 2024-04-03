using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BattleModule.Controllers.Targeting.Base;
using BattleModule.Utility;
using CharacterModule;

namespace BattleModule.Controllers.Targeting.Processor
{
    public class BattleTargetingProcessor
    {
        private readonly Dictionary<TargetSearchType, BattleTargeting> _targeting = new();
        
        private BattleTargeting _currentTargetingClass;

        public BattleTargetingProcessor()
        {
            Init();
        }

        private void Init() 
        {
            _targeting.Clear();

            var assembly = Assembly.GetAssembly(typeof(BattleTargeting));

            var allTargetingTypes = assembly.GetTypes()
                .Where(t => typeof(BattleTargeting)
                .IsAssignableFrom(t) && !t.IsAbstract);

            foreach(var targeting in allTargetingTypes) 
            {
                var targetingInstance = Activator.CreateInstance(targeting) as BattleTargeting;
                
                _targeting.Add(targetingInstance.TargetSearchType, targetingInstance);
            }
        }

        public void SetTargetingData(TargetSearchType targetSearchType,
            List<Character> targetPool,
            int maxTargetsToSelect) 
        {
            _currentTargetingClass = _targeting[targetSearchType];
            
            _currentTargetingClass.Init(targetPool, maxTargetsToSelect);
        }

        public List<Character> PreviewTargetList()
        {
            return _currentTargetingClass.PreviewTargetList();
        }

        public void PrepareTargets(int mainTargetIndex) 
        {
            _currentTargetingClass.PrepareTargets(mainTargetIndex);
        }

        public bool AddSelectedTargets(
            ref Stack<Character> currentTargets)
        {
            return _currentTargetingClass.AddSelectedTargets(ref currentTargets);
        }

        public void OnCancelAction(
            ref Stack<Character> currentTargets)
        {
            _currentTargetingClass.OnCancelAction(ref currentTargets);
        }
    }
}
