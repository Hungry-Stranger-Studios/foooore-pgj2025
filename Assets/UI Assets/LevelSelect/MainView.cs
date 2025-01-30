using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainView : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset m_ListEntryTemplate;
    
    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        var levelSelectController = new LevelSelectController();
        levelSelectController.InitializeLevelList(uiDocument.rootVisualElement, m_ListEntryTemplate);
    }
}
