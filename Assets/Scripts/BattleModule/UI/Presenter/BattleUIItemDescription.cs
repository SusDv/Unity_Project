using BattleModule.UI.Presenter.SceneSettings.Inventory;
using CharacterModule.Inventory.Items.Base;
using VContainer;

namespace BattleModule.UI.Presenter
{
    public class BattleUIItemDescription
    {
        private readonly BattleItemDescriptionSceneSettings _battleItemDescriptionSceneSettings;
        
        [Inject]
        public BattleUIItemDescription(
            BattleItemDescriptionSceneSettings battleItemDescriptionSceneSettings)
        {
            _battleItemDescriptionSceneSettings = battleItemDescriptionSceneSettings;
        }

        public void SetDescriptionPanelVisibility(bool inventoryVisibilityStatus)
        {
            _battleItemDescriptionSceneSettings.BattleItemDescriptionWindow.SetActive(inventoryVisibilityStatus && _battleItemDescriptionSceneSettings.BattleItemDescriptionWindow.activeSelf);
        }

        public void SetItemDescription(ItemBase itemBase)
        {
            _battleItemDescriptionSceneSettings.BattleItemDescriptionWindow.SetActive(!_battleItemDescriptionSceneSettings.BattleItemDescriptionWindow.activeSelf);
            
            if (_battleItemDescriptionSceneSettings.BattleItemDescriptionWindow.activeSelf)
            {
                _battleItemDescriptionSceneSettings.BattleUIItemDescriptionView.SetData(itemBase.ObjectInformation);
            }
        }
    }
}