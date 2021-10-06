using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RiskDisplay : MonoBehaviour
{
    public Risk risk;

    public int impact;
    public int prob;
    public Image image;
    public Text nameText;
    public Text descriptionText;
    public GameObject describer;
    public Text probText;
    public Text impactText;

    // Start is called before the first frame update
    void Start()
    {
        if(risk != null) nameText.text = risk.riskName;
    }

    public void ShowDescription()
    {
        if(describer != null) describer.SetActive(true);
        if(descriptionText != null) descriptionText.text = risk.riskDescription;
        if(image != null) image.sprite = risk.sprite;
    }

    public void ShowRiskInfos()
    {
        transform.position = new Vector3 (Screen.width * 0.5f, Screen.height * 0.5f, 0);
        Menus.DisableInteractbles();
        impactText.text += "\n" + risk.impactLevel;
        probText.text += "\n" + risk.probability;
    }

    public void DismissRiskDisplay()
    {
        transform.position = new Vector3 (Screen.width * 2f, Screen.height * 2f, 0);
        Menus.EnableInteractbles();
    }

    public void SetEvents(GameObject rskDisplay)
    {
        Button btn = rskDisplay.transform.GetChild(0).GetComponent<Button>();
        btn.onClick.AddListener(Select);
    }

    public void SetInfos()
    {
        if(risk != null) nameText.text = risk.riskName;
    }

    public void Select()
    {
        Add.selected = this.gameObject;
    }

    public void SelectMatrixCell()
    {
        Add.matrixCell = this.gameObject;
    }

    public void DisplayInMatrix(Risk rsk)
    {
        if(nameText.text == "") nameText.text += rsk.id.ToString();
        else nameText.text += ", " + rsk.id.ToString();
    }
}
