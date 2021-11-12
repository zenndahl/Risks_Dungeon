using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RisksAgent : MonoBehaviour
{
    private static RisksAgent _instance;
    [Header("Curves")]
    [SerializeField] private AnimationCurve veryHigh;
    [SerializeField] private AnimationCurve high;
    [SerializeField] private AnimationCurve moderate;
    [SerializeField] private AnimationCurve low;
    [SerializeField] private AnimationCurve veryLow;

    //evaluate functions
    float evVH(float x) {return veryHigh.Evaluate(x);}
    float evH(float x) {return high.Evaluate(x);}
    float evM(float x) {return moderate.Evaluate(x);}
    float evL(float x) {return low.Evaluate(x);}
    float evVL(float x) {return veryLow.Evaluate(x);}

    private List<Risk> risksPossible = new List<Risk>();
    private Risk riskChosen;
    private Risk previousRisk;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        DungeonAgent.OnDungeonActionCompleted += See;
        Room.OnEnterRoom += CallWait;
    }

    void See()
    {
        risksPossible = GameObject.Find("Beholder").GetComponent<DungeonAgent>().GetRisksPossible();
    }

    //will choose a risk
    void Perception()
    {
        foreach (Risk risk in risksPossible)
        {
            risk.VH = evVH(risk.probability);
            risk.H = evH(risk.probability);
            risk.M = evM(risk.probability);
            risk.L = evL(risk.probability);
            risk.VL = evVL(risk.probability);
        }

        //will randomly chose three of the risks possible
        List<int> randomList = new List<int>();

        int randIndex1 = Random.Range(0,risksPossible.Count);
        while(risksPossible[randIndex1] == previousRisk && previousRisk != null)
            randIndex1 = Random.Range(0,risksPossible.Count);
        randomList.Add(randIndex1);

        int randIndex2 = Random.Range(0,risksPossible.Count);
        while(randomList.Contains(randIndex2) || risksPossible[randIndex2] == previousRisk)
            randIndex2 = Random.Range(0,risksPossible.Count);
        randomList.Add(randIndex2);

        int randIndex3 = Random.Range(0,risksPossible.Count);
        while(randomList.Contains(randIndex3) || risksPossible[randIndex3] == previousRisk)
            randIndex3 = Random.Range(0,risksPossible.Count);
        randomList.Add(randIndex3);

        //will set each fuzzy values of the risks, then get the risk with the higher value based on the difficulty

        
        switch (GameManager.difficulty)
        {
            //hard
            case 1:
                if((risksPossible[randIndex1].VH > risksPossible[randIndex2].VH &&
                    risksPossible[randIndex1].VH > risksPossible[randIndex3].VH) ||
                    (risksPossible[randIndex1].H > risksPossible[randIndex2].H &&
                    risksPossible[randIndex1].H > risksPossible[randIndex3].H))
                {
                    riskChosen = risksPossible[randIndex1];
                }
                else if(risksPossible[randIndex2].VH > risksPossible[randIndex1].VH &&
                    risksPossible[randIndex2].VH > risksPossible[randIndex3].VH ||
                    (risksPossible[randIndex2].H > risksPossible[randIndex2].H &&
                    risksPossible[randIndex2].H > risksPossible[randIndex3].H))
                {
                    riskChosen = risksPossible[randIndex2];   
                }
                else riskChosen = risksPossible[randIndex3];
                break;
            //normal
            case 2:
                if((risksPossible[randIndex1].H > risksPossible[randIndex2].H &&
                    risksPossible[randIndex1].H > risksPossible[randIndex3].H )||
                    (risksPossible[randIndex1].M > risksPossible[randIndex2].M &&
                    risksPossible[randIndex1].M > risksPossible[randIndex3].M))
                {
                    riskChosen = risksPossible[randIndex1];
                }
                else if((risksPossible[randIndex2].H > risksPossible[randIndex1].H &&
                    risksPossible[randIndex2].H > risksPossible[randIndex3].H) ||
                    (risksPossible[randIndex2].M > risksPossible[randIndex2].M &&
                    risksPossible[randIndex2].M > risksPossible[randIndex3].M))
                {
                    riskChosen = risksPossible[randIndex2];   
                }
                else riskChosen = risksPossible[randIndex3];
                break;
            //easy
            case 3:
                if((risksPossible[randIndex1].L > risksPossible[randIndex2].L &&
                    risksPossible[randIndex1].L > risksPossible[randIndex3].L) ||
                    (risksPossible[randIndex2].VL > risksPossible[randIndex2].VL &&
                    risksPossible[randIndex2].VL > risksPossible[randIndex3].VL))
                {
                    riskChosen = risksPossible[randIndex1];
                }
                else if((risksPossible[randIndex2].L > risksPossible[randIndex1].L &&
                    risksPossible[randIndex2].L > risksPossible[randIndex3].L)  ||
                    (risksPossible[randIndex2].VL > risksPossible[randIndex2].VL &&
                    risksPossible[randIndex2].VL > risksPossible[randIndex3].VL))
                {
                    riskChosen = risksPossible[randIndex2];   
                }
                else riskChosen = risksPossible[randIndex3];
                break;
                
            default:
                riskChosen = risksPossible[randIndex1];
                break;
        }
        Action();
    }

    void Action()
    {
        //will draw the probability
        float randProb = Random.Range(0f,1f);

        //if the probability draw is less than the probability of the chosen risk, it happens
        if(randProb <= riskChosen.probability)
        {
            riskChosen.ActivateRisk();
            previousRisk = riskChosen;
            GameManager.risksInSequence++;
        }
        else //else an opportunity is drawn
        {
            RisksAgent.DrawOpportunity();
            GameManager.risksInSequence = 0;
        }
    }

    void CallWait()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        Perception();
    }

    public static void DrawOpportunity()
    {
        //will randomly chose one of the opportunities
        int randOpp = Random.Range(0,GameManager.opportunities.Count);
        GameObject oppDisplay = GameObject.Find("Opportunity Display");
        oppDisplay.GetComponent<OpportunityDisplay>().opportunity = GameManager.opportunities[randOpp];
        oppDisplay.GetComponent<OpportunityDisplay>().ShowDescription();
    }

    private void OnDestroy() {
        DungeonAgent.OnDungeonActionCompleted -= See;
        Room.OnEnterRoom -= CallWait;
    }
}