using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BattleModule.Controllers.Targeting.Base;
using BattleModule.Utility.Enums;
using UnityEngine.Assertions;

namespace BattleModule.Controllers.Targeting.Processor
{
    public static class BattleTargetingProcessor
    {
        private static readonly Dictionary<TargetSearchType, BattleTargeting> Targeting = new Dictionary<TargetSearchType, BattleTargeting>();
        
        private static bool _initialized;

        private static TargetSearchType _targetSearchType;

        private static void Init() 
        {
            Targeting.Clear();

            var assembly = Assembly.GetAssembly(typeof(BattleTargeting));

            var allTargetingTypes = assembly.GetTypes()
                .Where(t => typeof(BattleTargeting)
                .IsAssignableFrom(t) && !t.IsAbstract);

            foreach(var targeting in allTargetingTypes) 
            {
                BattleTargeting targetingInstance = Activator.CreateInstance(targeting) as BattleTargeting;
                
                Assert.IsNotNull(targetingInstance);
                
                Targeting.Add(targetingInstance.TargetSearchType, targetingInstance);
            }

            _initialized = true;
        }

        public static void SetCurrentSearchType(TargetSearchType targetSearchType) 
        {
            _targetSearchType = targetSearchType;
        } 

        public static void GetSelectedTargets(
            List<Character> characters,
            Character mainTarget,
            int numberOfCharactersToSelect) 
        {
            if (!_initialized) 
            {
                Init();
            }

            var targetingClass = Targeting[_targetSearchType];

            targetingClass.GetSelectedTargets(
                characters, mainTarget, numberOfCharactersToSelect);
        }

        public static bool AddSelectedTargets(
            ref Stack<Character> currentTargets)
        {
            if (!_initialized)
            {
                Init();
            }

            var targetingClass = Targeting[_targetSearchType];

            return targetingClass.AddSelectedTargets(ref currentTargets);
        }

        public static void OnCancelAction(
            ref Stack<Character> currentTargets)
        {
            if (!_initialized)
            {
                Init();
            }

            var targetingClass = Targeting[_targetSearchType];

            targetingClass.OnCancelAction(ref currentTargets);
        }
    }
}
