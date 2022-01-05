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

    private Player player;
    private GameManager gameManager;
    
    void Start()
    {
        gameManager = GameManager.Instance;
        player = Player.PlayerInstance;
        risksToPlan = gameManager.risksIdentified.ToList();
        if(risksToPlan.Count == 0) gameManager.LoadNextScene();
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

    public void SetPrevention(Prevention prevention)
    {
        preventionSelected = prevention;
    }

    void Mitigate()
    {        
        //planning for mitigation costs 1 money and 2 time
        player.OperateMoney(-1);
        player.OperateTime(-2);

        //if the prevention is correct, add the prevention to the preventions list and apply the prevention
        if(riskOnPlanning.preventions.Contains(preventionSelected))
        {
            //if the prevention is right, set the reaction to the risk as mitigate
            gameManager.risksIdentified.Find(risk => risk == riskOnPlanning).reaction = 1;
            //reward for correctly planning a risk
            player.OperateScope(1);
            player.preventCorrect++;
            riskOnPlanning.prevented = true;
            gameManager.preventionsMade.Add(preventionSelected);
            //gameManager.risksCorrectlyPlanned.Add(riskOnPlanning);
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
            gameManager.risksIdentified.Find(risk => risk == riskOnPlanning).reaction = 2;
            gameManager.risksAssigned.Add(riskOnPlanning);
            assigned++;
        }

        //set the risk to the "accept" reaction
        if (reaction == 3)
        {
            gameManager.risksIdentified.Find(risk => risk == riskOnPlanning).reaction = 3;
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
            player.IncreaseResources(5);
            correctlyTyped++;
        }
    }

    void FinishPlanning()
    {
        //Player player = GameObject.Find("Player").GetComponent<Player>();
        //show the feedback screen
        feedbackScreen.SetActive(true);

        //decrease the probability of the risks if the employees have skills that do so
        if(gameManager.project == 1)
        {
            foreach (Risk risk in GameObject.Find("Game Manager").GetComponent<GameManager>().GetProject1Risks())
            {   
                //activate the preventions from the team 
                foreach (Employee employee in player.team)
                {
                    //for each risk of the project, if the employees combat them, their probability is reduced
                    if(employee.skill.combat.Contains(risk))
                    {
                        risk.DecreaseProb(1);
                    }
                }
            }
        }
        if(gameManager.project == 2)
        {
            foreach (Risk risk in GameObject.Find("Game Manager").GetComponent<GameManager>().GetProject2Risks())
            {   
                foreach (Employee employee in player.team)
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
