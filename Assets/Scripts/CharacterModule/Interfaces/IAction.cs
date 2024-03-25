using System.Collections.Generic;
using CharacterModule.Stats.Managers;
using CharacterModule.Stats.StatModifier;

namespace CharacterModule.Interfaces
{
    public interface IAction
    {
        public StatModifiers SourceModifiers { get; set; }

        public void ExecuteAction(StatManager source, List<Character> targets);
    }
}