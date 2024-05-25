using UnityEngine;

namespace CharacterModule.Settings
{
    [CreateAssetMenu(fileName = "Character Information", menuName = "Character/Settings/Character Information")]
    public class BaseInformation : ScriptableObject
    {
        [field: SerializeField] public string CharacterName { get; private set; }
        
        [field: SerializeField] public string CharacterDescription{ get; private set; }
        [field: SerializeField] public Sprite CharacterImage{ get; private set; }
    }
}