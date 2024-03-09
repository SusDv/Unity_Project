using System.Collections.Generic;
using CharacterModule;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public List<Character> PlayerTeam;
    public List<Character> EnenmyTeam;
    
    public Character PlayableCharacter { get; set; }

    public Transform PlayableCharacter_Transform { get; set; }


    private void Awake()
    {
        InitializeSingleton();
    }

    private void InitializeSingleton() 
    {
        if (instance != null) 
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeScene(string sceneName) 
    {
        switch (sceneName) 
        {
            case UtilityTemp.BATTLE_SCENE_NAME:
                PlayableCharacter_Transform = PlayableCharacter.transform;
                break;
            case UtilityTemp.OPEN_WORLD_SCENE_NAME:
                break;
        }
        SceneManager.LoadScene(sceneName);
    }  
}
