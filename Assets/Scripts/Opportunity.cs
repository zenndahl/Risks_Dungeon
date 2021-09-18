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

    public void DeacreaseCosts()
    {

    }

    public void IncreaseBonus()
    {

    }
}
