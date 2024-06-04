using CharacterModule.Utility;
using CharacterModule.Utility.Stats;

namespace Utility.ObserverPattern
{
    public interface IStatObserver : IObserver<StatInfo>
    {
        public StatType StatType { get; set; }
    }

    public interface IObserver
    {
        public void UpdateValue(float value, bool negativeChange);
    }

    public interface IObserver<in T>
    {
        public void UpdateValue(T value, bool negativeChange);
    }
}