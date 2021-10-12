using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OpportunityDisplay : MonoBehaviour
{
    public Opportunity opportunity;
    public GameObject baseUI;
    public GameObject uiAuxiliar;
    public GameObject skillUI;
    public Text opportunityName;
    public Text descriptionText;
    public Text effectText;
    public Text bonusText;
    public Text costText;

    public void ShowDescription()
    {
        transform.position = new Vector3 ((Screen.width * 0.5f)-115, Screen.height * 0.5f, 0);
        Menus.DisableInteractbles();
        if(opportunityName != null) opportunityName.text = opportunity.opportunityName;
        if(descriptionText != null) descriptionText.text = opportunity.opportunityDescription;
        if(effectText != null) effectText.text = opportunity.opportunityEffect;

        ShowValues();
    }

    void ShowValues()
    {
        //check wich resource will be bonus and wich will be cost and show them apropriately
        for(int i = 0; i < 3; i++)
        {
            //check if the scope will be bonus
            if(opportunity.scopeBonus > 0)
            {
                bonusText.text = "Escopo: +" +opportunity.scopeBonus+ " "; 
            }
            else if(opportunity.scopeBonus < 0) //if it is not a bonus, it is a cost
            {
                costText.text = "Escopo: " +opportunity.scopeBonus+ " "; 
            }

            //same goes to the others

            if(opportunity.moneyBonus > 0)
            {
                bonusText.text += "Orçamento: +" +opportunity.moneyBonus+ " "; 
            }
            else if(opportunity.moneyBonus < 0)
            {
                costText.text = "Orçamento: " +opportunity.moneyBonus+ " "; 
            }

            if(opportunity.timeBonus > 0)
            {
                bonusText.text += "Cronograma: +" +opportunity.timeBonus; 
            }
            else if(opportunity.timeBonus < 0)
            {
                costText.text = "Cronograma: " +opportunity.timeBonus+ " "; 
            }
        }
    }

    public void HideAuxiliars()
    {
        uiAuxiliar.SetActive(false);
        skillUI.SetActive(false);
    }

    public void TakeOpportunity()
    {
        opportunity.ActivateOpportunity();
        //these 2 opportunities use aditional UI, so if it is one of them, the display is not disabled yet
        if(opportunity.addSkill)
        {
            GameObject.Find("BaseUI").SetActive(false);
        }
        if(!opportunity.diversify && !opportunity.addSkill) ResetDisplay();
    }

    public void ResetDisplay()
    {
        baseUI.SetActive(true); 
        transform.position = new Vector3 (Screen.width * 2f, Screen.height * 2f, 0);
        Menus.EnableInteractbles();
        if(!opportunity.repeatable) GameManager.opportunities.Remove(opportunity);
    }
    
    //methods for the opportunity "Diversificar ..."
    public void DrawNewOpportunity()
    {
        Room.DrawOpportunity();
    }

    public void DiversifyToScope()
    {
        Player.OperateScope(2);
    }

    public void DiversifyToTime()
    {
        Player.OperateTime(2);
    }

}
