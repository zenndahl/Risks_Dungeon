using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RiskDisplay : MonoBehaviour
{
    public Risk risk;

    public int impact;
    public int prob;
    public Sprite defaultSprite;
    public Sprite selectedSprite;
    public Text nameText;
    public Text descriptionText;
    public GameObject describer;

    // Start is called before the first frame update
    void Start()
    {
        if(risk != null) nameText.text = risk.riskName;
    }

    public void ShowDescription(){
        describer.SetActive(true);
        descriptionText.text = risk.riskDescription;
    }

    public void SetEvents(GameObject rskDisplay)
    {
        Button btn = rskDisplay.transform.GetChild(0).GetComponent<Button>();
        btn.onClick.AddListener(Select);
    }

    public void Select()
    {
        Add.selected = this.gameObject;
        //gameObject.transform.GetChild(0).GetComponent<Image>().sprite = selectedSprite;
    }

    public void Deselect()
    {
        Add.selected = null;
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = defaultSprite;
    }

    public void SelectMatrixCell()
    {
        Add.matrixCell = this.gameObject;
        Debug.Log("Selected: " + this.gameObject);
    }

    public void DisplayInMatrix(Risk rsk)
    {
        if(nameText.text == "") nameText.text += rsk.id.ToString();
        else nameText.text += ", " + rsk.id.ToString();
    }
}
