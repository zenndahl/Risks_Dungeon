using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Opportunity", menuName = "Risks Dungeon/Opportunity", order = 0)]
public class Opportunity : ScriptableObject 
{
    [Header("Opportunity Infos")]
    public int id;
    public string opportunityName;
    public string opportunityEffect;
    public string opportunityDescription;
    public bool repeatable;

    [Header("Opportunity Actions")]
    public Consequence[] consequences;
    public Prevention[] prevent;

    [Header("Opportunity Bonuses")]
    public int scopeBonus;
    public int moneyBonus;
    public int timeBonus;
}
