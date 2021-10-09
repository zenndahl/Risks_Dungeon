using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class Planning : MonoBehaviour
{
    [Header("Risks Infos")]
    //private int riskIndex = 0;
    private int riskType;
    private List<Risk> risksToPlan = new List<Risk>();
    private Risk riskOnPlanning;
    private int correctlyPlanned = 0;
    private int correctlyTyped = 0;
    private int assigned = 0;

    [Header("Preventions Infos")]
    public GameObject preventionsParent;
    public List<Prevention> preventions = new List<Prevention>();
    private Prevention preventionSelected;
    private int reaction;
    private int maxRange = 17;

    [Header("UI Infos")]
    public GameObject warningScreen;
    public GameObject feedbackScreen;
    public GameObject reactionScreen;
    public RiskDisplay riskDisplay;
    public GameObject[] preventionsDisplays;
    
    void Start()
    {
        risksToPlan = GameManager._instance.risksIdentified.ToList();
        NextRiskToPlan();
        RandomizePreventions();
    }

    //these are called by the pos buttons and set the reaction to be assign to the risk on planning
    public void SetMitigate()
    {
        reaction = 1;
    }

    public void SetAssign()
    {
        reaction = 2;
    }

    public void SetAccept()
    {
        reaction = 3;
    }

    public void SetRiskType(int type)
    {
        riskType = type;
    }

    //call and set the next risk to be planned
    public void NextRiskToPlan()
    {
        if(risksToPlan.Any()) 
        {
            riskDisplay.risk = risksToPlan[0];
            riskOnPlanning = risksToPlan[0];
            riskDisplay.SetInfos();
        }
    }

    public void RandomizePreventions()
    {
        List<int> randomList = new List<int>();
        int randNum;
        int randNum2;
        //get the employee display objects under the selection parent and randomize the employees displayed
        foreach (GameObject goPD in preventionsDisplays)
        {
            PreventionDisplay pd = goPD.GetComponent<PreventionDisplay>();
            randNum = Random.Range(0,maxRange);
            
            while(randomList.Contains(randNum))
    	        randNum = Random.Range(0,maxRange);
            randomList.Add(randNum);

            pd.prevention = preventions[randNum];

            //fazer checagem se tem membros repetidos e permitir novo sorteio
            
            //need to reset the display for the new employee to be shown in the display
            pd.ResetInfos();
        }

        //chose randomly a display to set an correct prevention for the risk on planning
        randNum = Random.Range(0,riskOnPlanning.preventions.Length);
        while(randomList.Contains(randNum))
            randNum = Random.Range(0,riskOnPlanning.preventions.Length);
        randomList.Add(randNum);
        
        randNum2 = Random.Range(0,2);
        PreventionDisplay pvD = preventionsDisplays[randNum2].GetComponent<PreventionDisplay>();
        pvD.prevention = riskOnPlanning.preventions[randNum];
        pvD.ResetInfos();
    }

    void CheckSetPrevention()
    {
        GameManager._instance.risksIdentified.Find(risk => risk == riskOnPlanning).reaction = 1;
        
        //planning for mitigation costs 1 of each resource even if its wrong
        GameObject.Find("Player").GetComponent<Player>().OperateScope(1);
        GameObject.Find("Player").GetComponent<Player>().OperateMoney(-1); 
        GameObject.Find("Player").GetComponent<Player>().OperateTime(-2);

        //if the prevention is correct, add the prevention to the preventions list
        if(riskOnPlanning.preventions.Contains(preventionSelected))
        {
            GameManager.preventionsMade.Add(preventionSelected);
            correctlyPlanned++;
        }
    }

    public void Prevent()
    {
        if(riskType == 0)
        {
            //GameObject.FindObjectsOfType(Button).GetComponent<Button>().enabled = false;
            warningScreen.SetActive(true);
        }
        else
        {
            //verify if the type given is correct, giving points if it is
            VerifyType();

            if(preventionSelected != null) 
            {
                preventionSelected = Add.selected.GetComponent<PreventionDisplay>().prevention;
            }
        }

        reactionScreen.SetActive(true);
    }

    public void React()
    {
        riskOnPlanning.reaction = reaction;

        //verify if the prevention selected is correct for mitigation
        if(reaction == 1)
        {
            if(preventionSelected != null) CheckSetPrevention();
            
        }

        //add to the assigned risks list (assigned risks cost money when the risk occurs)
        if(reaction == 2) 
        {
            GameManager._instance.risksIdentified.Find(risk => risk == riskOnPlanning).reaction = 2;
            GameManager.risksAssigned.Add(riskOnPlanning);
            assigned++;
        }

        //set the risk to the "accept" reaction
        if (reaction == 3)
        {
            GameManager._instance.risksIdentified.Find(risk => risk == riskOnPlanning).reaction = 3;
        }

        //check if there is another risk to plan, if not, finish the planning
        risksToPlan.RemoveAt(0);
        if(risksToPlan.Any()) 
        {
            //check if a prevention was selected, if it was, move it back to the initial place
            
            if(GameObject.Find("PreventionHolder").transform.childCount > 0)
            {
                Transform preventHolder = GameObject.Find("PreventionHolder").transform.GetChild(0);
                preventHolder.SetParent(preventionsParent.transform);
            }

            reactionScreen.SetActive(false);
            RandomizePreventions();
            NextRiskToPlan();
        }
        else FinishPlanning();
    }

    void VerifyType()
    {
        if(riskOnPlanning.type == riskType)
        {
            GameObject.Find("Player").GetComponent<Player>().IncreaseResources(1);
            correctlyTyped++;
        }
    }

    void FinishPlanning()
    {
        //show the feedback screen
        feedbackScreen.SetActive(true);

        foreach (Risk risk in GameManager._instance.risksIdentified)
        {
            
        }

        //display feedback of correctly identified risks and the current resources of the player
        feedbackScreen.GetComponent<Feedback>().DisplayFeedback("planejou", correctlyPlanned, correctlyTyped, assigned);
    }
}
