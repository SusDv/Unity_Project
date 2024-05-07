using System.Collections.Generic;

namespace BattleModule.AccuracyModule.Transformer
{
    public interface IOutcomeTransformer
    {
        public List<OutcomeTransformer> GetTransformers();
    }
}