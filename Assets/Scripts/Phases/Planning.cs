using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class Planning : MonoBehaviour
{
    [Header("Risks Infos")]
    private int riskType;
    public List<Risk> risksToPlan = new List<Risk>();
    private Risk riskOnPlanning;
    private int correctlyPlanned = 0;
    private int correctlyTyped = 0;
    private int assigned = 0;

    [Header("Preventions Infos")]
    public GameObject preventDispPrefab;
    public List<Prevention> preventions = new List<Prevention>();
    private Prevention preventionSelected;
    private int reaction;

    [Header("UI Infos")]
    public GameObject warningScreen;
    public GameObject feedbackScreen;
    public GameObject preventionsScreen;
    public GameObject reactionScreen;
    public RiskDisplay riskDisplay;
    public GameObject preventionsDisplay;
    
    void Start()
    {
        risksToPlan = GameManager.risksIdentified.ToList();
        if(risksToPlan.Count == 0) GameManager.LoadNextScene();
        else
        {
            //SetPreventions();
            NextRiskToPlan();
        }
        //RandomizePreventions();
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
            GameObject probDesc = GameObject.Find("Prob Evaluation");
            GameObject imapctDesc = GameObject.Find("Impact Evaluation");
            probDesc.GetComponent<Text>().text = "Prob.: " + riskOnPlanning.evaluatedProb;
            imapctDesc.GetComponent<Text>().text = "Impacto: " +  riskOnPlanning.evaluatedImpact;
        }
    }

    // public void SetPreventions()
    // {
    //     foreach (Prevention prevention in GameManager.preventions)
    //     {
    //         GameObject preventDisp = Instantiate(preventDispPrefab, transform.position, transform.rotation);
    //         preventDisp.GetComponent<PreventionDisplay>().prevention = prevention;
    //         preventDisp.transform.SetParent(preventionsScreen.transform);
    //         preventDisp.GetComponent<PreventionDisplay>().SetInfos();
    //         preventionsScreen.GetComponent<RectTransform>().offsetMin -= new Vector2(0,100);
    //     }
    // }

    // public void RandomizePreventions()
    // {
    //     List<int> randomList = new List<int>();
    //     int randNum;
    //     int randNum2;

    //     //get the preventions display objects under the planning parent and randomize the preventions displayed
    //     foreach (GameObject goPD in preventionsDisplays)
    //     {
    //         PreventionDisplay pd = goPD.GetComponent<PreventionDisplay>();
    //         randNum = Random.Range(0,maxRange);
            
    //         while(randomList.Contains(randNum))
    // 	        randNum = Random.Range(0,maxRange);
    //         randomList.Add(randNum);

    //         pd.prevention = preventions[randNum];
            
    //         //need to reset the display for the new employee to be shown in the display
    //         pd.ResetInfos();
    //     }

    //     //chose randomly a display to set an correct prevention for the risk on planning
    //     randNum = Random.Range(0, riskOnPlanning.preventions.Length);
    //     while(randomList.Contains(randNum))
    //         randNum = Random.Range(0,riskOnPlanning.preventions.Length);
    //     randomList.Add(randNum);
        
    //     randNum2 = Random.Range(0,2);
    //     PreventionDisplay pvD = preventionsDisplays[randNum2].GetComponent<PreventionDisplay>();
    //     pvD.prevention = riskOnPlanning.preventions[randNum];
    //     pvD.ResetInfos();
    // }

    public void SetPrevention(Prevention prevention)
    {
        preventionSelected = prevention;
    }

    void Mitigate()
    {        
        //planning for mitigation costs 1 money and 2 time
        Player.OperateMoney(-1);
        Player.OperateTime(-2);

        //if the prevention is correct, add the prevention to the preventions list and apply the prevention
        if(riskOnPlanning.preventions.Contains(preventionSelected))
        {
            //if the prevention is right, set the reaction to the risk as mitigate
            GameManager.risksIdentified.Find(risk => risk == riskOnPlanning).reaction = 1;
            //reward for correctly planning a risk
            Player.OperateScope(1);
            Player.preventCorrect++;
            GameManager.preventionsMade.Add(preventionSelected);
            GameManager.risksCorrectlyPlanned.Add(riskOnPlanning);
            correctlyPlanned++;

            //get the risks list, find the current risk on planning and decrease its probability
            List<Risk> rskList = GameObject.Find("Game Manager").GetComponent<GameManager>().GetProject1Risks();
            if(rskList.Any(x => x == riskOnPlanning))
            {
                rskList.Find(x => x == riskOnPlanning).DecreaseProb(1);
            }

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
            preventionsDisplay.SetActive(false);
            reactionScreen.SetActive(true);
        }
    }

    public void React()
    {
        riskOnPlanning.reaction = reaction;

        //verify if the prevention selected is correct for mitigation
        if(reaction == 1)
        {
            //will not apply mitigation if no prevention was selected
            if(preventionSelected != null) Mitigate();
            
        }

        //add to the assigned risks list (assigned risks cost money when the risk occurs)
        if(reaction == 2) 
        {
            GameManager.risksIdentified.Find(risk => risk == riskOnPlanning).reaction = 2;
            GameManager.risksAssigned.Add(riskOnPlanning);
            assigned++;
        }

        //set the risk to the "accept" reaction
        if (reaction == 3)
        {
            GameManager.risksIdentified.Find(risk => risk == riskOnPlanning).reaction = 3;
        }

        //check if there is another risk to plan, if not, finish the planning
        risksToPlan.RemoveAt(0);
        if(risksToPlan.Any()) 
        {
            //check if a prevention was selected, if it was, move it back to the initial place
            if(GameObject.Find("PreventionHolder").transform.childCount > 0)
            {
                Transform preventHolder = GameObject.Find("PreventionHolder").transform.GetChild(0);
                preventHolder.SetParent(preventionsScreen.transform);
            }

            reactionScreen.SetActive(false);
            preventionsDisplay.SetActive(true);
            NextRiskToPlan();
        }
        else FinishPlanning();
    }

    void VerifyType()
    {
        if(riskOnPlanning.type == riskType)
        {
            Player.IncreaseResources(5);
            correctlyTyped++;
        }
    }

    void FinishPlanning()
    {
        //Player player = GameObject.Find("Player").GetComponent<Player>();
        //show the feedback screen
        feedbackScreen.SetActive(true);

        //decrease the probability of the risks if the employees have skills that do so
        if(GameManager.project == 1)
        {
            foreach (Risk risk in GameObject.Find("Game Manager").GetComponent<GameManager>().GetProject1Risks())
            {   
                //activate the preventions from the team 
                foreach (Employee employee in Player.team)
                {
                    //for each risk of the project, if the employees combat them, their probability is reduced
                    if(employee.skill.combat.Contains(risk))
                    {
                        risk.DecreaseProb(1);
                    }
                }
            }
        }
        if(GameManager.project == 2)
        {
            foreach (Risk risk in GameObject.Find("Game Manager").GetComponent<GameManager>().GetProject2Risks())
            {   
                foreach (Employee employee in Player.team)
                {
                    //for each risk of the project, if the employees combat them, their probability is reduced
                    if(employee.skill.combat.Contains(risk))
                    {
                        risk.DecreaseProb(1);
                    }
                }
            }
        }
        
        //display feedback of correctly identified risks and the current resources of the player
        feedbackScreen.GetComponent<Feedback>().DisplayFeedback("planejou", correctlyPlanned, correctlyTyped, assigned);
    }
}
