using System;
using CharacterModule;
using UnityEngine;

public class Player : Character
{
    [SerializeField] LayerMask InteractibleObjectMask;
    [SerializeField] LayerMask TargetObjectMask;
    [SerializeField] Transform BulletSpawnPosition;

    GameObject targetEnemy;

    public event Action<bool> UIInteractionPanelShow;

    private bool interactibleObjectsNearby;
    private Collider[] interactibleObjects;
    public Player() 
    {
        
    }
    private void Loot() 
    {
        Loot obtainedLoot = GetNearestObject(interactibleObjects).GetComponent<Loot>();

        Destroy(obtainedLoot.gameObject);
    }
    private void Update()
    {
        interactibleObjects = OverlapInteractibleObjects();
        interactibleObjectsNearby = interactibleObjects.Length > 0;

        targetEnemy = OpenWorld_TargetEnemy();
        OpenWorld_Attack();
        UIInteractionPanelShow?.Invoke(interactibleObjectsNearby);
        if (interactibleObjectsNearby && Input.GetKeyDown(KeyCode.F)) 
            Loot();

        if (Input.GetKeyDown(KeyCode.H))
            GameController.instance.ChangeScene(UtilityTemp.BATTLE_SCENE_NAME);
    }
    private GameObject GetNearestObject(Collider[] objects)
    {
        if (objects.Length == 0)
            return null;

        GameObject nearestObject = objects[0].gameObject;
        for (int i = 0; i < objects.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, objects[i].transform.position);
            for (int j = 0; j < objects.Length; j++) 
            {
                if (distance > Vector3.Distance(transform.position, objects[j].transform.position))
                    nearestObject = objects[j].gameObject;
            }
            
        }
        return nearestObject;
    }
    public Collider[] OverlapInteractibleObjects() 
    {
        Vector3 extents = new Vector3(6, 1, 6);
        return Physics.OverlapBox(transform.position, extents, Quaternion.identity, InteractibleObjectMask);
    }

    private GameObject OpenWorld_TargetEnemy()
    {
        Vector3 extents = new Vector3(10, 1, 10);
        return GetNearestObject(Physics.OverlapBox(transform.position, extents, Quaternion.identity, TargetObjectMask));
    }

    private void OpenWorld_Attack() 
    {
        if (targetEnemy != null && Input.GetKeyDown(KeyCode.Return))
        {
            transform.LookAt(targetEnemy.transform);
            transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);
        }
    }
}
