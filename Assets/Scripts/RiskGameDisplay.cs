using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RiskGameDisplay : RiskDisplay
{
    public Image image;
    public Text probText;
    public Text impactText;

    public override void ShowDescription()
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
        ShowDescription();
    }

    public void DismissRiskDisplay()
    {
        transform.position = new Vector3 (Screen.width * 2f, Screen.height * 2f, 0);
        Menus.EnableInteractbles();
    }
}
