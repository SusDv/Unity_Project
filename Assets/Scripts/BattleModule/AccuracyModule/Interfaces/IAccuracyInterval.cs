using BattleModule.AccuracyModule.AccuracyRange.Intervals.Utility;
using BattleModule.Actions.BattleActions.Outcome;

namespace BattleModule.AccuracyModule.Interfaces
{
    public interface IAccuracyInterval
    {
        public IntervalType IntervalType { get; }

        public IntervalRange IntervalRange { get; set; }

        public float IntervalPercentage { get; }

        public void SetIntervalPercentage(float intervalPercentage);

        public BattleActionOutcome GetBattleActionOutcome();
    }
}