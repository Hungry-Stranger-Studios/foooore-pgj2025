using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class LevelListItemController
{
    Label m_NameLabel;


    public void SetVisualElement(VisualElement visualElement)
    {
        m_NameLabel = visualElement.Q<Label>("level-name");
    }


    public void SetLevelData(LevelData levelData)
    {
        m_NameLabel.text = levelData.levelName;
    }
}
