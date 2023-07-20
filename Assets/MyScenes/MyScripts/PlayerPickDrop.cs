using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MalbersAnimations.Scriptables;


public class PlayerPickDrop : MonoBehaviour
{
    [SerializeField] 
    GameObject pivot;

    [SerializeField]
    UnityEvent m_MyEvent = new UnityEvent(); //Run the eat animation

    [SerializeField]
    UnityEvent m_MyEvent2 = new UnityEvent(); //Run the pick animation
    

    GameObject _errorDisplay;

    bool _inDropArea = false;

    GameObject _dropArea= null;


    public List<GameObject> Branches = new List<GameObject>();


    // This Script is kinda the game manager, managews the beaver picking, dropping and eating as well as the UI for interactions and Error Display
    void Start()
    {
        _errorDisplay = GameObject.Find("Error");
    }

    // Update is called once per frame
    private void Update() // check if Y is pressed to drop obkects, pick up is handled by YTinteractor
    {
       
        if (Branches.Count > 0 && Input.GetKeyUp(KeyCode.Y) && !_inDropArea) // Perform Regular Drop if beaver not in the Drop Area, just disables physics
        {
            Drop();
        }

        if (Branches.Count > 0 && Input.GetKeyUp(KeyCode.Y) && _inDropArea && _dropArea!= null) // Perform Special Drop if in the Drop Area
        {
            for (int i = 0; i < Branches.Count; i++) // for each branch in the list
            {
                //Branches[i].tag = "Untagged";
                Branches[i].GetComponent<Stick>().enabled = false; // can no longer pick up the item

                _dropArea.GetComponent<DropField>().CollectedBranches.Add(Branches[i]); // move branches to collected branches (Can do Fade Anmiations etc)
            }
            _dropArea.GetComponent<DropField>().addItem(Branches.Count);
            Drop();

        }

    }

    private void OnTriggerEnter(Collider other) // check if in  drop area or need to show UI
    {
        if(other.tag=="DropArea")
        {
            _inDropArea = true;
            _dropArea = other.gameObject;
            _dropArea.GetComponent<DropField>()._UIdir.SetEn();
        }

        if (other.tag == "Branch" || other.tag == "Food" || other.tag == "Plant")
        {
            YTInteractable _interactableUI = other.gameObject.GetComponent<YTInteractable>();
            if( _interactableUI != null && !_inDropArea)
            {
                _interactableUI.PromptUI.Show();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DropArea")
        {
            _inDropArea=false;
            _dropArea.GetComponent<DropField>()._UIdir.Close();
            _dropArea = null;
            
        }

        if (other.tag == "Branch" || other.tag == "Food" || other.tag == "Plant")
        {
            YTInteractable _interactableUI = other.gameObject.GetComponent<YTInteractable>();
            if (_interactableUI != null)
            {
                _interactableUI.PromptUI.Hide();    
            }

        }

    }



    public void TryToPick(Collider other) //Eats if Food, Picks if Item
    {

        if (other.gameObject.tag == "Branch" && !_inDropArea)
        {
            
            if (!Branches.Contains(other.gameObject) && Branches.Count <= 9)
            {

                Branches.Add(other.gameObject);
                m_MyEvent2.Invoke(); // make the move
                StartCoroutine(DelayedFunction(other, 0.5f)); // tbd
                
            }

            Debug.Log(other.gameObject.name);
            Debug.Log(Branches.Count);
        }

        if(other.gameObject.tag== "Food")
        {
            if (Branches.Count == 0) // not holding any object can eat
            {
                
                StartCoroutine(DelayedFunction(other, 0.5f)); // tbd
                m_MyEvent.Invoke();
                DestroyFood(other);
            }
            else
            {
                _errorDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "You cannot eat while holding items! Press Y to Drop.";
                _errorDisplay.GetComponent<Fadeable>().Toggle(0.7f, 0.7f);
            }
        }
    }

    IEnumerator DelayedFunction(Collider other, float delay) // Coroutine implemented to be able to delay the pick action
    {
        yield return new WaitForSeconds(delay);

        // Call the function with the parameters
        PickUp(other);
    }

    public void PickUp(Collider other)  // picks up Item by makiing it a child of the pivot
    {
        Vector3 nextpos = -pivot.transform.localPosition;

        //Disable Physics
        var Rigid = other.gameObject.GetComponent<Rigidbody>();
        RemPhysics(Rigid);

        var localScale = other.gameObject.transform.localScale;
        other.gameObject.transform.parent = pivot.gameObject.transform;  //Parent it to the Holder
        other.gameObject.transform.localPosition = Vector3.zero;     //Offset the Position
        //other.gameObject.transform.localEulerAngles = pivot.transform.localEulerAngles;    //Offset the Rotation

        other.gameObject.transform.localScale = localScale;

    }

    
    public void Drop()  // Drops items in the List
    {
        for (int i = 0; i < Branches.Count; i++)
        {
            Dropping(Branches[i]);
        }
        
        Branches.Clear();
    }

    public void Dropping(GameObject _branch) // Enables Physics to perform fall action and makes the parent null
    {
        EnPhysics(_branch.GetComponent<Rigidbody>());
        _branch.transform.parent = null;
    }
    
    void RemPhysics( Rigidbody Rigid) //removes physics
    {
        Rigid.useGravity = false;
        Rigid.velocity = Vector3.zero;
        Rigid.collisionDetectionMode = CollisionDetectionMode.Discrete;
        Rigid.isKinematic = true;
        Rigid.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    void EnPhysics (Rigidbody Rigid) // enavbles physics
    {
        Rigid.useGravity = true;
        Rigid.isKinematic = false;
        Rigid.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        Rigid.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    void DestroyFood(Collider other) // destrot object when eaten
    {
        Destroy(other.gameObject,3);
       
    }
}


