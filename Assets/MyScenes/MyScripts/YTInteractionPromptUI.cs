using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YTInteractionPromptUI : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private GameObject _UIPanel;
    [SerializeField] private TMPro.TextMeshProUGUI _promptText;
    

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _UIPanel.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var rotation = _mainCamera.transform.rotation;
        transform.LookAt(transform.position+rotation*Vector3.forward, rotation*Vector3.up );
    }

    public bool IsDisplayed = false;
    public void SetUp(string promptText)
    {
        _promptText.text = promptText;
        IsDisplayed = true;
        _UIPanel.SetActive(true);
    }

    public void Close()
    {
        if(IsDisplayed != false) { _UIPanel.SetActive(false); IsDisplayed = false; }
    }

    public void SetEn()
    {
        IsDisplayed = true;
        _UIPanel.SetActive(true);
    }
}
