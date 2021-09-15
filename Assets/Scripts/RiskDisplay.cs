using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RiskDisplay : MonoBehaviour
{
    public Risk risk;

    public Text nameText;
    public Text descriptionText;
    public GameObject describer;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = risk.riskName;
    }

    public void ShowDescription(){
        describer.SetActive(true);
        descriptionText.text = risk.riskDescription;
    }

    public void SelectRisk(){
        Add.selected = this.gameObject;
    }
}
