using InventorySystem.Core;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot : MonoBehaviour
{ 
    [SerializeField] public List<InventoryItem> lootToObtain = new List<InventoryItem>();
}
