using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RiskDisplay : MonoBehaviour
{
    [Header("Compartilhadas")]
    public Risk risk;
    public int impact;
    public int prob;
    public Text nameText;
    public Text descriptionText;
    public GameObject describer;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        SetInfos();
    }

    public virtual void ShowDescription()
    {
        //if(describer != null) GameObject.Find("Describer").SetAsLastSibling();
        if(GameObject.Find("Describer/Text")) GameObject.Find("Describer/Text").GetComponent<Text>().text = risk.riskDescription;
    }

    public void SetEvents(GameObject rskDisplay)
    {
        Button btn = rskDisplay.transform.GetChild(0).GetComponent<Button>();
        btn.onClick.AddListener(Select);
    }

    public void SetInfos()
    {
        if(risk != null) nameText.text = risk.id + " - " + risk.riskName;
    }

    public void Select()
    {
        Add.selected = this.gameObject;
    }
}
