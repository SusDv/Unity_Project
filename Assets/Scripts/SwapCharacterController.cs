using System.Collections.Generic;
using CharacterModule;
using UnityEngine;

public class SwapCharacterController : MonoBehaviour
{
    [SerializeField] private GameObject CharacterContainer;
    [SerializeField] private PlayerMovementController CharacterMovementController;

    private GameController _gameController;

    public int PlayerTeamSize { get { return _gameController.PlayerTeam.Count; } }

    public List<Character> PlayerTeamRef { get { return _gameController.PlayerTeam; } }

    private Character CharacterAtScene;
    private Character PreviousCharacter;


    private void Awake()
    {
        if (GameController.instance != null)
            _gameController = GameController.instance;

        SpawnCharacter(_gameController.PlayableCharacter);
    }
    public void SpawnCharacter(Character character)
    {
        if (CharacterAtScene == character && character != null)
            return;

        if (character == null)
            character = _gameController.PlayerTeam[0];

        PreviousCharacter = CharacterAtScene;  

        CharacterAtScene = Instantiate(character,
            CharacterContainer.transform.position,
            CharacterAtScene == null ?
            CharacterContainer.transform.rotation : CharacterAtScene.transform.rotation,
            CharacterContainer.transform);

        SetUpActiveCharacter();
        _gameController.PlayableCharacter = character;
    }

    private void SetUpActiveCharacter()
    {
        //CharacterMovementController.SetUpAvatar(CharacterAtScene.gameObject);
        if (PreviousCharacter != null)
        {
            Destroy(PreviousCharacter.gameObject);
        }
    }
}
