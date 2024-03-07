using UnityEngine;

namespace CharacterModule.Data.Info
{
    [CreateAssetMenu(fileName = "Character Information", menuName = "Character/Settings/Character Information")]
    public class CharacterInformation : ScriptableObject
    {
        [field: SerializeField] public string CharacterName { get; private set; }
        
        [field: SerializeField] public string CharacterDescription{ get; private set; }
        [field: SerializeField] public Sprite CharacterImage{ get; private set; }
    }
}