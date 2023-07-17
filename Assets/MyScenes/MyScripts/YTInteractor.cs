using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class YTInteractor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private YTInteractionPromptUI _interactionPromptUI;

    private YTInteractable _interactable;
    

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            _interactable = _colliders[0].gameObject.GetComponent<YTInteractable>();
            
            //Debug.Log(interactable);

            if (_interactable != null)
            {
                /*
                _interactionPromptUI = _interactable.PromptUI;
                // _interactable.Interact(this);
                if (!_interactionPromptUI.IsDisplayed)
                {
                    _interactionPromptUI.SetUp(_interactable.InteractionPrompt);
                }
                */

                if (Input.GetKeyDown(KeyCode.E))
                {
                   
                    _interactable.Interact(this);
                    _interactable.PromptUI.Hide();
                    
                }
            }
        }
        else
        {
           // if (_interactionPromptUI!=null && _interactionPromptUI.IsDisplayed) { _interactionPromptUI.Close(); _interactionPromptUI = null; }
            if (_interactable != null) { _interactable = null; }
            

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
