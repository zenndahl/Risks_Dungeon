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
        GameManager._instance.risksIdentified.Add(risk);
    }

    public void RemoveId(Risk risk)
    {
        GameManager._instance.risksIdentified.Remove(risk);
    }

    public void FinishIdentification()
    {
        //hide the risks lists
        GameObject.Find("Identification").SetActive(false);
        Player player = GameObject.Find("Player").GetComponent<Player>();

        foreach (Risk risk in GameManager._instance.risksIdentified)
        {
            //check if the risk identified is general or for the selected project and adds the resources if it is
            if(risk.project == 0 || risk.project == GameManager._instance.project)
            {
                player.IncreaseResources(1);
                correctlyId++;
            }
            else //if the risk is identified incorrectly decrease the player resources
            {
                player.DecreaseResources(1);
            }
        }

        //show the feedback screen
        feedbackScreen.SetActive(true);

        //display correctly feedbakc of identified risks and the current resources of the player
        feedbackScreen.GetComponent<Feedback>().DisplayFeedback("identificou", correctlyId);

        GameManager._instance.risks = GameManager._instance.risksIdentified.ToList();
        
    }
}
