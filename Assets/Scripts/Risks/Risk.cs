using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "Risk", menuName = "Risks Dungeon/Risk", order = 0)]
public class Risk : ScriptableObject 
{
    [Header("Risk Infos")]
    public int id;

    //type can be 1, 2 or 3, respectively: planning, product, business
    public int type;
    public string riskClass;

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
    public string evaluatedProb;
    public string evaluatedImpact;

    [Header("Risk Probability")]
    //the probability is the number, between 0 and 1, that will in fact be used to calculate its chance to be activated
    //it match the scale: 0.1, 0.3, 0.5, 0.7, 0.9, following the scale from the level
    //the scale 0.05, 0.1, 0.2, 0.4, 0.8 may be implemented later for the player to choose
    public float baseProbability;
    public float probability;
    public float reincidence;

    [Header("Risk Effect")]
    //the impact of the risk in the player resources
    public int scopeCost;
    public int moneyCost;
    public int timeCost;

    //event variables
    public delegate void ActivatingRisk(Risk risk);
    public static event ActivatingRisk OnActivateRisk;

    // Start is called before the first frame update
    void Start()
    {
        //text.text = riskName;
        probability = baseProbability;
    }

    public void Subscribe()
    {
        
    }

    public void IncreaseProb(int mult)
    {
        if(probability < 0.9) probability += 0.1f * mult;
    }

    public void DecreaseProb(int mult)
    {
        //the minimum probability of a risk is 0.1;
        if(probability - 0.1f > 0.1f) probability -= 0.1f * mult;
    }

    public void ActivateRisk()
    {
        if(OnActivateRisk != null) OnActivateRisk(this);

        //Player player = GameObject.Find("Player").GetComponent<Player>();
        Player.risksActivated++;

        GameObject rskDisplay = GameObject.Find("Risk Game Display");
        rskDisplay.GetComponent<RiskGameDisplay>().risk = this;
        rskDisplay.GetComponent<RiskGameDisplay>().ShowRiskInfos();
        

        //check wich reaction is assigned to this risk and apply the correct action
        if(reaction == 0) CheckDisciplin();
        if(reaction == 1) ApplyMitigation();
        if(reaction == 2) AssignRisk();
        if(reaction == 3) AcceptRisk();

        probability = reincidence;

        //when a risk happens, its derivatives will have more chances to happen too
        foreach (Risk risk in derivatives)
        {
            risk.IncreaseProb(1);
        }
    }

    void CheckDisciplin()
    {
        if(Player.disciplin)
        {
            Player.OperateScope(-(scopeCost - 1));
            Player.OperateMoney(-(moneyCost - 1));
            Player.OperateTime(-(timeCost - 1));
        }
        else AcceptRisk();
    }

    public void ApplyMitigation()
    {
        int mod = 0;
        foreach (Employee employee in Player.team)
        {
            if(employee.skill.combat.Contains(this))
            {
                mod++;
                break;
            }
        }

        //if the player has the skill organized, add 1 to the impact reduction
        if(Player.organized)
        {
            Debug.Log("Organizado");
            mod++;
        }
        
        mod *= Player.combatPower;

        //the minimum cost for a risk is 0, else it will add resouces
        if(scopeCost - mod < 0) mod = scopeCost;
        if(moneyCost - mod < 0) mod = moneyCost;
        if(timeCost - mod < 0) mod = timeCost;

        //Player player = GameObject.Find("Player").GetComponent<Player>();
        Player.OperateScope(-(scopeCost - mod));
        Player.OperateMoney(-(moneyCost - mod));
        Player.OperateTime(-(timeCost - mod));
    }

    public void AssignRisk()
    {
        //Player player = GameObject.Find("Player").GetComponent<Player>();
        Player.DecreaseResources(moneyCost+2);
    }

    void AcceptRisk()
    {
        //Player player = GameObject.Find("Player").GetComponent<Player>();
        Player.OperateScope(-scopeCost);
        Player.OperateMoney(-moneyCost);
        Player.OperateTime(-timeCost);
    }
}