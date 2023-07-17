using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropField : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _targetText;
    [SerializeField] private TMPro.TextMeshProUGUI _inText;
    [SerializeField] public YTInteractionPromptUI _UIdir;

    public List<GameObject> CollectedBranches = new List<GameObject>(); // keep track of collected branches

    public int _targetAmt=10;
    public int _inAmt =0;

    // Start is called before the first frame update
    void Start()
    {
        _targetText.text = _targetAmt.ToString();
        _inText.text = _inAmt.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addItem(int Added)
    {
        _inAmt += Added;
        _inText.text = _inAmt.ToString();
    }
}
