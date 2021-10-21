using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine;

public class TeamAgent : MonoBehaviour
{
    public AnimationCurve high;
    public AnimationCurve normal;
    public AnimationCurve low;
    public float teamMorale;
    public TeamState teamState;
    public float highMorale;
    public float normalMorale;
    public float lowMorale;
    private List<float> hM = new List<float>();
    private List<float> nM = new List<float>();
    private List<float> lM = new List<float>();

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
        highMorale = 0;
        normalMorale = 0;
        lowMorale = 0;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //RiskGameDisplay.OnDisplayDismissed += CalculateTeamMorale;
    }

    //set the team morale state based on the fuzzy output
    void SetTeamMoraleState()
    {
        if(teamMorale <= 0.3) teamState = TeamState.Desmotivada;
        if(teamMorale > 0.3 && teamMorale < 0.7) teamState = TeamState.Normal;
        else teamState = TeamState.Dedicada;
        Debug.Log(teamState);
    }

    public void CalculateTeamMorale()
    {
        //fuzzy rules
        highMorale += hM.Min();
        normalMorale += nM.Min();
        lowMorale += lM.Min();
        //adicionar mais

        teamMorale = System.Math.Max(highMorale, System.Math.Max(normalMorale, lowMorale));
        Debug.Log(teamMorale);     

        SetTeamMoraleState();
    }

    //will set the team state by fuzzy logic
    public void SetTeamMorales(Employee employee)
    {
        //fuzzyfication of the morale
        float mH = evH(employee.morale);
        float mN = evN(employee.morale);
        float mL = evL(employee.morale);
        hM.Add(mH);
        nM.Add(mN);
        lM.Add(mL);
    }
}
