using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Evaluation : MonoBehaviour
{
    public GameObject riskDisplayPrefab;
    public GameObject feedbackScreen;
    //public GameObject cellRisksList;
    private int correctlyEvaluated = 0;
    private int closelyEvaluated = 0;

    private void Start()
    {
        SetUpEvaluation();
    }

    void SetUpEvaluation()
    {
        if(GameManager.risksIdentified.Count == 0) GameManager.LoadNextScene();
        GameObject rskList = GameObject.Find("Risk List/Risks");
        foreach (Risk rsk in GameManager.risksIdentified)
        {
            //instantiating and setting the parent for the risk
            GameObject rskDisplay = Instantiate(riskDisplayPrefab, rskList.transform);

            //set spacing between risks
            rskList.GetComponent<RectTransform>().offsetMin -= new Vector2(0,100);

            //rskDisplay.transform.SetParent(rskList.transform);
            //assigning the risk to the display
            rskDisplay.GetComponent<RiskDisplay>().risk = rsk;

            //adding button listeners events by calling method inside RiskDisplay
            rskDisplay.GetComponent<RiskDisplay>().SetEvents(rskDisplay);
        }
    }

    void SetRiskEvaluationChoosed(Risk risk, MatrixRiskDisplay matrixRiskDisplay)
    {
        //set the probability text
        switch (matrixRiskDisplay.prob)
        {
            case 1:
                risk.evaluatedProb = "Muito Baixa";
                break;
            case 2:
                risk.evaluatedProb = "Baixa";
                break;
            case 3:
                risk.evaluatedProb = "Moderada";
                break;
            case 4:
                risk.evaluatedProb = "Alta";
                break;
            case 5:
                risk.evaluatedProb = "Muito Alta";
                break;
            default:
                break;
        }

        //set the impact text
        switch (matrixRiskDisplay.impact)
        {
            case 1:
                risk.evaluatedImpact = "Muito Baixo";
                break;
            case 2:
                risk.evaluatedImpact = "Baixo";
                break;
            case 3:
                risk.evaluatedImpact = "Moderado";
                break;
            case 4:
                risk.evaluatedImpact = "Alto";
                break;
            case 5:
                risk.evaluatedImpact = "Muito Alto";
                break;
            default:
                break;
        }
    }

    public void Evaluate(Risk risk, MatrixRiskDisplay matrixRiskDisplay)
    {
        //Player player = GameObject.Find("Player").GetComponent<Player>();
        GameObject rskList = GameObject.Find("Risks");

        //set the evaluation probability and impact chose by the player
        SetRiskEvaluationChoosed(risk, matrixRiskDisplay);

        //if the player chooses the correct probabilit and impact, he gets 3 points
        //if it gets the probability or the impact, he gets 1 point
        //else he gets no points
        if(risk.impactLevel == matrixRiskDisplay.impact && risk.probLevel == matrixRiskDisplay.prob)
        {
            GameManager.risksCorrectlyEvaluated.Add(risk);
            Player.IncreaseResources(3);
            correctlyEvaluated++;
        }
        else if(risk.impactLevel - matrixRiskDisplay.impact < 2)
        {
            Player.IncreaseResources(1);
            closelyEvaluated++;
        } 
        else if(risk.probLevel - matrixRiskDisplay.prob < 2)
        {
            Player.IncreaseResources(1);
            closelyEvaluated++;
        }

        GameManager.risks.Remove(risk);
        if(!GameManager.risks.Any()) FinishEvaluation();
        
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
