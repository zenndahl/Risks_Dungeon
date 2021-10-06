using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OpportunityDisplay : MonoBehaviour
{
    public Opportunity opportunity;
    public GameObject uiAuxiliar;
    public GameObject skillUI;
    public Text opportunityName;
    public Text descriptionText;
    public Text effectText;
    public Text bonusText;
    public Text costText;

    void Start()
    {
        
    }

    public void ShowDescription()
    {
        transform.position = new Vector3 (Screen.width * 0.5f, Screen.height * 0.5f, 0);
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
            if(opportunity.scopeBonus >= 0)
            {
                bonusText.text = "Escopo: +" +opportunity.scopeBonus+ "\n"; 
            }
            else //if it is not a bonus, it is a cost
            {
                costText.text = "Escopo: " +opportunity.scopeBonus+ "\n"; 
            }

            //same goes to the others

            if(opportunity.moneyBonus >= 0)
            {
                bonusText.text += "Orçamento: +" +opportunity.moneyBonus+ "\n"; 
            }
            else
            {
                costText.text = "Orçamento: " +opportunity.moneyBonus+ "\n"; 
            }

            if(opportunity.timeBonus >= 0)
            {
                bonusText.text += "Cronograma: +" +opportunity.timeBonus; 
            }
            else
            {
                costText.text = "Cronograma: " +opportunity.timeBonus+ "\n"; 
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
        ResetDisplay();
    }

    public void ResetDisplay()
    {
        transform.position = new Vector3 (Screen.width * 2f, Screen.height * 2f, 0);
        Menus.EnableInteractbles();
        if(!opportunity.repeatable) GameManager.opportunities.Remove(opportunity);
    }
    
    public void DrawNewOpportunity()
    {
        Room.DrawOpportunity();
    }

    public void DiversifyToScope()
    {
        GameObject.Find("Player").GetComponent<Player>().OperateScope(2);
    }

    public void DiversifyToTime()
    {
        GameObject.Find("Player").GetComponent<Player>().OperateTime(2);
    }

}
