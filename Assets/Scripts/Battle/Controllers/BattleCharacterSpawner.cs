using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacterSpawner : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] private Transform _playerCharactersSpawnPoint;
    [SerializeField] private Transform _enemyCharactersSpawnPoint;

    [Header("Spawn Config")]
    [SerializeField] [Range(3, 5)] 
    private int _maximumEnemiesAllowedOnScreen = 3;

    [SerializeField] [Range(5f, 15f)] 
    private float _defaultCharacterLineLength = 5f;

    private float _characterSeperatorLength;

    private List<Character> _spawnedCharacters;

    private void Awake()
    {
        _characterSeperatorLength = (_defaultCharacterLineLength / (_maximumEnemiesAllowedOnScreen - 1));
    }

    public List<Character> SpawnCharacters(List<Character> charactersToSpawn) 
    {
        var characterInfo = GetSpawnedCharacterInfo(charactersToSpawn[0]);

        _spawnedCharacters = new List<Character>();

        for (int i = 0; i < charactersToSpawn.Count; i++) 
        {
            Vector3 spawnPosition = new Vector3(characterInfo.spawnPoint.localPosition.x - _characterSeperatorLength + (_characterSeperatorLength * i), characterInfo.spawnPoint.localPosition.y, characterInfo.spawnPoint.position.z);

            Character character = Instantiate(charactersToSpawn[i], spawnPosition, Quaternion.identity);

            _spawnedCharacters.Add(character);
        }

        return _spawnedCharacters;
    }
    private (bool isEnemy, Transform spawnPoint) GetSpawnedCharacterInfo(Character characterToSpawn) 
    {
        bool isEnemy = characterToSpawn is Enemy;

        return (isEnemy, isEnemy ? _enemyCharactersSpawnPoint : _playerCharactersSpawnPoint);
    }

}
