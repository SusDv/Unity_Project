using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BattleModule.Controllers.Targeting.Base;
using BattleModule.Utility.Enums;
using CharacterModule;

namespace BattleModule.Controllers.Targeting.Processor
{
    public class BattleTargetingProcessor
    {
        private readonly Dictionary<TargetSearchType, BattleTargeting> _targeting = new();
        
        private BattleTargeting _currentTargetingClass;

        private int _maxTargetsToSelect;

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

        public void SetTargetingData(TargetSearchType targetSearchType, int maxTargetsToSelect) 
        {
            _currentTargetingClass = _targeting[targetSearchType];
            _maxTargetsToSelect = maxTargetsToSelect;
        } 

        public IEnumerable<Character> GetSelectedTargets(
            List<Character> characters,
            Character mainTarget) 
        {
            return _currentTargetingClass.GetSelectedTargets(
                characters, mainTarget, _maxTargetsToSelect);
        }

        public bool AddSelectedTargets(
            ref Stack<Character> currentTargets)
        {
            return _currentTargetingClass.AddSelectedTargets(ref currentTargets, _maxTargetsToSelect);
        }

        public void OnCancelAction(
            ref Stack<Character> currentTargets)
        {
            _currentTargetingClass.OnCancelAction(ref currentTargets);
        }
    }
}
