using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "Risk", menuName = "Risks Dungeon/Risk", order = 0)]
public class Risk : ScriptableObject 
{
    [Header("Risk Infos")]
    public int id;

    //type can be 1, 2 or 3, respectively: planning, product, business
    public int type;

    //project can be 0, 1 or 2, respectively: any project, ERp project, app project
    public int project;
    public string riskName;
    public string riskDescription;

    //the sprite to show in the dungeon when the risk activates
    public Sprite sprite; 
    public Prevention[] preventions;

    //the action planned to be take once the risk occurs, 1 - mitigate, 2 - assign, 3 - accept
    public int reaction;
    public bool prevented = false;
    //updates the times the risk has been prevented, therefore decreasing its probability
    public int timesPrevented;
    //risks that have its probability increased if this risk is activated
    public Risk[] derivatives;
    //impact and prob level goes from 1 to 5, matchin the scale: very low, low, medium, high, very high
    public int impactLevel;
    public int probLevel;

    [Header("Risk Probability")]
    //the probability is the number, between 0 and 1, that will in fact be used to calculate its chance to be activated
    //it match the scale: 0.1, 0.3, 0.5, 0.7, 0.9, following the scale from the level
    //the scale 0.05, 0.1, 0.2, 0.4, 0.8 may be implemented later for the player to choose
    public float probability;
    public float reincidence;

    [Header("Risk Effect")]
    //the impact of the risk in the player resources
    public int scopeCost;
    public int moneyCost;
    public int timeCost;       

    // Start is called before the first frame update
    void Start()
    {
        //text.text = riskName;
    }

    public void IncreaseProb(){
        probability += 0.2f;
    }

    public void DecreaseProb(){
        probability -= 0.2f;
    }

    public void ActivateRisk(){
        Player player = GameObject.Find("Player").GetComponent<Player>();

        player.DecreaseResources(scopeCost);
        player.DecreaseResources(moneyCost);
        player.DecreaseResources(timeCost);

        foreach (Risk risk in derivatives)
        {
            risk.IncreaseProb();
        }
    }

    public void ApplyMitigation(int mitigation)
    {
        scopeCost -= mitigation;
        moneyCost -= mitigation;
        timeCost -= mitigation;

        ActivateRisk();

        scopeCost += mitigation;
        moneyCost += mitigation;
        timeCost += mitigation;
    }
}
