using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelSelectController
{
    //UXML Asset for level entries
    VisualTreeAsset m_LevelEntryTemp;

    //UI Element References
    ListView m_LevelList;
    Label m_LevelLabel;
    VisualElement m_LevelImage;

    List<LevelData> m_AllLevels;

    public void InitializeLevelList(VisualElement root, VisualTreeAsset listElementTemplate)
    {
        EnumerateAllLevels();

        m_LevelEntryTemp = listElementTemplate;

        m_LevelList = root.Q<ListView>("level-list");

        m_LevelLabel = root.Q<Label>("level-name");
        m_LevelImage = root.Q<Image>("level-image");

        FillLevelList();

        m_LevelList.selectionChanged += OnLevelSelected;
    }

    public void EnumerateAllLevels()
    {
        m_AllLevels = new List<LevelData>();
        m_AllLevels.AddRange(Resources.LoadAll<LevelData>("Levels"));
    }

    public void FillLevelList()
    {
        m_LevelList.makeItem = () =>
        {
            // Instantiate the UXML template for the entry
            var newListEntry = m_LevelEntryTemp.Instantiate();
            // Instantiate a controller for the data
            var newListEntryLogic = new LevelListItemController();
            // Assign the controller script to the visual element
            newListEntry.userData = newListEntryLogic;
            // Initialize the controller script
            newListEntryLogic.SetVisualElement(newListEntry);
            // Return the root of the instantiated visual tree
            return newListEntry;
        };

        // Set up bind function for a specific list entry
        m_LevelList.bindItem = (item, index) =>
        {
            (item.userData as LevelListItemController)?.SetLevelData(m_AllLevels[index]);
        };

        // Set a fixed item height matching the height of the item provided in makeItem. 
        // For dynamic height, see the virtualizationMethod property.
        m_LevelList.fixedItemHeight = 45;

        // Set the actual item's source list/array
        m_LevelList.itemsSource = m_AllLevels;
    }

    private void OnLevelSelected(IEnumerable<object> selectedItem)
    {

    }
}
