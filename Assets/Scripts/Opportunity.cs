using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Opportunity", menuName = "Risks Dungeon/Opportunity", order = 0)]
public class Opportunity : ScriptableObject 
{
    [Header("Opportunity Infos")]
    public int id;
    public string opportunityName;
    //abstract of the effect
    public string opportunityEffect;
    //description of the opportunity
    public string opportunityDescription;
    //if the opportunity can happen again or is removed from the opportunities available
    public bool repeatable;

    [Header("Opportunity Bonuses")]
    public int scopeBonus;
    public int moneyBonus;
    public int timeBonus;

    [Header("Effect Infos")]
    public Prevention prevention;
    public bool makePrevention;
    public bool increasePower;
    public bool increaseBonus;
    public bool decreaseCosts;
    public bool addSkill;
    public bool newOpportunity;
    public bool diversify;

    public void DeacreaseCosts()
    {
        if(scopeBonus < 0 && scopeBonus+1 != 0) scopeBonus++;
        if(moneyBonus < 0 && moneyBonus+1 != 0) moneyBonus++;
        if(timeBonus < 0 && timeBonus+1 != 0) timeBonus++;
    }

    public void IncreaseBonus()
    {
        if(scopeBonus > 0) scopeBonus++;
        if(moneyBonus > 0) moneyBonus++;
        if(timeBonus > 0) timeBonus++;
    }

    public void ActivateOpportunity()
    {
        //apply resource bonus
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.opportunitiesTaken++;
        player.OperateScope(scopeBonus);
        player.OperateMoney(moneyBonus);
        player.OperateTime(timeBonus);

        //check ans apply the extra effects if has any
        if(makePrevention) prevention.Prevent();
        if(increasePower) GameObject.Find("Player").GetComponent<Player>().combatPower++;
        if(increaseBonus) IncreaseBonus();
        if(newOpportunity) Room.DrawOpportunity();
        if(diversify) GameObject.Find("Opportunity Display").GetComponent<OpportunityDisplay>().uiAuxiliar.SetActive(true);
        if(addSkill) GameObject.Find("Opportunity Display").GetComponent<OpportunityDisplay>().skillUI.SetActive(true);
        if(decreaseCosts)
        {
            foreach (Opportunity opp in GameManager.opportunities)
            {
                opp.DeacreaseCosts();
            }
        }
    }
}
