using System.Collections.Generic;
using System.Linq;
using CharacterModule.CharacterType;
using CharacterModule.Inventory;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{
    private InventoryBase Inventory;

    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject InventoryContainer;

    [SerializeField] private GameObject InteractionPanel;
    [SerializeField] private GameObject InteractionPanelPrefab;

    [SerializeField] private GameObject CharacterSelectionPanel;

    [SerializeField] private UICharacterSelect UICharacterSelectionPrefab;


    
    private SwapCharacterController controller;

    void Start()
    {
        controller = FindObjectOfType<SwapCharacterController>();
        SetSelectionCharacterData();
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            if (InventoryPanel.gameObject.activeSelf == false)
            {
                SetInventoryData();
                Show();
            }
            else
                Hide();
        }
    }

    private void ShowInteractionPanel(bool active) 
    {
        InteractionPanel.SetActive(active);
        if (InteractionPanel.transform.childCount == 0)
            Instantiate(InteractionPanelPrefab, InteractionPanel.transform.position, Quaternion.identity, InteractionPanel.transform);
    }
    
    public void SetInventoryData()
    {
        DestroyChildren();
    }

    public void SetSelectionCharacterData() 
    {
        for (int i = 0; i < controller.PlayerTeamSize; i++) 
        {
            UICharacterSelect UIItem = Instantiate(UICharacterSelectionPrefab, CharacterSelectionPanel.transform.position,
                Quaternion.identity, CharacterSelectionPanel.transform);
            UIItem.SetData(controller.PlayerTeamRef[i].name, controller.PlayerTeamRef[i]);
        }
    }
    public void DestroyChildren() 
    {
        for (int i = 0; i < InventoryContainer.transform.childCount; i++) 
        {
            Destroy(InventoryContainer.transform.GetChild(i).gameObject);
        }
    }


    public void Show() => InventoryPanel.SetActive(true);

    public void Hide() => InventoryPanel.SetActive(false);
}
