using UnityEngine;

namespace BattleModule.Actions.BattleActions.Outcome.Information
{
    [CreateAssetMenu(fileName = "Outcome Information", menuName = "Battle/Action/Outcome Information")]
    public class OutcomeInformation : ScriptableObject
    {
        [field: SerializeField]
        public string OutcomeName { get; private set; }
    }
}