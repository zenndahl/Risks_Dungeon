using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RiskGameDisplay : RiskDisplay
{
    public Image image;
    public Text probText;
    public Text impactText;
    public Text actionText;

    protected override void Start() 
    {
        if(risk != null) ShowRiskInfos();
    }

    public override void ShowDescription()
    {
        //if(describer != null) describer.SetActive(true);
        if(risk.reaction == 0) actionText.text = "Não Planejado Corretamente";
        if(risk.reaction == 1) actionText.text = "Reação: Mitigar";
        if(risk.reaction == 2) actionText.text = "Reação: Atribuir";
        if(risk.reaction == 3) actionText.text = "Reação: Aceitar";
        descriptionText.text = risk.riskDescription;
        image.sprite = risk.sprite;
    }

    public void ShowRiskInfos()
    {
        transform.position = new Vector3 (Screen.width * 0.5f, Screen.height * 0.5f, 0);
        Menus.DisableInteractbles();
        if(risk != null) nameText.text = risk.id + " - " + risk.riskName;
        impactText.text = risk.impactLevel.ToString();
        probText.text = (risk.probability * 100) + "%";
        ShowDescription();
    }

    public void DismissRiskDisplay()
    {
        transform.position = new Vector3 (Screen.width * 2f, Screen.height * 2f, 0);
        Menus.EnableInteractbles();
    }
}
