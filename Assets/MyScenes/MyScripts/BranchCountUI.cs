using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchCountUI : MonoBehaviour
{
    [SerializeField]
    GameObject _Beaverinteractor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<TMPro.TextMeshProUGUI>().text = _Beaverinteractor.GetComponent<PlayerPickDrop>().Branches.Count.ToString();
    }
}
