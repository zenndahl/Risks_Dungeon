using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Identification : MonoBehaviour
{
    public GameObject feedbackScreen;
    private int correctlyId = 0;

    public void AddId(Risk risk)
    {
        GameManager.risksIdentified.Add(risk);
    }

    public void RemoveId(Risk risk)
    {
        GameManager.risksIdentified.Remove(risk);
    }

    public void FinishIdentification()
    {
        //hide the risks lists
        GameObject.Find("Identification").SetActive(false);
        //Player player = GameObject.Find("Player").GetComponent<Player>();

        foreach (Risk risk in GameManager.risksIdentified)
        {
            //check if the risk identified is general or for the selected project and adds the resources if it is
            if(risk.project == 0 || risk.project == GameManager.project)
            {
                Player.IncreaseResources(1);
                GameManager.risksCorrectlyIdentified.Add(risk);
                correctlyId++;
            }
            else //if the risk is identified incorrectly decrease the player resources
            {
                Player.DecreaseResources(1);
            }
        }

        //show the feedback screen
        feedbackScreen.SetActive(true);

        //display correctly feedbakc of identified risks and the current resources of the player
        feedbackScreen.GetComponent<Feedback>().DisplayFeedback("identificou", correctlyId);

        GameManager.risks = GameManager.risksIdentified.ToList();
        
    }
}
