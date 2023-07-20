using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIinfoPanel : MonoBehaviour
{
    [SerializeField]
    public TMPro.TextMeshProUGUI _name;
    [SerializeField]
    public TMPro.TextMeshProUGUI _syilx;
    [SerializeField]
    public TMPro.TextMeshProUGUI _latin;
    [SerializeField]
    public TMPro.TextMeshProUGUI _desc;

    public AudioClip _sound;

    // Start is called before the first frame update
    void Start()
    {
        //_name = GameObject.Find("Error").GetComponent<TMPro.TextMeshProUGUI>();
        //Debug.Log(this.name);
        this.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTExt(string name, string syilx, string latin, string desc)
    {
        this.gameObject.SetActive(true);
        _name.text = name;
        _syilx.text = syilx;    
        _latin.text = latin;    
        _desc.text = desc;  
    }

    public void SearchPlant(string name)
    {
        if(name== "CommonYarrow")
        {
            this.gameObject.SetActive(true);
            _name.text = name;
            _desc.text = "These beautiful plants are truly eye catching. They can stand over a meter in height and are crowned with pyramidal clusters of bright yellow flowers. They are found in a variety of open area habitats such as meadows, disturbed areas, forest edges and roadsides. The Okanagan people boiled the stems to make a tea to calm a baby’s fever and boiled the flowers into tea to combat diarrhea. Goldenrod has historical significance in other cultures as well. The Crusades were known to carry goldenrod into battle. It is also used as a natural mordant for dying fabric golden/yellow.";
            _latin.text = "Solidago Altissima";
            _syilx.text = "pw?pw?láqa?";

        }
        if(name== "GoldenRod")
        {
            this.gameObject.SetActive(true);
            _name.text = name;
            _syilx.text = "q??cq??c?wiy?a?húps";
            _latin.text = "Achillea Millefolium";
            _desc.text = "?cq??c?wiy?a?húps (yarrow) is abundant in the Okanagan and throughout North America. It can thrive in a variety of environments, both moist and dry, such as rocky slopes, open forests, meadows, clearings, gravel bars and roadsides. The yarrow plant is beneficial to the ecosystem by attracting pollinating insects and deterring pest species that may harm other plants and flowers. The Okanagan People burn the leaves and stems to keep mosquitos away. Yarrow tea was traditionally used and still is to this day for a multitude of medicinal benefits. Additionally, the leaves can be used on wounds and nosebleeds to reduce bleeding and help in healing.";
        }
    }
}
