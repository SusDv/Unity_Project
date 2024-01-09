namespace BattleModule.ActionCore
{
    public abstract class BattleAction
    {
        protected object ActionObject;

        public void SetupAction(object actionObject) 
        {
            ActionObject = actionObject;
        }


        public abstract void PerformAction(Character source, Character target);
    }
}
