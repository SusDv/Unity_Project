using BattleModule.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BattleModule.Controllers.Targeting
{
    public static class BattleTargetingProcessor
    {
        private static Dictionary<TargetSearchType, BattleTargeting> _targeting = new Dictionary<TargetSearchType, BattleTargeting>();
        
        private static bool _initialized;

        public static void Init() 
        {
            _targeting.Clear();

            var assembly = Assembly.GetAssembly(typeof(BattleTargeting));

            var allTargetingTypes = assembly.GetTypes()
                .Where(t => typeof(BattleTargeting)
                .IsAssignableFrom(t) && !t.IsAbstract);

            foreach(var targeting in allTargetingTypes) 
            {
                BattleTargeting targetingInstance = Activator.CreateInstance(targeting) as BattleTargeting;
                _targeting.Add(targetingInstance.TargetSearchType, targetingInstance);
            }

            _initialized = true;
        }

        public static void GetSelectedTargets(
            TargetSearchType targetSearchType,
            List<Character> characters,
            Character mainTarget,
            int numberOfCharactersToSelect) 
        {
            if (!_initialized) 
            {
                Init();
            }

            var targetingClass = _targeting[targetSearchType];

            targetingClass.GetSelectedTargets(
                characters, mainTarget, numberOfCharactersToSelect);
        }

        public static bool AddSelectedTargets(
            TargetSearchType targetSearchType,
            ref Stack<Character> currentTargets)
        {
            if (!_initialized)
            {
                Init();
            }

            var targetingClass = _targeting[targetSearchType];

            return targetingClass.AddSelectedTargets(ref currentTargets);
        }

        public static void OnCancelAction(
            TargetSearchType targetSearchType,
            ref Stack<Character> currentTargets)
        {
            if (!_initialized)
            {
                Init();
            }

            var targetingClass = _targeting[targetSearchType];

            targetingClass.OnCancelAction(ref currentTargets);
        }
    }
}
