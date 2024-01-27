namespace BattleModule.ActionCore.Interfaces
{
    public interface IBattleAction
    {
        public void SetBattleAction<T>(object actionObject)
            where T : BattleAction;

        public void ResetBattleAction();
    }
}
