using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine;

public class TeamAgent : MonoBehaviour
{
    [Header("Curves")]
    [SerializeField] private AnimationCurve high;
    [SerializeField] private AnimationCurve normal;
    [SerializeField] private AnimationCurve low;

    [Header("State")]
    public TeamState teamState;
    public TeamState oldState;

    public float teamMorale;
    private float defuzzy;
    private float highMorale;
    private float normalMorale;
    private float lowMorale;
    // private float high3Morale;
    // private float normal3Morale;
    // private float low3Morale;
    private List<float> hM = new List<float>();
    private List<float> nM = new List<float>();
    private List<float> lM = new List<float>();
    private List<float> highList = new List<float>();
    private List<float> normalList = new List<float>();
    private List<float> lowList = new List<float>();

    public enum TeamState
    {
        Dedicada,
        Normal,
        Desmotivada
    }

    //evaluate functions
    float evH(float x) {return high.Evaluate(x);}
    float evN(float x) {return normal.Evaluate(x);}
    float evL(float x) {return low.Evaluate(x);}

    private void Start()
    {
        //reset the variables
        teamState = TeamState.Normal;
        oldState = teamState;
        highList.Clear();
        normalList.Clear();
        lowList.Clear();
        hM.Clear();
        nM.Clear();
        lM.Clear();
        highMorale = 0;
        //high3Morale = 0;
        normalMorale = 0;
        //normal3Morale = 0;
        lowMorale = 0;
        //low3Morale = 0;
    }

    void AtLeast3Rule()
    {
        //1
        //     //gets the min of each triplet (operator 'and') on the employee morale list
        //     for(int i = 0; i <= 2; i++)
        //     {
        //         for(int j = i+1; j <= hM.Count-1; j++)
        //         {
        //             for(int k = j+1; k <= hM.Count; k++)
        //             {
        //                 Debug.Log(i + "/" + j + "/" + k);
        //                 highList.Add(System.Math.Min(hM.ElementAt(i),System.Math.Min(hM.ElementAt(j), hM.ElementAt(j))));
        //                 Debug.Log(System.Math.Min(hM.ElementAt(i),System.Math.Min(hM.ElementAt(j), hM.ElementAt(j))));
        //             }
        //         }
        //     }
        //     //gets the max value between the triplets (operator 'or')
        //     high3Morale = highList.Max();

        //     //gets the min of each triplet (operator 'and') on the employee morale list
        //     for(int i = 0; i <= 2; i++)
        //     {
        //         for(int j = i+1; j <= nM.Count-1; j++)
        //         {
        //             for(int k = j+1; k <= nM.Count; k++)
        //             {
        //                 normalList.Add(System.Math.Min(nM.ElementAt(i),System.Math.Min(nM.ElementAt(j), nM.ElementAt(j))));
        //             }
        //         }
        //     }
        //     //gets the max value between the triplets (operator 'or')
        //     normal3Morale = normalList.Max();

        //     //gets the min of each triplet (operator 'and') on the employee morale list
        //     for(int i = 0; i <= 2; i++)
        //     {
        //         for(int j = i+1; j <= lM.Count-1; j++)
        //         {
        //             for(int k = j+1; k <= lM.Count; k++)
        //             {
        //                 lowList.Add(System.Math.Min(lM.ElementAt(i),System.Math.Min(lM.ElementAt(j), lM.ElementAt(j))));
        //                 Debug.Log(System.Math.Min(lM.ElementAt(i),System.Math.Min(lM.ElementAt(j), lM.ElementAt(j))));
        //             }
        //         }
        //     }
        //     //gets the max value between the triplets (operator 'or')
        //     low3Morale = lowList.Max();
    }

    public void CalculateTeamMorale()
    {
        //fuzzy rules
        highMorale = hM.Min(); //all the employees are with high motivation
        normalMorale = nM.Min(); //all the employees are with normal motivation
        lowMorale = lM.Min(); //all the employees are with low motivation
        //AtLeast3Rule();

        //defuzzyfication
        //get the sets sum and multiply by their rules output
        defuzzy = hM.Sum()*highMorale + nM.Sum()*normalMorale + lM.Sum()*lowMorale;
        //defuzzy += highList.Sum()*high3Morale + normalList.Sum()*normal3Morale + lowList.Sum()*low3Morale;

        //then divides by the rules output times the number of members of each set
        float denominator = highMorale*5 + normalMorale*5 + lowMorale*5;
        //denominator += high3Morale*highList.Count + normal3Morale*normalList.Count + low3Morale*lowList.Count;
        defuzzy /= denominator;

        //pass the defuzzyfied valor through the fuzzy sets to determine wich one of them is more suited
        teamMorale = System.Math.Max(evH(defuzzy), System.Math.Max(evN(defuzzy), evL(defuzzy)));

        //set the team morale state based on the defuzzyfication output
        //compare the max with each result and find the match, then set the state accordingly
        if(teamMorale == evH(defuzzy)) 
        {
            teamState = TeamState.Dedicada;
        }
        if(teamMorale == evN(defuzzy)) 
        {
            teamState = TeamState.Normal;
        }
        if(teamMorale == evL(defuzzy)) 
        {
            teamState = TeamState.Desmotivada;
        }

        //see if the state has changed
        Perception();
    }

    void Perception()
    {
        //if the state change, do the actions
        if(oldState != teamState) Action((int)teamState);
        oldState = teamState;
    }

    //will set the team state by fuzzy logic
    public void SetTeamMorales(Employee employee)
    {
        hM.Clear();
        nM.Clear();
        lM.Clear();
        //fuzzyfication of the morale
        float mH = evH(employee.morale);
        float mN = evN(employee.morale);
        float mL = evL(employee.morale);
        hM.Add(mH);
        nM.Add(mN);
        lM.Add(mL);
    }

    //actions
    void Action(int state)
    {
        foreach (Risk risk in GameManager.risks)
        {
            //if the team is dedicated
            if(state == 0)
            {
                if(risk.riskClass == "Equipe" || risk.riskClass == "Produto") risk.DecreaseProb(1);
            }

            //if the team is unmotivated
            if(state == 3)
            {
                //increase prob of proudct and team risks
                if(risk.riskClass == "Equipe" || risk.riskClass == "Produto") risk.IncreaseProb(1);
            }
        }
    }
}
