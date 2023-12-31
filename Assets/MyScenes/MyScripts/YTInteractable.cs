using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface YTInteractable
{
    public string InteractionPrompt { get; }
    public YTInteractionPromptUI PromptUI { get; }
    public bool Interact(YTInteractor interactor);

}
