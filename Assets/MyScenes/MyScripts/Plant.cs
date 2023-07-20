using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour, YTInteractable
{
    [SerializeField] private string _prompt;

    [SerializeField] 
    private YTInteractionPromptUI _promptUI;


    public string InteractionPrompt => _prompt;

    public YTInteractionPromptUI PromptUI => _promptUI;

    //[SerializeField]
    GameObject _UIPanel;

    void Awake()
    {
        _UIPanel = GameObject.Find("InfoPanel");
        Debug.Log(this.name);
    }

    
    public bool Interact(YTInteractor interactor)
    {
       var panel = _UIPanel.GetComponent<UIinfoPanel>();
        if (panel != null)
        {
            //panel.SetTExt("hi", "hi", "hi", "hi");
            panel.SearchPlant(this.name);
        }
        Debug.Log("Picking");
        return true;
    }
}
