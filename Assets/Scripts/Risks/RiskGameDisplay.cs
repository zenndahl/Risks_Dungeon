using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class RiskGameDisplay : RiskDisplay
{
    public Image image;
    public GameObject effectsDisplay;
    public Text probText;
    public Text impactText;
    public Text actionText;
    public Text skillsText;
    public Text planningMessage;
    public List<Skill> skillsActivated = new List<Skill>();

    protected override void Start() 
    {
        if(risk != null) ShowRiskInfos();
    }

    public override void ShowDescription()
    {
        foreach (Skill skill in skillsActivated)
        {
            skillsText.text += skill.skillName + ", ";
        }
        
        if(risk.reaction == 0){
            actionText.text = "Não Planejado Corretamente";
            planningMessage.text = "Este risco não foi planejado corretamente!";
        }
        if(risk.reaction == 1) 
        {
            actionText.text = "Reação: Mitigar";
            planningMessage.text = "Este risco foi mitigado com sucesso. \n Impacto reduzido!";
        }
        if(risk.reaction == 2) 
        {
            actionText.text = "Reação: Atribuir";
            planningMessage.text = "Risco atribuido a terceiros. \n Impacto apenas no orçamento!";
        }
        if(risk.reaction == 3)
        {
            actionText.text = "Reação: Aceitar";
            planningMessage.text = "O impacto deste risco foi aceito!";
        }

        descriptionText.text = risk.riskDescription;
        image.sprite = risk.sprite;
    }

    public void ShowRiskInfos()
    {
        transform.position = new Vector3 (Screen.width * 0.5f, Screen.height * 0.5f, 0);
        Menus.DisableInteractbles();
        if(risk != null) nameText.text = risk.id + " - " + risk.riskName;
        impactText.text = risk.impactLevel.ToString();
        probText.text = (((float)System.Math.Round(risk.probability * 100f) / 100f) * 100) + "%";
        ShowDescription();
    }

    public void DismissEffectsDisplay()
    {
        effectsDisplay.SetActive(false);
        skillsText.text = "";
        planningMessage.text = "";
    }

    public void DismissRiskDisplay()
    {
        transform.position = new Vector3 (Screen.width * 2f, Screen.height * 2f, 0);
        effectsDisplay.SetActive(true);
        Menus.EnableInteractbles();
        GameObject.Find("Beholder").GetComponent<TeamAgent>().CalculateTeamMorale();
    }
}
