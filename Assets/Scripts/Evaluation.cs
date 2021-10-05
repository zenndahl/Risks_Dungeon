using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Evaluation : MonoBehaviour
{
    public GameObject riskDisplayPrefab;
    public GameObject feedbackScreen;
    private int correctlyEvaluated = 0;
    private int closelyEvaluated = 0;

    private void Start()
    {
        SetUpEvaluation();
    }
    void SetUpEvaluation()
    {
        GameObject rskList = GameObject.Find("Risk List/Risks");
        foreach (Risk rsk in GameManager._instance.risksIdentified)
        {
            //instantiating and setting the parent for the risk
            GameObject rskDisplay = Instantiate(riskDisplayPrefab, rskList.transform);

            //set spacing between risks
            //rskList.GetComponent<VerticalLayoutGroup>().spacing += 0.5f;

            //rskDisplay.transform.SetParent(rskList.transform);
            rskDisplay.GetComponent<RiskDisplay>().risk = rsk;

            //adding button listeners events by calling method inside RiskDisplay
            rskDisplay.GetComponent<RiskDisplay>().SetEvents(rskDisplay);
        }
    }

    public void Evaluate(Risk risk, RiskDisplay riskDisplay)
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        GameObject rskList = GameObject.Find("Risks");

        if(risk.impactLevel == riskDisplay.impact && risk.probLevel == riskDisplay.prob)
        {
            player.IncreaseResources(3);
            correctlyEvaluated++;
            //Debug.Log("Pontos ganhos: " + 3);
        }
        else if(risk.impactLevel - riskDisplay.impact < 2)
        {
            player.IncreaseResources(1);
            closelyEvaluated++;
            //Debug.Log("Pontos ganhos: " + 1);
        } 
        else if(risk.probLevel - riskDisplay.prob < 2)
        {
            player.IncreaseResources(1);
            closelyEvaluated++;
            //Debug.Log("Pontos ganhos: " + 1);
        }

        GameManager._instance.risks.Remove(risk);
        if(!GameManager._instance.risks.Any()) FinishEvaluation();
        
    }
    void FinishEvaluation()
    {
        //hide the risks lists
        GameObject.Find("Evaluation").SetActive(false);

        //show the feedback screen
        feedbackScreen.SetActive(true);

        //display correctly feedbakc of identified risks and the current resources of the player
        feedbackScreen.GetComponent<Feedback>().DisplayFeedback("identificou", correctlyEvaluated, closelyEvaluated);
    }
}
