using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleModule.Actions.BattleActions.Transformer.Transformers
{
    [Serializable]
    public abstract class OutcomeTransformers
    {
        protected List<OutcomeTransformer> Transformers;

        public abstract List<OutcomeTransformer> GetTransformers();
    }

    [Serializable]
    public class StaticTransformers : OutcomeTransformers
    {
        [field: SerializeField]
        private List<StaticOutcomeTransformer> _staticOutcomeTransformers = new ();
        
        public override List<OutcomeTransformer> GetTransformers()
        {
            Transformers = new List<OutcomeTransformer>();
            
            Transformers.AddRange(_staticOutcomeTransformers);

            return Transformers;
        }
    }
    
    [Serializable]
    public class DynamicTransformers : OutcomeTransformers
    {
        [field: SerializeField]
        private List<TemporaryOutcomeTransformer> _temporaryOutcomeTransformers = new ();
        
        public override List<OutcomeTransformer> GetTransformers()
        {
            Transformers = new List<OutcomeTransformer>();

            Transformers.AddRange(_temporaryOutcomeTransformers);
            
            return Transformers;
        }
    }
    
    [Serializable]
    public class HybridTransformers : OutcomeTransformers
    {
        [field: SerializeField]
        private List<StaticOutcomeTransformer> _staticOutcomeTransformers = new ();

        [field: SerializeField]
        private List<TemporaryOutcomeTransformer> _temporaryOutcomeTransformers = new ();
        
        public override List<OutcomeTransformer> GetTransformers()
        {
            Transformers = new List<OutcomeTransformer>();

            Transformers.AddRange(_staticOutcomeTransformers);
            
            Transformers.AddRange(_temporaryOutcomeTransformers);
            
            return Transformers;
        }
    }
}