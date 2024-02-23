namespace StatModule.Utility
{
    public class StatInfo
    {
        private StatInfo(float baseValue, float finalValue, float maxValue)
        {
            BaseValue = baseValue;
            FinalValue = finalValue;
            MaxValue = maxValue;
        }

        public float BaseValue { get; private set; }

        public float FinalValue { get; private set; }

        public float MaxValue { get; private set; }


        public static StatInfo GetInstance(float baseValue, float finalValue, float maxValue)
        {
            return new StatInfo(baseValue, finalValue, maxValue);
        }
    }
}