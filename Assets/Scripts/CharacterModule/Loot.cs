using System.Collections.Generic;
using CharacterModule.Inventory;
using UnityEngine;

[System.Serializable]
public class Loot : MonoBehaviour
{ 
    [SerializeField] public List<InventoryItem> lootToObtain = new List<InventoryItem>();
}
