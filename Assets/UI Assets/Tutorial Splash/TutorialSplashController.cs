using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialSplashController : MonoBehaviour
{
    private UIDocument _tutorialTree;
    [SerializeField] private MouseControlledCamera mcc;
    private void OnEnable()
    {
        _tutorialTree = GetComponent<UIDocument>();
        var root = _tutorialTree.rootVisualElement;
        root.Q<Button>().clicked += OnButtonClick;
        mcc = GameObject.Find("Main Camera").GetComponent<MouseControlledCamera>();
    }
    private void Start()
    {
        mcc.pauseCamera();
    }

    private void OnDestroy()
    {
        mcc.unpauseCamera();
    }

    private void OnButtonClick()
    {
        Debug.Log("hiya");
        Destroy(this.gameObject);
    }
}
