using BattleModule.Actions.Interfaces;
using CharacterModule.Types.Base;
using Utility.Information;

namespace BattleModule.Actions.Context
{
    public class BattleActionContext
    {
        public BattleActionContext(object actionObject, 
            Character characterInAction)
        {
            CharacterInAction = characterInAction;
            
            ObjectInformation = (actionObject as IObjectInformation)?.ObjectInformation;

            BattleObject = actionObject as IBattleObject;
        }

        public Character CharacterInAction { get; }

        public ObjectInformation ObjectInformation { get; }
        
        public IBattleObject BattleObject { get; }
    }
}
