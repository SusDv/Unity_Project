using CharacterModule;
using CharacterModule.Stats.Utility.Enums;

namespace BattleModule.Controllers.Turn
{
    public class BattleCharacterInTurn
    {
        public Character AssociatedCharacter { get; set; }

        public float BattlePoints { get; set; }

        private BattleCharacterInTurn(Character associatedCharacter)
        {
            AssociatedCharacter = associatedCharacter;

            BattlePoints = associatedCharacter.CharacterStats.GetStatInfo(StatType.BATTLE_POINTS).FinalValue;
        }

        public static BattleCharacterInTurn CreateInstance(
            Character associatedCharacter)
        {
            return new BattleCharacterInTurn(associatedCharacter);
        }
    }
}