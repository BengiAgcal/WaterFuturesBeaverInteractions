using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour, YTInteractable
{
    [SerializeField] private string _prompt;

    [SerializeField] private YTInteractionPromptUI _promptUI;
    public string InteractionPrompt => _prompt;

    public YTInteractionPromptUI PromptUI => _promptUI;

    public bool Interact(YTInteractor interactor)
    {
        interactor.gameObject.GetComponent<PlayerPickDrop>().TryToPick(this.gameObject.GetComponent<Collider>());
        Debug.Log("Picking");
        return true;
    }
}
